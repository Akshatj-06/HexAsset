using HexAsset.Models;
using HexAsset.Repositories;

namespace HexAsset.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _repository;

        public ServiceRequestService(IServiceRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _repository.GetAllServiceRequestsAsync();
        }

        public async Task<ServiceRequest?> GetServiceRequestByIdAsync(int id)
        {
            return await _repository.GetServiceRequestByIdAsync(id);
        }

        public async Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest serviceRequest)
        {
            // Add additional business logic here, if required
            return await _repository.AddServiceRequestAsync(serviceRequest);
        }

        public async Task<ServiceRequest?> UpdateServiceRequestAsync(int id, ServiceRequest updatedRequest)
        {
            var existingRequest = await _repository.GetServiceRequestByIdAsync(id);
            if (existingRequest == null) return null;

            // Add any validation or business rules here if necessary
            return await _repository.UpdateServiceRequestAsync(id, updatedRequest);
        }

        public async Task<bool> DeleteServiceRequestAsync(int id)
        {
            return await _repository.DeleteServiceRequestAsync(id);
        }
    }
}