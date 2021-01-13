using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    public interface IUserService
    {
        void readUsers();
        int saveUser(Object obj);

        void deleteUser();
    }
}
