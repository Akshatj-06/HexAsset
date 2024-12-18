using HexAsset.Models;

namespace HexAsset.Services;

public interface IAssetRequestServices
{
    Task<IEnumerable<AssetRequest>> GetAllAssetRequestsAsync();
    Task<AssetRequest?> GetAssetRequestByIdAsync(int id);
    Task<AssetRequest> CreateAssetRequestAsync(AssetRequest assetRequest);
    Task<AssetRequest?> UpdateAssetRequestAsync(int id, AssetRequest updatedAssetRequest);
    Task<bool> DeleteAssetRequestAsync(int id);
}