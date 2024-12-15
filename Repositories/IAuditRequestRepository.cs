using HexAsset.Models;
using HexAsset.Models.Dto;

namespace HexAsset.Repositories;

public interface IAuditRequestRepository
{
    Task<IEnumerable<AuditRequest>> GetAllAsync();
    Task<AuditRequest> GetByIdAsync(int id);
    Task<AuditRequest> AddAsync(AuditRequestDto auditRequestDto);
    Task<AuditRequest> UpdateAsync(int id, AuditRequestDto auditRequestDto);
    Task<bool> DeleteAsync(int id);
}