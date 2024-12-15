using HexAsset.Data;
using HexAsset.Models;
using Microsoft.EntityFrameworkCore;

namespace HexAsset.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly AppDbContext dbContext;

        public ServiceRequestRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await dbContext.ServiceRequests.ToListAsync();
        }

        public async Task<ServiceRequest?> GetServiceRequestByIdAsync(int id)
        {
            return await dbContext.ServiceRequests.FindAsync(id);
        }

        public async Task<ServiceRequest> AddServiceRequestAsync(ServiceRequest serviceRequest)
        {
            dbContext.ServiceRequests.Add(serviceRequest);
            await dbContext.SaveChangesAsync();
            return serviceRequest;
        }

        public async Task<ServiceRequest?> UpdateServiceRequestAsync(int id, ServiceRequest serviceRequest)
        {
            var existingRequest = await dbContext.ServiceRequests.FindAsync(id);
            if (existingRequest == null) return null;

            existingRequest.AssetId = serviceRequest.AssetId;
            existingRequest.UserId = serviceRequest.UserId;
            existingRequest.Description = serviceRequest.Description;
            existingRequest.RequestStatus = serviceRequest.RequestStatus;
            existingRequest.RequestDate = serviceRequest.RequestDate;

            dbContext.ServiceRequests.Update(existingRequest);
            await dbContext.SaveChangesAsync();
            return existingRequest;
        }

        public async Task<bool> DeleteServiceRequestAsync(int id)
        {
            var serviceRequest = await dbContext.ServiceRequests.FindAsync(id);
            if (serviceRequest == null) return false;

            dbContext.ServiceRequests.Remove(serviceRequest);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
