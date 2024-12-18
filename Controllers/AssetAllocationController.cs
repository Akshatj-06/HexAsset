using HexAsset.Models;
using HexAsset.Models.Dto;
using HexAsset.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetAllocationController : ControllerBase
    {
        private readonly IAssetAllocationService _assetAllocationService;

        public AssetAllocationController(IAssetAllocationService assetAllocationService)
        {
            _assetAllocationService = assetAllocationService;
        }

        [HttpGet]
        [Route("GetAssetAllocation")]
        public async Task<IActionResult> GetAllAssetAllocations()
        {
            try
            {
                var assetAllocations = await _assetAllocationService.GetAllAssetAllocationsAsync();
                return Ok(assetAllocations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAssetAllocationById/{id}")]
        public async Task<IActionResult> GetAssetAllocationById(int id)
        {
            var assetAllocation = await _assetAllocationService.GetAssetAllocationByIdAsync(id);
            if (assetAllocation == null)
            {
                return NotFound($"AssetAllocation with ID {id} not found.");
            }
            return Ok(assetAllocation);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddAssetAllocation")]
        public async Task<IActionResult> AddAssetAllocation(AssetAllocationDto assetAllocationDto)
        {
            try
            {
                var newAssetAllocation = new AssetAllocation
                {
                    AssetId = assetAllocationDto.AssetId,
                    UserId = assetAllocationDto.UserId,
                    AllocationDate = assetAllocationDto.AllocationDate,
                    ReturnDate = assetAllocationDto.ReturnDate,
                    AllocationStatus = assetAllocationDto.AllocationStatus,
                };

                var addedAssetAllocation = await _assetAllocationService.AddAssetAllocationAsync(newAssetAllocation);
                return Ok(addedAssetAllocation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateAssetAllocation/{id}")]
        public async Task<IActionResult> UpdateAssetAllocationById(int id, AssetAllocationDto assetAllocationDto)
        {
            try
            {
                var updatedAssetAllocation = new AssetAllocation
                {
                    AssetId = assetAllocationDto.AssetId,
                    UserId = assetAllocationDto.UserId,
                    AllocationDate = assetAllocationDto.AllocationDate,
                    ReturnDate = assetAllocationDto.ReturnDate,
                    AllocationStatus = assetAllocationDto.AllocationStatus,
                };

                var result = await _assetAllocationService.UpdateAssetAllocationAsync(id, updatedAssetAllocation);
                if (result == null)
                {
                    return NotFound($"AssetAllocation with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAssetAllocation/{id}")]
        public async Task<IActionResult> DeleteAssetAllocationById(int id)
        {
            try
            {
                var isDeleted = await _assetAllocationService.DeleteAssetAllocationAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"AssetAllocation with ID {id} not found.");
                }
                return Ok($"AssetAllocation with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
