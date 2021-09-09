﻿using System;
using System.Collections.Generic;
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

        private Korisnik _idKorisnika;
        public Korisnik IdKorisnika
        {
            get { return _idKorisnika;  }
            set { _idKorisnika = value; }
        }

        private int _domZdravljaId;

        public int DomZdravljaId
        {
            get { return _domZdravljaId; }
            set { _domZdravljaId = value; }
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



    }
}
