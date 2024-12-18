using HexAsset.Models;
using HexAsset.Models.Dto;
using HexAsset.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetRequestController : ControllerBase
    {
        private readonly IAssetRequestServices _assetRequestService;

        public AssetRequestController(IAssetRequestServices assetRequestService)
        {
            _assetRequestService = assetRequestService;
        }

        [HttpGet("GetAssetRequest")]
        public async Task<IActionResult> GetAllAssetRequests()
        {
            try
            {
                var assetRequests = await _assetRequestService.GetAllAssetRequestsAsync();
                return Ok(assetRequests);
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
                var assetRequest = await _assetRequestService.GetAssetRequestByIdAsync(id);
                if (assetRequest == null)
                {
                    return NotFound($"Asset request with ID {id} not found.");
                }
                return Ok(assetRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddAssetRequest")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddAssetRequest(AssetRequestDto assetRequestDto)
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
                var addedAssetRequest = await _assetRequestService.CreateAssetRequestAsync(newAssetRequest);
                return Ok(addedAssetRequest);
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
                var updatedAssetRequest = new AssetRequest
                {
                    AssetId = assetRequestDto.AssetId,
                    UserId = assetRequestDto.UserId,
                    RequestStatus = assetRequestDto.RequestStatus,
                    RequestDate = assetRequestDto.RequestDate
                };
                var result = await _assetRequestService.UpdateAssetRequestAsync(id, updatedAssetRequest);
                if (result == null)
                {
                    return NotFound($"Asset request with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAssetRequest/{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeleteAssetRequestById(int id)
        {
            try
            {
                var isDeleted = await _assetRequestService.DeleteAssetRequestAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Asset request with ID {id} not found.");
                }
                return Ok($"Asset request with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
