using AutoMapper;
using DroneAPI.Data;
using DroneAPI.Models;
using System.Linq;

namespace DroneAPI.Services.Profiles
{
    /// <summary>
    /// The Drone Medication Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DroneMedicationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneMedicationProfile"/> class.
        /// </summary>
        public DroneMedicationProfile()
        {
            CreateMap<DroneEntity, DroneWithMedicationsModel>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).
                ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber)).
                ForMember(dest => dest.WeightLimit, opt => opt.MapFrom(src => src.WeightLimit)).
                ForMember(dest => dest.BatteryCapacity, opt => opt.MapFrom(src => src.BatteryCapacity)).
                ForMember(dest => dest.State, opt => opt.MapFrom(src => src.DroneState.ToString())).
                ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.DroneModel.ToString())).
                ForMember(dest => dest.Medications, opt => opt.MapFrom(src => src.DroneMedications.
                Select(medication => new MedicationModel() { 
                    Code = medication.Medication.Code,
                    Name = medication.Medication.Name,
                    Weight= medication.Medication.Weight,
                    Image= $"{medication.Medication.Id}.png",
                }).ToList()));
        }
    }
}
