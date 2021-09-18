using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Models
{
    [Serializable]
    public class Korisnik : IDataErrorInfo
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _ime;

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        private string _prezime;

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        private string _lozinka;

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _jmbg;

        public string Jmbg
        {
            get { return _jmbg; }
            set { _jmbg = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

        private EPol _pol;

        public EPol  Pol
        {
            get { return _pol; }
            set { _pol = value; }
        }

        private ETipKorisnika _tipKorisnika;

        public ETipKorisnika TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public string Error
        {
            get
            {
                return "Message!";
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "Ime":
                        if (string.IsNullOrEmpty(Ime))
                            return "Ime je obavezno da se unese!";
                        break;
                    case "Prezime":
                        if (string.IsNullOrEmpty(Prezime))
                            return "Prezime je obavezno da se unese!";
                        break;
                }
                return String.Empty;
            }
        }

        public Korisnik()
        {
            
        }

        public override string ToString()
        {
            return Ime + " " + Prezime + " | " + Jmbg;
        }


        public string KorisnikZaUpisUFajl()
        {
            return Ime + ";" + Prezime + ";" + Jmbg + ";" +
                Email + ";" + Lozinka + ";" + Pol + ";" + TipKorisnika+ ";" + Aktivan;
        }
     
        
        public Korisnik Clone()
        {
            Korisnik kopija = new Korisnik();

            kopija.Id = Id;
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.Jmbg = Jmbg;
            kopija.Email = Email;
            kopija.Adresa = Adresa;
            kopija.Pol = Pol;
            kopija.Lozinka = Lozinka;
            kopija.TipKorisnika = TipKorisnika;
            kopija.Aktivan = Aktivan;
          
            return kopija;
        }

       


        /* public static implicit operator Korisnik(Korisnik v)
         {
         }*/

        // public static implicit operator Korisnik(Korisnik v)
        //{
        //   throw new NotImplementedException();
        //}
    }
}
