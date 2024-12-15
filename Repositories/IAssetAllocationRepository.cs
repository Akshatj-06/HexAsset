using HexAsset.Models;

namespace HexAsset.Repositories;

public interface IAssetAllocationRepository
{
    Task<IEnumerable<AssetAllocation>> GetAllAssetAllocationsAsync();
    Task<AssetAllocation> GetAssetAllocationByIdAsync(int id);
    Task<AssetAllocation> AddAssetAllocationAsync(AssetAllocation assetAllocation);
    Task<AssetAllocation> UpdateAssetAllocationAsync(int id, AssetAllocation assetAllocation);
    Task<bool> DeleteAssetAllocationAsync(int id);
}