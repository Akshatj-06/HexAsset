using HexAsset.Models;
using HexAsset.Repositories;

namespace HexAsset.Services
{
    public class AssetAllocationService : IAssetAllocationService
    {
        private readonly IAssetAllocationRepository _assetAllocationRepository;

        public AssetAllocationService(IAssetAllocationRepository assetAllocationRepository)
        {
            _assetAllocationRepository = assetAllocationRepository;
        }

        public async Task<IEnumerable<AssetAllocation>> GetAllAssetAllocationsAsync()
        {
            return await _assetAllocationRepository.GetAllAssetAllocationsAsync();
        }

        public async Task<AssetAllocation> GetAssetAllocationByIdAsync(int id)
        {
            return await _assetAllocationRepository.GetAssetAllocationByIdAsync(id);
        }

        public async Task<AssetAllocation> AddAssetAllocationAsync(AssetAllocation assetAllocation)
        {
            // Add business logic if necessary before saving
            return await _assetAllocationRepository.AddAssetAllocationAsync(assetAllocation);
        }

        public async Task<AssetAllocation> UpdateAssetAllocationAsync(int id, AssetAllocation updatedAssetAllocation)
        {
            var existingAssetAllocation = await _assetAllocationRepository.GetAssetAllocationByIdAsync(id);
            if (existingAssetAllocation == null)
            {
                return null; // Or throw an exception based on your requirement
            }

            // Add business logic if needed
            return await _assetAllocationRepository.UpdateAssetAllocationAsync(id, updatedAssetAllocation);
        }

        public async Task<bool> DeleteAssetAllocationAsync(int id)
        {
            return await _assetAllocationRepository.DeleteAssetAllocationAsync(id);
        }
    }
}