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
        private string _korisnickoIme;


        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set {
                //if (value != null)
                //{
                    //if(Util.Instance.Korisnici.ToList().Exists(k=>k.KorisnickoIme.Equals(value)))
                   // {
                        //throw new ArgumentException("Korisnicko ime mora biti jedinstveno!");
                    //}
               // }

                _korisnickoIme = value; }

            
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

        private int _adresaId;

        public int AdresaId
        {
            get { return _adresaId; }
            set { _adresaId = value; }
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
            return "Ja sam " + KorisnickoIme + ". Moje email je:" + Email + ". Tip: " + TipKorisnika; // + ". Moja adresa je " + Adresa.ToString();
        }

        public string KorisnikZaUpisUFajl()
        {
            return KorisnickoIme + ";" + Ime + ";" + Prezime + ";" + Jmbg + ";" +
                Email + ";" + Lozinka + ";" + Pol + ";" + TipKorisnika+ ";" + Aktivan;
        }
     

        public Korisnik Clone()
        {
            Korisnik kopija = new Korisnik();

            kopija.AdresaId = AdresaId;
            kopija.Aktivan = Aktivan;
            kopija.Email = Email;
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.Pol = Pol;
            kopija.Lozinka = Lozinka;
            kopija.Jmbg = Jmbg;
            kopija.KorisnickoIme = KorisnickoIme;

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
