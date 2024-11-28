﻿using System.ComponentModel.DataAnnotations;

namespace HexAsset.Models
{
	public class Asset
	{
		[Key]
		public int AssetId { get; set; }
		public required string AssetName { get; set; }
		public string? AssetCategory { get; set; }
		public string? AssetModel { get; set; }
		public DateTime? ManufacturingDate { get; set; }
		public DateTime? ExpiryDate { get; set; }
		public int AssetValue { get; set; }
		public required string CurrentStatus { get; set; }


		public ICollection<AssetAllocation>? Allocations { get; set; }
		public ICollection<ServiceRequest>? ServiceRequests { get; set; }
		public ICollection<AssetRequest>? AssetRequests { get; set; }
	}
}
