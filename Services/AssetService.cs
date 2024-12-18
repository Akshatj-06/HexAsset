using HexAsset.Models;
using HexAsset.Repositories;

namespace HexAsset.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _assetRepository.GetAllAssetsAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            return await _assetRepository.GetAssetByIdAsync(id);
        }

        public async Task<Asset> AddAssetAsync(Asset asset)
        {
            // Add any business logic or validation here
            if (string.IsNullOrEmpty(asset.AssetName) || asset.AssetValue <= 0)
            {
                throw new ArgumentException("Invalid asset data.");
            }

            return await _assetRepository.AddAssetAsync(asset);
        }

        public async Task<Asset> UpdateAssetAsync(int id, Asset updatedAsset)
        {
            // Validate input
            if (updatedAsset == null || string.IsNullOrEmpty(updatedAsset.AssetName))
            {
                throw new ArgumentException("Updated asset data is invalid.");
            }

            var existingAsset = await _assetRepository.GetAssetByIdAsync(id);
            if (existingAsset == null)
            {
                throw new KeyNotFoundException("Asset not found.");
            }

            return await _assetRepository.UpdateAssetAsync(id, updatedAsset);
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            var existingAsset = await _assetRepository.GetAssetByIdAsync(id);
            if (existingAsset == null)
            {
                throw new KeyNotFoundException("Asset not found.");
            }

            return await _assetRepository.DeleteAssetAsync(id);
        }
    }
}