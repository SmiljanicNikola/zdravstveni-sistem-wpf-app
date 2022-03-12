using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    [Serializable]
    public class Dezurstvo
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

        private DateTime _pocetak;

        public DateTime Pocetak
        {
            get { return _pocetak; }
            set { _pocetak = value; }
        }

        private DateTime _kraj;

        public DateTime Kraj
        {
            get { return _kraj; }
            set { _kraj = value; }
        }


        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public Dezurstvo()
        {

        }

        public override string ToString()
        {
            return Pocetak + " " + Kraj;
        }
        public Dezurstvo Clone()
        {
            Dezurstvo kopija = new Dezurstvo();

            kopija.Lekar = Lekar;
            kopija.Pocetak = Pocetak;
            kopija.Kraj = Kraj;

            kopija.Aktivan = Aktivan;

            return kopija;
        }

    }
}
