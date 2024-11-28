using HexAsset.Data;
using HexAsset.Models;
using HexAsset.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssetAllocationController : ControllerBase
	{
		private readonly AppDbContext dbContext;
		public AssetAllocationController(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[Authorize(Roles ="Admin")]
		[HttpGet]
		public async Task<IActionResult> GetAllAssetAllocations()
		{
			await dbContext.SaveChangesAsync();
			return Ok(dbContext.AssetAllocations.ToList());
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetAssetAllocationById(int id)
		{
			var assetAllocation = dbContext.AssetAllocations.Find(id);
			if (assetAllocation == null)
			{
				return NotFound();
			}
			await dbContext.SaveChangesAsync();
			return Ok(assetAllocation);
		}

		[HttpPost]
		public async Task<IActionResult> AddAssetAllocation(AssetAllocationDto assetAllocationDto)
		{
			var newAssetAllocation = new AssetAllocation
			{
				AssetId = assetAllocationDto.AssetId,
				UserId	= assetAllocationDto.UserId,
				AllocationDate = assetAllocationDto.AllocationDate,
				ReturnDate = assetAllocationDto.ReturnDate,
				AllocationStatus = assetAllocationDto.AllocationStatus,
			};
			dbContext.AssetAllocations.Add(newAssetAllocation);
			await dbContext.SaveChangesAsync();

			return Ok(newAssetAllocation);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAssetAllocationById(int id, AssetAllocationDto assetAllocationDto)
		{
			var assetAllocation = dbContext.AssetAllocations.Find(id);
			if (assetAllocation == null) { return NotFound(); }

			assetAllocation.AssetId = assetAllocationDto.AssetId;
			assetAllocation.UserId = assetAllocationDto.UserId;
			assetAllocation.AllocationDate = assetAllocationDto.AllocationDate;
			assetAllocation.ReturnDate = assetAllocationDto.ReturnDate;
			assetAllocation.AllocationStatus = assetAllocationDto.AllocationStatus;


			dbContext.AssetAllocations.Update(assetAllocation);
			await dbContext.SaveChangesAsync();
			return Ok(assetAllocation);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAssetAllocationById(int id)
		{
			var assetAllocation = dbContext.AssetAllocations.Find(id);
			if (assetAllocation == null)
			{
				return NotFound();
			}
			dbContext.AssetAllocations.Remove(assetAllocation);
			await dbContext.SaveChangesAsync();
			return Ok(assetAllocation);
		}
	}
}
