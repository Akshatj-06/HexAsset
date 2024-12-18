using HexAsset.Models;
using HexAsset.Repositories;

namespace HexAsset.Services
{
    public class AssetRequestService : IAssetRequestServices
    {
        private readonly IAssetRequestRepository _assetRequestRepository;

        public AssetRequestService(IAssetRequestRepository assetRequestRepository)
        {
            _assetRequestRepository = assetRequestRepository;
        }

        public async Task<IEnumerable<AssetRequest>> GetAllAssetRequestsAsync()
        {
            return await _assetRequestRepository.GetAllAssetRequestsAsync();
        }

        public async Task<AssetRequest?> GetAssetRequestByIdAsync(int id)
        {
            return await _assetRequestRepository.GetAssetRequestByIdAsync(id);
        }

        public async Task<AssetRequest> CreateAssetRequestAsync(AssetRequest assetRequest)
        {
            if (assetRequest == null)
            {
                throw new ArgumentNullException(nameof(assetRequest));
            }
            // Add additional business logic here if needed
            return await _assetRequestRepository.AddAssetRequestAsync(assetRequest);
        }

        public async Task<AssetRequest?> UpdateAssetRequestAsync(int id, AssetRequest updatedAssetRequest)
        {
            if (updatedAssetRequest == null)
            {
                throw new ArgumentNullException(nameof(updatedAssetRequest));
            }
            // Business logic: validate data or check preconditions here
            return await _assetRequestRepository.UpdateAssetRequestAsync(id, updatedAssetRequest);
        }

        public async Task<bool> DeleteAssetRequestAsync(int id)
        {
            return await _assetRequestRepository.DeleteAssetRequestAsync(id);
        }
    }
}