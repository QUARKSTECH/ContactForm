using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactForm.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactForm.API.Data
{
    public class EntityRepository : IEntityRepository
    {
        private readonly DataContext _context;
        public EntityRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Edit<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
       
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
        {
            var allEnquiries = await _context.Enquiries.ToListAsync();
            return allEnquiries;
        }

        public async Task<Tenant> GetTenant(int TenantId)
        {
            var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == TenantId);
            return tenant;
        }
    }
}