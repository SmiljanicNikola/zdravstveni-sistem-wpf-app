using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    public class Pacijent
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Korisnik _korisnik;

        public Korisnik korisnik
        {
            get { return _korisnik; }
            set { _korisnik = value; }
        }


        private ObservableCollection<Terapija> _listaTerapija;

        public ObservableCollection<Terapija> ListaTerapija
        {
            get { return _listaTerapija; }
            set { _listaTerapija = value; }
        }

        public override string ToString()
        {
            return $"Ime: {this.korisnik.Ime} + Prezime: {this.korisnik.Prezime}";
        }




    }
}
