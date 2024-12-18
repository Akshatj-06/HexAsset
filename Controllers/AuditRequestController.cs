using HexAsset.Models.Dto;
using HexAsset.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HexAsset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditRequestController : ControllerBase
    {
        private readonly IAuditRequestService _auditRequestService;

        public AuditRequestController(IAuditRequestService auditRequestService)
        {
            _auditRequestService = auditRequestService;
        }

        [HttpGet]
        [Route("GetAuditRequest")]
        public async Task<IActionResult> GetAllAuditRequests()
        {
            try
            {
                var auditRequests = await _auditRequestService.GetAllAuditRequestsAsync();
                return Ok(auditRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAuditRequestById/{id}")]
        public async Task<IActionResult> GetAuditRequestById(int id)
        {
            try
            {
                var auditRequest = await _auditRequestService.GetAuditRequestByIdAsync(id);
                if (auditRequest == null)
                {
                    return NotFound($"Audit request with ID {id} not found.");
                }

                return Ok(auditRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddAuditRequest")]
        public async Task<IActionResult> AddAuditRequest(AuditRequestDto auditRequestDto)
        {
            try
            {
                var newAuditRequest = await _auditRequestService.CreateAuditRequestAsync(auditRequestDto);
                return Ok(newAuditRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateAuditRequest/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuditRequestById(int id, AuditRequestDto auditRequestDto)
        {
            try
            {
                var updatedAuditRequest = await _auditRequestService.UpdateAuditRequestAsync(id, auditRequestDto);
                if (updatedAuditRequest == null)
                {
                    return NotFound($"Audit request with ID {id} not found.");
                }

                return Ok(updatedAuditRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAuditRequest/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuditRequestById(int id)
        {
            try
            {
                var isDeleted = await _auditRequestService.DeleteAuditRequestAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Audit request with ID {id} not found.");
                }

                return Ok($"Audit request with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
