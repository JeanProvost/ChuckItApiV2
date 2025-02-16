using ChuckItApiV2.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}
