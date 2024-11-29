﻿using HexAsset.Data;
using HexAsset.Models;
using HexAsset.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssetController : ControllerBase
	{
		private readonly AppDbContext dbContext;
		public AssetController(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllAssets()
		{
			await dbContext.SaveChangesAsync();
			return Ok(dbContext.Assets.ToList());
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetAssetById(int id)
		{
			var asset = dbContext.Assets.Find(id);
			if (asset == null)
			{
				return NotFound();
			}
			await dbContext.SaveChangesAsync();
			return Ok(asset);
		}


		[HttpPost]
		public async Task<IActionResult> AddAsset(AssetDto assetDto)
		{
			var newAsset = new Asset
			{
				AssetName = assetDto.AssetName,
				AssetCategory = assetDto.AssetCategory,
				AssetModel = assetDto.AssetModel,
				ManufacturingDate = assetDto.ManufacturingDate,
				ExpiryDate = assetDto.ExpiryDate,
				AssetValue = assetDto.AssetValue,
				CurrentStatus = assetDto.CurrentStatus
			};
			dbContext.Assets.Add(newAsset);
			await dbContext.SaveChangesAsync();

			return Ok(newAsset);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAssetById(int id, AssetDto assetDto)
		{
			var asset = dbContext.Assets.Find(id);
			if (asset == null) { return NotFound(); }

			asset.AssetName = assetDto.AssetName;
			asset.AssetCategory = assetDto.AssetCategory;
			asset.AssetModel = assetDto.AssetModel;
			asset.ManufacturingDate = assetDto.ManufacturingDate;
			asset.ExpiryDate = assetDto.ExpiryDate;
			asset.AssetValue = assetDto.AssetValue;
			asset.CurrentStatus = assetDto.CurrentStatus;


			dbContext.Assets.Update(asset);
			await dbContext.SaveChangesAsync();
			return Ok(asset);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAssetById(int id)
		{
			var asset = dbContext.Assets.Find(id);
			if (asset == null)
			{
				return NotFound();
			}
			dbContext.Assets.Remove(asset);
			await dbContext.SaveChangesAsync();
			return Ok(asset);
		}
	}
}