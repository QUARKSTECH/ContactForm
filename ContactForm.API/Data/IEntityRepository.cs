using System.Collections.Generic;
using System.Threading.Tasks;
using ContactForm.API.Models;

namespace ContactForm.API.Data
{
    public interface IEntityRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         void Edit<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<Enquiry>> GetAllEnquiries();
    }
}