﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    class Administrator : Korisnik
    {

        public string AdminZaUpisUFajl()
        {
            return KorisnickoIme + ";" + Ime + ";" + Prezime + ";" + JMBG + ";" +
                Email + ";" + Lozinka + ";" + Pol + ";" + TipKorisnika + ";" + Aktivan;
        }
    }
}
