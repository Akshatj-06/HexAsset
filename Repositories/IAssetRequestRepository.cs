using HexAsset.Models;

namespace HexAsset.Repositories;

public interface IAssetRequestRepository
{
    Task<IEnumerable<AssetRequest>> GetAllAssetRequestsAsync();
    Task<AssetRequest?> GetAssetRequestByIdAsync(int id);
    Task<AssetRequest> AddAssetRequestAsync(AssetRequest assetRequest);
    Task<AssetRequest?> UpdateAssetRequestAsync(int id, AssetRequest assetRequest);
    Task<bool> DeleteAssetRequestAsync(int id);
}