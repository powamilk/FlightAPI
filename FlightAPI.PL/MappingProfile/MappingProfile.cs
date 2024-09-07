using AutoMapper;
using FlightAPI.PL.Entities;
using FlightAPI.PL.ViewModel.Flight;

namespace FlightAPI.PL.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Flight, FlightVM>().ReverseMap();
            CreateMap<CreateFlightVM, Flight>();
            CreateMap<UpdateFlightVM, Flight>();
        }
    }
}
