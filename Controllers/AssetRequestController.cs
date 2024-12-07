using HexAsset.Data;
using HexAsset.Models.Dto;
using HexAsset.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		[Route("GetAssetRequest")]
		public async Task<IActionResult> GetAllAssetRequests()
		{
			try
			{
				var assetRequests= await dbContext.AssetRequests.ToListAsync();
				return Ok(dbContext.AssetRequests.ToList());
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}

		[HttpGet("GetAssetRequestById/{id}")]
		public async Task<IActionResult> GetAssetRequestById(int id)
		{
			try
			{
				var assetRequest = dbContext.ServiceRequests.Find(id);
				if (assetRequest == null)
				{
					return NotFound($"Asset request with ID {id} not found.");
				}
				await dbContext.SaveChangesAsync();
				return Ok(assetRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}

		[HttpPost]
		[Route("AddAssetRequest")]
		public async Task<IActionResult> AddAsset(AssetRequestDto assetRequestDto)
		{
			try
			{
				var newAssetRequest = new AssetRequest
				{
					AssetId = assetRequestDto.AssetId,
					UserId = assetRequestDto.UserId,
					RequestStatus = assetRequestDto.RequestStatus,
					RequestDate = assetRequestDto.RequestDate

				};
				dbContext.AssetRequests.Add(newAssetRequest);
				await dbContext.SaveChangesAsync();

				return Ok(newAssetRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}

		[HttpPut("UpdateAssetRequest/{id}")]
		public async Task<IActionResult> UpdateAssetRequestById(int id, AssetRequestDto assetRequestDto)
		{
			try
			{
				var assetRequest = dbContext.AssetRequests.Find(id);
				if (assetRequest == null)
				{ 
					return NotFound($"Asset request with ID {id} not found."); 
				}

				assetRequest.AssetId = assetRequestDto.AssetId;
				assetRequest.UserId = assetRequestDto.UserId;
				assetRequest.RequestStatus = assetRequestDto.RequestStatus;
				assetRequest.RequestDate = assetRequestDto.RequestDate;

				dbContext.AssetRequests.Update(assetRequest);
				await dbContext.SaveChangesAsync();
				return Ok(assetRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}

		[HttpDelete("DeleteAssetRequest/{id}")]
		public async Task<IActionResult> DeleteAssetRequestById(int id)
		{
			try
			{
				var assetRequest = dbContext.AssetRequests.Find(id);
				if (assetRequest == null)
				{
					return NotFound($"Asset request with ID {id} not found.");
				}
				dbContext.AssetRequests.Remove(assetRequest);
				await dbContext.SaveChangesAsync();
				return Ok(assetRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}
	}
}
