using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    [Serializable]
    public class Lekar

    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Korisnik _korisnik;
        public Korisnik Korisnik
        {
            get { return _korisnik;  }
            set { _korisnik = value; }
        }

        private DomZdravlja _domZdravlja;

        public DomZdravlja DomZdravlja
        {
            get { return _domZdravlja; }
            set { _domZdravlja = value; }
        }

        private string _termini;

        public string Termini
        {
            get { return _termini; }
            set { _termini = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }


        public Lekar Clone()
        {
            Lekar kopija = new Lekar();

            kopija.Korisnik = Korisnik;
            kopija.DomZdravlja = DomZdravlja;
            kopija.Aktivan = Aktivan;

            return kopija;
        }

        public override string ToString()
        {
            return Korisnik.Ime + " " + Korisnik.Prezime;
        }

    }
}
