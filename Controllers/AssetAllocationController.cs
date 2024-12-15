using HexAsset.Data;
using HexAsset.Models;
using HexAsset.Models.Dto;
using HexAsset.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AssetAllocationController : ControllerBase
    {
        private readonly IAssetAllocationRepository repository;

        public AssetAllocationController(IAssetAllocationRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetAssetAllocation")]
        public async Task<IActionResult> GetAllAssetAllocations()
        {
            try
            {
                var assetAllocations = await repository.GetAllAssetAllocationsAsync();
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
            var assetAllocation = await repository.GetAssetAllocationByIdAsync(id);
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

                var addedAssetAllocation = await repository.AddAssetAllocationAsync(newAssetAllocation);
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

                var result = await repository.UpdateAssetAllocationAsync(id, updatedAssetAllocation);
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
                var isDeleted = await repository.DeleteAssetAllocationAsync(id);
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
