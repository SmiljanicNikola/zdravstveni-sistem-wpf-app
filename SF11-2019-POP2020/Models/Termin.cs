using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    [Serializable]
   public class Termin
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _lekarId;

        public int LekarId
        {
            get { return _lekarId; }
            set { _lekarId = value; }
        }


        private DateTime _datum;

        public DateTime Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        private EStatusTermina _statusTermina;

        public EStatusTermina StatusTermina
        {
            get { return _statusTermina; }
            set { _statusTermina = value; }
        }

        private int _pacijentId;

        public int PacijentId
        {
            get { return _pacijentId; }
            set { _pacijentId = value; }
        }


        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

      

        public Termin()
        {

        }


        public Termin Clone()
        {
            Termin kopijaTermin = new Termin();

            kopijaTermin.Id = Id;
            kopijaTermin.LekarId = LekarId;
            kopijaTermin.Datum = Datum;
            kopijaTermin.StatusTermina = StatusTermina;
            kopijaTermin.Aktivan = Aktivan;

            return kopijaTermin;
        }

        public override string ToString()
        {
            return "Id " + Id + " Lekar Id " + LekarId + "Datum " + Datum + " PacijentId " + PacijentId + "Status Termina" + StatusTermina;
        }

    }
}
