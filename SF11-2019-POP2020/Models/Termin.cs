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

        private Lekar _lekar;

        public Lekar Lekar
        {
            get { return _lekar; }
            set { _lekar = value; }
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

        private Pacijent _pacijent;

        public Pacijent Pacijent
        {
            get { return _pacijent; }
            set { _pacijent = value; }
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
            kopijaTermin.Lekar = Lekar;
            kopijaTermin.Datum = Datum;
            kopijaTermin.StatusTermina = StatusTermina;
            kopijaTermin.Pacijent = Pacijent;
            kopijaTermin.Aktivan = Aktivan;

            return kopijaTermin;
        }
    }
}
