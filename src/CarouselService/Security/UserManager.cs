using CarouselService.Data;
using CarouselService.Data.Model;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Data.Entity;

namespace CarouselService.Security
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(IPrincipal user);
    }

    public class UserManager : IUserManager
    {
        public UserManager(ICarouselServiceContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(IPrincipal user) => await _context.Users
            .Include(x=>x.Tenant)
            .SingleAsync(x => x.Username == user.Identity.Name);

        protected readonly ICarouselServiceContext _context;
    }
}
