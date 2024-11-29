using HexAsset.Data;
using HexAsset.Models.Dto;
using HexAsset.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceRequestController : ControllerBase
	{
		private readonly AppDbContext dbContext;
		public ServiceRequestController(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllServiceRequests()
		{
			await dbContext.SaveChangesAsync();
			return Ok(dbContext.ServiceRequests.ToList());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetServiceRequestById(int id)
		{
			var serviceRequest = dbContext.ServiceRequests.Find(id);
			if (serviceRequest == null)
			{
				return NotFound();
			}
			await dbContext.SaveChangesAsync();
			return Ok(serviceRequest);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsset(ServiceRequestDto serviceRequestDto)
		{
			var newServiceRequest = new ServiceRequest
			{
				AssetId=serviceRequestDto.AssetId,
				UserId=serviceRequestDto.UserId,
				Description=serviceRequestDto.Description,
				IssueType=serviceRequestDto.IssueType,
				RequestStatus=serviceRequestDto.RequestStatus,
				RequestDate=serviceRequestDto.RequestDate

			};
			dbContext.ServiceRequests.Add(newServiceRequest);
			await dbContext.SaveChangesAsync();

			return Ok(newServiceRequest);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateServiceRequestById(int id, ServiceRequestDto serviceRequestDto)
		{
			var serviceRequest = dbContext.ServiceRequests.Find(id);
			if (serviceRequest == null) { return NotFound(); }

			serviceRequest.AssetId = serviceRequestDto.AssetId;
			serviceRequest.UserId = serviceRequestDto.UserId;
			serviceRequest.Description = serviceRequestDto.Description;
			serviceRequest.IssueType = serviceRequestDto.IssueType;
			serviceRequest.RequestStatus = serviceRequestDto.RequestStatus;
			serviceRequest.RequestDate = serviceRequestDto.RequestDate;

			dbContext.ServiceRequests.Update(serviceRequest);
			await dbContext.SaveChangesAsync();
			return Ok(serviceRequest);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteServiceRequestById(int id)
		{
			var serviceRequest = dbContext.ServiceRequests.Find(id);
			if (serviceRequest == null)
			{
				return NotFound();
			}
			dbContext.ServiceRequests.Remove(serviceRequest);
			await dbContext.SaveChangesAsync();
			return Ok(serviceRequest);
		}
	}
}
