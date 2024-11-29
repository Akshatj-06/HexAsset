using HexAsset.Data;
using HexAsset.Models.Dto;
using HexAsset.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssetRequestController : ControllerBase
	{
		private readonly AppDbContext dbContext;
		public AssetRequestController(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllAssetRequests()
		{
			await dbContext.SaveChangesAsync();
			return Ok(dbContext.AssetRequests.ToList());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAssetRequestById(int id)
		{
			var assetRequest = dbContext.ServiceRequests.Find(id);
			if (assetRequest == null)
			{
				return NotFound();
			}
			await dbContext.SaveChangesAsync();
			return Ok(assetRequest);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsset(AssetRequestDto assetRequestDto)
		{
			var newAssetRequest = new AssetRequest
			{
				AssetId = assetRequestDto.AssetId,
				UserId = assetRequestDto.UserId,
				IssueType = assetRequestDto.IssueType,
				RequestStatus = assetRequestDto.RequestStatus,
				RequestDate = assetRequestDto.RequestDate

			};
			dbContext.AssetRequests.Add(newAssetRequest);
			await dbContext.SaveChangesAsync();

			return Ok(newAssetRequest);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAssetRequestById(int id, AssetRequestDto assetRequestDto)
		{
			var assetRequest = dbContext.AssetRequests.Find(id);
			if (assetRequest == null) { return NotFound(); }

			assetRequest.AssetId = assetRequestDto.AssetId;
			assetRequest.UserId = assetRequestDto.UserId;
			assetRequest.IssueType = assetRequestDto.IssueType;
			assetRequest.RequestStatus = assetRequestDto.RequestStatus;
			assetRequest.RequestDate = assetRequestDto.RequestDate;

			dbContext.AssetRequests.Update(assetRequest);
			await dbContext.SaveChangesAsync();
			return Ok(assetRequest);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAssetRequestById(int id)
		{
			var assetRequest = dbContext.AssetRequests.Find(id);
			if (assetRequest == null)
			{
				return NotFound();
			}
			dbContext.AssetRequests.Remove(assetRequest);
			await dbContext.SaveChangesAsync();
			return Ok(assetRequest);
		}
	}
}
