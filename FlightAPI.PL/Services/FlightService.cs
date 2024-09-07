using AutoMapper;
using FlightAPI.PL.Entities;
using FlightAPI.PL.ViewModel.Flight;

namespace FlightAPI.PL.Services
{
    public class FlightService : IFlightService
    {
        private static List<Flight> _flights = new();
        private readonly ILogger<FlightService> _logger;
        private readonly IMapper _mapper;

        public FlightService(ILogger<FlightService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public List<FlightVM> GetList(out string errorMessage)
        {
            if (_flights.Any())
            {
                errorMessage = null;
                return _mapper.Map<List<FlightVM>>(_flights);
            }

            errorMessage = "Không có máy bay nào trong danh sách.";
            return null;
        }

        public FlightVM GetById(int id, out string errorMessage)
        {
            var flight = _flights.FirstOrDefault(f => f.Id == id);
            if (flight != null)
            {
                errorMessage = null;
                return _mapper.Map<FlightVM>(flight);
            }

            errorMessage = "Không tìm thấy máy bay với ID này.";
            return null;
        }

        public bool Create(CreateFlightVM request, out string errorMessage)
        {
            try
            {
                if (request.Capacity <= 0 ||
                    string.IsNullOrWhiteSpace(request.Model) || request.Model.Length > 100 ||
                    string.IsNullOrWhiteSpace(request.Manufacturer) || request.Manufacturer.Length > 100 ||
                    !new[] { "hoạt động", "bảo trì", "ngừng hoạt động" }.Contains(request.Status))
                {
                    errorMessage = "Dữ liệu đầu vào không hợp lệ. Vui lòng kiểm tra lại các trường thông tin.";
                    return false;
                }

                var flight = _mapper.Map<Flight>(request);
                flight.Id = _flights.Any() ? _flights.Max(f => f.Id) + 1 : 1;

                _flights.Add(flight);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi tạo máy bay: {ex.Message}";
                return false;
            }
        }

        public bool Update(int id, UpdateFlightVM request, out string errorMessage)
        {
            try
            {
                var flight = _flights.FirstOrDefault(f => f.Id == id);
                if (flight == null)
                {
                    errorMessage = "Không tìm thấy máy bay với ID này.";
                    return false;
                }

                if (request.Capacity <= 0 ||
                    string.IsNullOrWhiteSpace(request.Model) || request.Model.Length > 100 ||
                    string.IsNullOrWhiteSpace(request.Manufacturer) || request.Manufacturer.Length > 100 ||
                    !new[] { "hoạt động", "bảo trì", "ngừng hoạt động" }.Contains(request.Status))
                {
                    errorMessage = "Dữ liệu đầu vào không hợp lệ. Vui lòng kiểm tra lại các trường thông tin.";
                    return false;
                }

                _mapper.Map(request, flight);

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi cập nhật máy bay: {ex.Message}";
                return false;
            }
        }

        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var flight = _flights.FirstOrDefault(f => f.Id == id);
                if (flight == null)
                {
                    errorMessage = "Không tìm thấy máy bay với ID này.";
                    return false;
                }

                _flights.Remove(flight);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi xóa máy bay: {ex.Message}";
                return false;
            }
        }
    }
}
