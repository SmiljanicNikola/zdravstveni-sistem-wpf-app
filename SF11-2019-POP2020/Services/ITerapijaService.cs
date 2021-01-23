using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
   public interface ITerapijaService
    {
        void readTerapije();
        int saveTerapije(Object obj);

        void updateTerapije(Object obj);
        void deleteTerapiju(int id);

    }
}
