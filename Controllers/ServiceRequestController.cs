using HexAsset.Models;
using HexAsset.Models.Dto;
using HexAsset.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexAsset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestRepository serviceRequestRepository;

        public ServiceRequestController(IServiceRequestRepository serviceRequestRepository)
        {
            this.serviceRequestRepository = serviceRequestRepository;
        }

        [HttpGet]
        [Route("GetServiceRequest")]
        public async Task<IActionResult> GetAllServiceRequests()
        {
            try
            {
                var serviceRequests = await serviceRequestRepository.GetAllServiceRequestsAsync();
                return Ok(serviceRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetServiceRequestById/{id}")]
        public async Task<IActionResult> GetServiceRequestById(int id)
        {
            try
            {
                var serviceRequest = await serviceRequestRepository.GetServiceRequestByIdAsync(id);
                if (serviceRequest == null)
                {
                    return NotFound($"Service request with ID {id} not found.");
                }
                return Ok(serviceRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddServiceRequest")]
        public async Task<IActionResult> AddServiceRequest(ServiceRequestDto serviceRequestDto)
        {
            try
            {
                var newServiceRequest = new ServiceRequest
                {
                    AssetId = serviceRequestDto.AssetId,
                    UserId = serviceRequestDto.UserId,
                    Description = serviceRequestDto.Description,
                    RequestStatus = serviceRequestDto.RequestStatus,
                    RequestDate = serviceRequestDto.RequestDate
                };

                var createdRequest = await serviceRequestRepository.AddServiceRequestAsync(newServiceRequest);
                return Ok(createdRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("UpdateServiceRequest/{id}")]
        public async Task<IActionResult> UpdateServiceRequestById(int id, ServiceRequestDto serviceRequestDto)
        {
            try
            {
                var updatedRequest = await serviceRequestRepository.UpdateServiceRequestAsync(id, new ServiceRequest
                {
                    AssetId = serviceRequestDto.AssetId,
                    UserId = serviceRequestDto.UserId,
                    Description = serviceRequestDto.Description,
                    RequestStatus = serviceRequestDto.RequestStatus,
                    RequestDate = serviceRequestDto.RequestDate
                });

                if (updatedRequest == null)
                {
                    return NotFound($"Service request with ID {id} not found.");
                }

                return Ok(updatedRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpDelete("DeleteServiceRequest/{id}")]
        public async Task<IActionResult> DeleteServiceRequestById(int id)
        {
            try
            {
                var result = await serviceRequestRepository.DeleteServiceRequestAsync(id);
                if (!result)
                {
                    return NotFound($"Service request with ID {id} not found.");
                }

                return Ok($"Service request with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
