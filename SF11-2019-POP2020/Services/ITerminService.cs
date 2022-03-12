using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    public interface ITerminService
    {
        void readTermine();

        int saveTermin(Object obj);

        void updateTermin(Object obj);

        void deleteTermin(int id);

    }
}
