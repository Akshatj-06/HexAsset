using HexAsset.Models;

namespace HexAsset.Services;

public interface IAssetAllocationService
{
    Task<IEnumerable<AssetAllocation>> GetAllAssetAllocationsAsync();
    Task<AssetAllocation> GetAssetAllocationByIdAsync(int id);
    Task<AssetAllocation> AddAssetAllocationAsync(AssetAllocation assetAllocation);
    Task<AssetAllocation> UpdateAssetAllocationAsync(int id, AssetAllocation updatedAssetAllocation);
    Task<bool> DeleteAssetAllocationAsync(int id);
}