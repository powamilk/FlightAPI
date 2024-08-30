using FlightAPI.PL.ViewModel.Flight;

namespace FlightAPI.PL.Services
{
    public interface IFlightService
    {
        List<FlightVM> GetList(out string errorMessage);
        FlightVM GetById(int id, out string errorMessage);
        bool Create(CreateFlightVM request, out string errorMessage);
        bool Update(int id, UpdateFlightVM request, out string errorMessage);
        bool Delete(int id, out string errorMessage);

    }
}
