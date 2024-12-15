using HexAsset.Data;
using HexAsset.Models;
using HexAsset.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexAsset.Repositories
{
    public class AuditRequestRepository : IAuditRequestRepository
    {
        private readonly AppDbContext _dbContext;

        public AuditRequestRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AuditRequest>> GetAllAsync()
        {
            return await _dbContext.AuditRequests.ToListAsync();
        }

        public async Task<AuditRequest> GetByIdAsync(int id)
        {
            return await _dbContext.AuditRequests.FindAsync(id);
        }

        public async Task<AuditRequest> AddAsync(AuditRequestDto auditRequestDto)
        {
            var newAuditRequest = new AuditRequest
            {
                UserId = auditRequestDto.UserId,
                AuditStatus = auditRequestDto.AuditStatus,
                AuditDate = auditRequestDto.AuditDate,
            };

            _dbContext.AuditRequests.Add(newAuditRequest);
            await _dbContext.SaveChangesAsync();
            return newAuditRequest;
        }

        public async Task<AuditRequest> UpdateAsync(int id, AuditRequestDto auditRequestDto)
        {
            var auditRequest = await _dbContext.AuditRequests.FindAsync(id);
            if (auditRequest == null) return null;

            auditRequest.UserId = auditRequestDto.UserId;
            auditRequest.AuditStatus = auditRequestDto.AuditStatus;
            auditRequest.AuditDate = auditRequestDto.AuditDate;

            _dbContext.AuditRequests.Update(auditRequest);
            await _dbContext.SaveChangesAsync();
            return auditRequest;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var auditRequest = await _dbContext.AuditRequests.FindAsync(id);
            if (auditRequest == null) return false;

            _dbContext.AuditRequests.Remove(auditRequest);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
