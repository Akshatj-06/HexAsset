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
			try
			{
				var auditRequests= await dbContext.AuditRequests.ToListAsync();
				return Ok(dbContext.AuditRequests.ToList());
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetAuditRequestById(int id)
		{
			try
			{
				var auditRequest = dbContext.AuditRequests.Find(id);
				if (auditRequest == null)
				{
					return NotFound($"Audit request with ID {id} not found.");
				}
				await dbContext.SaveChangesAsync();
				return Ok(auditRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}


		}


		[HttpPost]
		public async Task<IActionResult> AddAuditRequest(AuditRequestDto auditassetRequestDto)
		{
			try
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
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAuditRequestById(int id, AuditRequestDto auditRequestDto)
		{
			try
			{
				var auditRequest = dbContext.AuditRequests.Find(id);
				if (auditRequest == null) 
				{ 
					return NotFound($"Audit request with ID {id} not found.");
				}

				auditRequest.UserId = auditRequestDto.UserId;
				auditRequest.AuditStatus = auditRequestDto.AuditStatus;
				auditRequest.AuditDate = auditRequestDto.AuditDate;


				dbContext.AuditRequests.Update(auditRequest);
				await dbContext.SaveChangesAsync();
				return Ok(auditRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuditRequestById(int id)
		{
			try
			{
				var auditRequest = dbContext.AuditRequests.Find(id);
				if (auditRequest == null)
				{
					return NotFound($"Audit request with ID {id} not found.");
				}
				dbContext.AuditRequests.Remove(auditRequest);
				await dbContext.SaveChangesAsync();
				return Ok(auditRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}

		}
	}
}
