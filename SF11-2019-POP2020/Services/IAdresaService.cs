using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    public interface IAdresaService
    {
        void readAdrese();

        int saveAdresa(Object obj);

        void updateAdresa(Object obj);

        void deleteAdresa(int id);
    }
}
