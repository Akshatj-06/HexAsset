using HexAsset.Data;
using HexAsset.Models;
using Microsoft.EntityFrameworkCore;

namespace HexAsset.Repositories
{
    public class AssetRequestRepository : IAssetRequestRepository
    {
        private readonly AppDbContext _dbContext;

        public AssetRequestRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AssetRequest>> GetAllAssetRequestsAsync()
        {
            return await _dbContext.AssetRequests.ToListAsync();
        }

        public async Task<AssetRequest?> GetAssetRequestByIdAsync(int id)
        {
            return await _dbContext.AssetRequests.FindAsync(id);
        }

        public async Task<AssetRequest> AddAssetRequestAsync(AssetRequest assetRequest)
        {
            await _dbContext.AssetRequests.AddAsync(assetRequest);
            await _dbContext.SaveChangesAsync();
            return assetRequest;
        }

        public async Task<AssetRequest?> UpdateAssetRequestAsync(int id, AssetRequest updatedAssetRequest)
        {
            var assetRequest = await _dbContext.AssetRequests.FindAsync(id);
            if (assetRequest == null) return null;

            assetRequest.AssetId = updatedAssetRequest.AssetId;
            assetRequest.UserId = updatedAssetRequest.UserId;
            assetRequest.RequestStatus = updatedAssetRequest.RequestStatus;
            assetRequest.RequestDate = updatedAssetRequest.RequestDate;

            _dbContext.AssetRequests.Update(assetRequest);
            await _dbContext.SaveChangesAsync();
            return assetRequest;
        }

        public async Task<bool> DeleteAssetRequestAsync(int id)
        {
            var assetRequest = await _dbContext.AssetRequests.FindAsync(id);
            if (assetRequest == null) return false;

            _dbContext.AssetRequests.Remove(assetRequest);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}