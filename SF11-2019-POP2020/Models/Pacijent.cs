using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    [Serializable]
    public class Pacijent
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
            get { return _korisnik; }
            set { _korisnik = value; }
        }

        private string _termini;

        public string Termini
        {
            get { return _termini; }
            set { _termini = value; }
        }

        private ObservableCollection<Terapija> _listaTerapija;

        public ObservableCollection<Terapija> ListaTerapija
        {
            get { return _listaTerapija; }
            set { _listaTerapija = value; }
        }


        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public Pacijent Clone()
        {
            Pacijent kopija = new Pacijent();

            kopija.Korisnik = Korisnik;
            kopija.Termini = Termini;
            kopija.Aktivan = Aktivan;

            return kopija;

        }
        public override string ToString()
        {
            return Korisnik.Ime + " " + Korisnik.Prezime;
        }
    }
}
