using DroneAPI.Controllers.BaseController;
using DroneAPI.Core;
using DroneAPI.Extensions;
using DroneAPI.Models;
using DroneAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace DroneAPI.Controllers
{
    /// <summary>
    /// Dispatch Controller
    /// </summary>
    /// <seealso cref="ApiBaseController" />
    [ApiController]
    [Route("/v1/dispatch")]
    public class DispatchController : ApiBaseController
    {
        /// <summary>
        /// The drone validator
        /// </summary>
        private IValidator<DroneRegisterModel> droneValidator;

        /// <summary>
        /// The load drone with medications validator
        /// </summary>
        private IValidator<LoadDroneWithMedicationsModel> loadDroneWithMedicationsValidator;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<DispatchController> logger;

        /// <summary>
        /// The drone service
        /// </summary>
        private readonly IDroneService droneService;

        /// <summary>
        /// The medication service
        /// </summary>
        private readonly IMedicationService medicationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="droneService">The drone service.</param>
        /// <param name="medicationService">The medication service.</param>
        /// <param name="loadDroneWithMedicationsValidator">The load drone with medications validator.</param>
        /// <param name="droneValidator">The drone validator.</param>
        public DispatchController(ILogger<DispatchController> logger,
            IDroneService droneService,
            IMedicationService medicationService,
            IValidator<LoadDroneWithMedicationsModel> loadDroneWithMedicationsValidator,
            IValidator<DroneRegisterModel> droneValidator)
        {
            this.logger = logger;
            this.droneService = droneService;
            this.medicationService = medicationService;
            this.droneValidator = droneValidator;
            this.loadDroneWithMedicationsValidator = loadDroneWithMedicationsValidator;
        }

        /// <summary>
        /// Registers the drone.
        /// </summary>
        /// <param name="droneModel">The drone model.</param>
        /// <returns>IActionResult</returns>
        [Route("register-drone")]
        [HttpPost()]
        public async Task<IActionResult> RegisterDrone(DroneRegisterModel droneModel)
        {
            try
            {
                var result = await droneValidator.ValidateAsync(droneModel);
                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);
                    return BadRequest(ModelState);
                }

                var model = await droneService.RegisterDrone(droneModel);
                if (model == null ||
                    model.Id == 0)
                    return Problem(ModelState.ToString(), DroneAPITexts.SomethingWendWrong);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return HandleException(ex, logger);
            }
        }

        /// <summary>
        /// Loads the drone with medication items.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("load-drone-with-medication-items")]
        [HttpPost()]
        public async Task<IActionResult> LoadDroneWithMedicationItems(LoadDroneWithMedicationsModel model)
        {
            try
            {
                var result = await loadDroneWithMedicationsValidator.ValidateAsync(model);
                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);
                    return BadRequest(ModelState);
                }

                var availableDrones = await droneService.GetAvailableDronesForLoading();
                var medications = await medicationService.GetMedications(model.MedicationIds);

                if (!availableDrones.Any())
                    return BadRequest(DroneAPITexts.IdeleDronesNotFound);

                var medicationWeight = medications.Sum(sm => sm.Weight);
                var suitableDrone = availableDrones.
                    Where(w => w.WeightLimit >= medicationWeight).
                    OrderBy(o => o.WeightLimit).FirstOrDefault();

                if(suitableDrone == null)
                    return BadRequest(DroneAPITexts.SuitableDroneNotFound);

                await droneService.UpdateDroneState(suitableDrone.Id, DroneState.LOADING);

                var loadedDrone = await droneService.LoadDroneWithMedicationItems(suitableDrone.Id, model.MedicationIds);
                return Ok(loadedDrone);
            }
            catch (Exception ex)
            {
                return HandleException(ex, logger);
            }
        }

        /// <summary>
        /// Gets the loaded medication items.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <returns>Medication items for the given drone id.</returns>
        [Route("get-loaded-medication-items:int")]
        [HttpGet()]
        public async Task<IActionResult> GetLoadedMedicationItems(int droneId)
        {
            try
            {
                var medications = await this.droneService.GetLoadedMedicationItems(droneId);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                return HandleException(ex, logger);
            }
        }

        /// <summary>
        /// Gets the available drones.
        /// </summary>
        /// <returns></returns>
        [Route("get-available-drones-for-loading")]
        [HttpGet()]
        public async Task<IActionResult> GetAvailableDronesForLoading()
        {
            try
            {
                return Ok(await this.droneService.GetAvailableDronesForLoading());
            }
            catch (Exception ex)
            {
                return HandleException(ex, logger);
            }
        }

        /// <summary>
        /// Checks the drone battery level.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <returns>The battery level of the drone</returns>
        [Route("check-drone-battery-level:int")]
        [HttpGet()]
        public async Task<IActionResult> CheckDroneBatteryLevel(int droneId)
        {
            try
            {
                return Ok(await this.droneService.GetDroneBatteryLevel(droneId));
            }
            catch (Exception ex)
            {
                return HandleException(ex, logger);
            }
        }
    }
}