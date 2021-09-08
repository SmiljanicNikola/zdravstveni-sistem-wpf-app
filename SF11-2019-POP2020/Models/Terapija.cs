﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    [Serializable]

    public class Terapija
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _opis;

        public string Opis
        {
            get { return _opis; }
            set { _opis = value; }
        }

        private int _lekarId;

        public int LekarId
        {
            get { return _lekarId; }
            set { _lekarId = value; }
        }


        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }


        public Terapija()
        {

        }

        public Terapija Clone()
        {
            Terapija kopija = new Terapija();

            kopija.Opis = Opis;
            kopija.LekarId = LekarId;
            kopija.Aktivan = Aktivan;

            return kopija;
        }

    }
}
