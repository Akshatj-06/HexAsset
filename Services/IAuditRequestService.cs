using HexAsset.Models;
using HexAsset.Models.Dto;

namespace HexAsset.Services;

public interface IAuditRequestService
{
    Task<IEnumerable<AuditRequest>> GetAllAuditRequestsAsync();
    Task<AuditRequest> GetAuditRequestByIdAsync(int id);
    Task<AuditRequest> CreateAuditRequestAsync(AuditRequestDto auditRequestDto);
    Task<AuditRequest> UpdateAuditRequestAsync(int id, AuditRequestDto auditRequestDto);
    Task<bool> DeleteAuditRequestAsync(int id);
}