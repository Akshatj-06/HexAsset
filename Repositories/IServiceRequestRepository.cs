using HexAsset.Models;

namespace HexAsset.Repositories;

public interface IServiceRequestRepository
{
    Task<List<ServiceRequest>> GetAllServiceRequestsAsync();
    Task<ServiceRequest?> GetServiceRequestByIdAsync(int id);
    Task<ServiceRequest> AddServiceRequestAsync(ServiceRequest serviceRequest);
    Task<ServiceRequest?> UpdateServiceRequestAsync(int id, ServiceRequest serviceRequest);
    Task<bool> DeleteServiceRequestAsync(int id);

}