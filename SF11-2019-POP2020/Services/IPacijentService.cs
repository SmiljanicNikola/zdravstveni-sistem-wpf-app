using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    public interface IPacijentService
    {

        void readPacijente();
        int savePacijenta(Object obj);

        void updatePacijenta(Object obj);
        void deletePacijenta(int id);

        void deletePacijentaZapravo(int id);

    }
}
