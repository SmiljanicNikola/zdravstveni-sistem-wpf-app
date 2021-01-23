﻿using System;
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

        private int _adresaId;

        public int adresaId
        {
            get { return _adresaId; }
            set { _adresaId = value; }
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
            kopija.adresaId = adresaId;
            kopija.Aktivan = Aktivan;

            return kopija;
        }


    }
}