using HexAsset.Models;
using HexAsset.Models.Dto;
using HexAsset.Repositories;

namespace HexAsset.Services


{
    public class AuditRequestService : IAuditRequestService
    {
        private readonly IAuditRequestRepository _auditRequestRepository;

        public AuditRequestService(IAuditRequestRepository auditRequestRepository)
        {
            _auditRequestRepository = auditRequestRepository;
        }

        public async Task<IEnumerable<AuditRequest>> GetAllAuditRequestsAsync()
        {
            return await _auditRequestRepository.GetAllAsync();
        }

        public async Task<AuditRequest> GetAuditRequestByIdAsync(int id)
        {
            return await _auditRequestRepository.GetByIdAsync(id);
        }

        public async Task<AuditRequest> CreateAuditRequestAsync(AuditRequestDto auditRequestDto)
        {
            return await _auditRequestRepository.AddAsync(auditRequestDto);
        }

        public async Task<AuditRequest> UpdateAuditRequestAsync(int id, AuditRequestDto auditRequestDto)
        {
            return await _auditRequestRepository.UpdateAsync(id, auditRequestDto);
        }

        public async Task<bool> DeleteAuditRequestAsync(int id)
        {
            return await _auditRequestRepository.DeleteAsync(id);
        }
    }


}