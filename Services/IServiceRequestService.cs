using HexAsset.Models;

namespace HexAsset.Services;

public interface IServiceRequestService
{
    Task<List<ServiceRequest>> GetAllServiceRequestsAsync();
    Task<ServiceRequest?> GetServiceRequestByIdAsync(int id);
    Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest serviceRequest);
    Task<ServiceRequest?> UpdateServiceRequestAsync(int id, ServiceRequest updatedRequest);
    Task<bool> DeleteServiceRequestAsync(int id);
}