using HexAsset.Models.Dto;
using HexAsset.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HexAsset.Models;

namespace HexAsset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet("GetAsset")]
        public async Task<IActionResult> GetAllAssets()
        {
            try
            {
                var assets = await _assetService.GetAllAssetsAsync();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAssetById/{id}")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            try
            {
                var asset = await _assetService.GetAssetByIdAsync(id);
                if (asset == null)
                {
                    return NotFound($"Asset with ID {id} not found.");
                }
                return Ok(asset);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddAsset")]
        public async Task<IActionResult> AddAsset(AssetDto assetDto)
        {
            try
            {
                var newAsset = new Asset
                {
                    AssetName = assetDto.AssetName,
                    AssetCategory = assetDto.AssetCategory,
                    AssetModel = assetDto.AssetModel,
                    AssetValue = assetDto.AssetValue,
                    CurrentStatus = assetDto.CurrentStatus
                };

                var addedAsset = await _assetService.AddAssetAsync(newAsset);
                return Ok(addedAsset);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateAsset/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAssetById(int id, AssetDto assetDto)
        {
            try
            {
                var updatedAsset = new Asset
                {
                    AssetName = assetDto.AssetName,
                    AssetCategory = assetDto.AssetCategory,
                    AssetModel = assetDto.AssetModel,
                    AssetValue = assetDto.AssetValue,
                    CurrentStatus = assetDto.CurrentStatus
                };

                var asset = await _assetService.UpdateAssetAsync(id, updatedAsset);
                return Ok(asset);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAsset/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAssetById(int id)
        {
            try
            {
                var deleted = await _assetService.DeleteAssetAsync(id);
                return Ok($"Asset with ID {id} has been deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
