using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    public interface IDomZdravljaService
    {
        void readDomoveZdravlja();

        int saveDomoveZdravlja(Object obj);

        void updateDomoveZdravlja(Object obj);

        void deleteDomoveZdravlja(int id);

    }
}
