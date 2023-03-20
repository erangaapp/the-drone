using Microsoft.AspNetCore.Mvc;

namespace DroneAPI.Controllers.BaseController
{
    /// <summary>
    /// The API Base Controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public class ApiBaseController : ControllerBase
    {
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>BadRequest result</returns>
        [NonAction]
        public IActionResult HandleException(Exception exception, ILogger logger)
        {
            var key = Guid.NewGuid().ToString();
            logger.LogError(key, exception);

            return BadRequest(string.Format(DroneAPITexts.ErrorOccured, key));
        }
    }
}
