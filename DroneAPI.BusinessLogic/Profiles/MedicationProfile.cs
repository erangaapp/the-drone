using AutoMapper;
using DroneAPI.Data;
using DroneAPI.Models;

namespace DroneAPI.Services.Profiles
{
    /// <summary>
    /// The Medication Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MedicationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationProfile"/> class.
        /// </summary>
        public MedicationProfile()
        {
            CreateMap<MedicationEntity, MedicationModel>().
                ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code)).
                ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).
                ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{src.Id}.png")).
                ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight));
        }
    }
}
