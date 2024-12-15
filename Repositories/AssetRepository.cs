using HexAsset.Data;
using HexAsset.Models;
using Microsoft.EntityFrameworkCore;

namespace HexAsset.Repositories;


    public class AssetRepository : IAssetRepository
    {
        private readonly AppDbContext _dbContext;

        public AssetRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _dbContext.Assets.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            return await _dbContext.Assets.FindAsync(id);
        }

        public async Task<Asset> AddAssetAsync(Asset asset)
        {
            _dbContext.Assets.Add(asset);
            await _dbContext.SaveChangesAsync();
            return asset;
        }

        public async Task<Asset> UpdateAssetAsync(int id, Asset updatedAsset)
        {
            var existingAsset = await _dbContext.Assets.FindAsync(id);
            if (existingAsset == null)
                return null;

            existingAsset.AssetName = updatedAsset.AssetName;
            existingAsset.AssetCategory = updatedAsset.AssetCategory;
            existingAsset.AssetModel = updatedAsset.AssetModel;
            existingAsset.AssetValue = updatedAsset.AssetValue;
            existingAsset.CurrentStatus = updatedAsset.CurrentStatus;

            _dbContext.Assets.Update(existingAsset);
            await _dbContext.SaveChangesAsync();

            return existingAsset;
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            var asset = await _dbContext.Assets.FindAsync(id);
            if (asset == null)
                return false;

            _dbContext.Assets.Remove(asset);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
