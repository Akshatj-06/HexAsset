using HexAsset.Data;
using HexAsset.Models.Dto;
using HexAsset.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuditRequestController : ControllerBase
	{
		private readonly AppDbContext dbContext;
		public AuditRequestController(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAuditRequests()
		{
			await dbContext.SaveChangesAsync();
			return Ok(dbContext.AuditRequests.ToList());
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetAuditRequestById(int id)
		{
			var auditRequest = dbContext.AuditRequests.Find(id);
			if (auditRequest == null)
			{
				return NotFound();
			}
			await dbContext.SaveChangesAsync();
			return Ok(auditRequest);
		}


		[HttpPost]
		public async Task<IActionResult> AddAuditRequest(AuditRequestDto auditassetRequestDto)
		{
			var newAuditRequest = new AuditRequest
			{
				UserId = auditassetRequestDto.UserId,
				AuditStatus = auditassetRequestDto.AuditStatus,
				AuditDate = auditassetRequestDto.AuditDate,
			};
			dbContext.AuditRequests.Add(newAuditRequest);
			await dbContext.SaveChangesAsync();

			return Ok(newAuditRequest);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAuditRequestById(int id, AuditRequestDto auditRequestDto)
		{
			var auditRequest = dbContext.AuditRequests.Find(id);
			if (auditRequest == null) { return NotFound(); }

			auditRequest.UserId = auditRequestDto.UserId;
			auditRequest.AuditStatus = auditRequestDto.AuditStatus;
			auditRequest.AuditDate = auditRequestDto.AuditDate;
			

			dbContext.AuditRequests.Update(auditRequest);
			await dbContext.SaveChangesAsync();
			return Ok(auditRequest);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuditRequestById(int id)
		{
			var auditRequest = dbContext.AuditRequests.Find(id);
			if (auditRequest == null)
			{
				return NotFound();
			}
			dbContext.AuditRequests.Remove(auditRequest);
			await dbContext.SaveChangesAsync();
			return Ok(auditRequest);
		}
	}
}
