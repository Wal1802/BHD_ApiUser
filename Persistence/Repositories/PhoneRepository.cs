using BHD.Application.Repositories;
using BHD.Domain.Models;

namespace BHD.Persistence.Repositories
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        public PhoneRepository(BHDContext context): base(context)
        {
            
        }
    }
}
