using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    [Serializable]
    public class DomZdravlja
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nazivInstitucije;

        public string NazivInstitucije
        {
            get { return _nazivInstitucije; }
            set { _nazivInstitucije = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

        private bool _Aktivan;

        public bool Aktivan
        {
            get { return _Aktivan; }
            set { _Aktivan = value; }
        }

        public DomZdravlja Clone()
        {
            DomZdravlja kopija = new DomZdravlja();

            kopija.NazivInstitucije = NazivInstitucije;
            kopija.Adresa = Adresa;
            kopija.Aktivan = Aktivan;

            return kopija;
        }


    }
}
