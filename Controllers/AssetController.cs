using HexAsset.Data;
using HexAsset.Models;
using HexAsset.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HexAsset.Repositories;

namespace HexAsset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;

        public AssetController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        [HttpGet("GetAsset")]
        public async Task<IActionResult> GetAllAssets()
        {
            try
            {
                var assets = await _assetRepository.GetAllAssetsAsync();
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
                var asset = await _assetRepository.GetAssetByIdAsync(id);
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
        [Authorize(Roles = "Admin")]
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

                var addedAsset = await _assetRepository.AddAssetAsync(newAsset);
                return Ok(addedAsset);
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

                var asset = await _assetRepository.UpdateAssetAsync(id, updatedAsset);
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

        [HttpDelete("DeleteAsset/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAssetById(int id)
        {
            try
            {
                var deleted = await _assetRepository.DeleteAssetAsync(id);
                if (!deleted)
                {
                    return NotFound($"Asset with ID {id} not found.");
                }

                return Ok($"Asset with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
