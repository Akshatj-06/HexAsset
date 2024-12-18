using HexAsset.Models;

namespace HexAsset.Services;

public interface IAssetService
{
    Task<IEnumerable<Asset>> GetAllAssetsAsync();
    Task<Asset> GetAssetByIdAsync(int id);
    Task<Asset> AddAssetAsync(Asset asset);
    Task<Asset> UpdateAssetAsync(int id, Asset updatedAsset);
    Task<bool> DeleteAssetAsync(int id);
}