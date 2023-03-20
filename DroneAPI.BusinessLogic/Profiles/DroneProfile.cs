using AutoMapper;
using DroneAPI.Data;
using DroneAPI.Models;
using System;

namespace DroneAPI.Services.Profiles
{
    /// <summary>
    /// The Drone Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DroneProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneProfile"/> class.
        /// </summary>
        public DroneProfile()
        {
            CreateMap<DroneEntity, DroneModel>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).
                ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber)).
                ForMember(dest => dest.WeightLimit, opt => opt.MapFrom(src => src.WeightLimit)).
                ForMember(dest => dest.BatteryCapacity, opt => opt.MapFrom(src => $"{src.BatteryCapacity:n2}%")).
                ForMember(dest => dest.State, opt => opt.MapFrom(src => src.DroneState.ToString())).
                ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.DroneModel.ToString()));

            CreateMap<DroneRegisterModel, DroneEntity>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0)).
                ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber)).
                ForMember(dest => dest.WeightLimit, opt => opt.MapFrom(src => src.WeightLimit)).
                ForMember(dest => dest.DroneModel, opt => opt.MapFrom(src => Enum.Parse(typeof(Core.DroneModel), src.Model))).
                ForMember(dest => dest.DroneState, opt => opt.MapFrom(src => Core.DroneState.IDLE)).
                ForMember(dest => dest.BatteryCapacity, opt => opt.MapFrom(src => 100));
        }
    }
}
