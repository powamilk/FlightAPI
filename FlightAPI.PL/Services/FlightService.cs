using FlightAPI.PL.Entities;
using FlightAPI.PL.ViewModel.Flight;

namespace FlightAPI.PL.Services
{
    public class FlightService : IFlightService
    {
        private static List<Flight> _flights = new();
        private readonly ILogger<FlightService> _logger;

        public FlightService(ILogger<FlightService> logger)
        {
            _logger = logger;
        }

        public List<FlightVM> GetList(out string errorMessage)
        {
            if (_flights.Any())
            {
                errorMessage = null;
                return _flights.Select(f => new FlightVM
                {
                    Id = f.Id,
                    Model = f.Model,
                    Manufacturer = f.Manufacturer,
                    Capacity = f.Capacity,
                    Status = f.Status
                }).ToList();
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
                return new FlightVM
                {
                    Id = flight.Id,
                    Model = flight.Model,
                    Manufacturer = flight.Manufacturer,
                    Capacity = flight.Capacity,
                    Status = flight.Status
                };
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

                var flight = new Flight
                {
                    Id = _flights.Any() ? _flights.Max(f => f.Id) + 1 : 1,
                    Model = request.Model,
                    Manufacturer = request.Manufacturer,
                    Capacity = request.Capacity,
                    Status = request.Status
                };

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

                flight.Model = request.Model;
                flight.Manufacturer = request.Manufacturer;
                flight.Capacity = request.Capacity;
                flight.Status = request.Status;

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
