using HexAsset.Data;
using HexAsset.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HexAsset.Repositories
{
    public class AssetAllocationRepository : IAssetAllocationRepository
    {
        private readonly AppDbContext dbContext;

        public AssetAllocationRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AssetAllocation>> GetAllAssetAllocationsAsync()
        {
            return await dbContext.AssetAllocations.ToListAsync();
        }

        public async Task<AssetAllocation> GetAssetAllocationByIdAsync(int id)
        {
            return await dbContext.AssetAllocations.FindAsync(id);
        }

        public async Task<AssetAllocation> AddAssetAllocationAsync(AssetAllocation assetAllocation)
        {
            dbContext.AssetAllocations.Add(assetAllocation);
            await dbContext.SaveChangesAsync();
            return assetAllocation;
        }

        public async Task<AssetAllocation> UpdateAssetAllocationAsync(int id, AssetAllocation updatedAssetAllocation)
        {
            var existingAssetAllocation = await dbContext.AssetAllocations.FindAsync(id);
            if (existingAssetAllocation == null)
            {
                return null;
            }

            existingAssetAllocation.AssetId = updatedAssetAllocation.AssetId;
            existingAssetAllocation.UserId = updatedAssetAllocation.UserId;
            existingAssetAllocation.AllocationDate = updatedAssetAllocation.AllocationDate;
            existingAssetAllocation.ReturnDate = updatedAssetAllocation.ReturnDate;
            existingAssetAllocation.AllocationStatus = updatedAssetAllocation.AllocationStatus;

            dbContext.AssetAllocations.Update(existingAssetAllocation);
            await dbContext.SaveChangesAsync();
            return existingAssetAllocation;
        }

        public async Task<bool> DeleteAssetAllocationAsync(int id)
        {
            var assetAllocation = await dbContext.AssetAllocations.FindAsync(id);
            if (assetAllocation == null)
            {
                return false;
            }

            dbContext.AssetAllocations.Remove(assetAllocation);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
