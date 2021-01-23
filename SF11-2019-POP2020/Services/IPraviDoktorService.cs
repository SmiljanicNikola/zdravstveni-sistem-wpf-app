using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    public interface IPraviDoktorService
    {


        void readDoktore();
        int saveDoktora(Object obj);

        void updateDoktora(Object obj);
        void deleteDoktora(string jmbg);

        void deleteUserZapravo(int id);
    }
}
