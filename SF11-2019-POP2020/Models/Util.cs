using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SF11_2019_POP2020.Services;

namespace SF11_2019_POP2020.Models
{
    public sealed class Util
    {


        public static string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;
                                                    Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                                    TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                                    MultiSubnetFailover=False";
        private static readonly Util instance = new Util();
        IUserService _userService;
        IUserService _doctorService;

        private Util()
        {
            _userService = new UserService();
            _doctorService = new DoctorService();
        }
        static Util()
        {
            
        }

        public static Util Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<Korisnik> Korisnici { get; set; }
        public ObservableCollection<Lekar> Lekari { get; set; }

        public void Initialize()
        {
            Korisnici = new ObservableCollection<Korisnik>();
            Lekari = new ObservableCollection<Lekar>();

            Adresa adresa = new Adresa
            {
                Grad = "Grad 1",
                Broj = "Broj 1",
                Drzava = "Drzava 1",
                Ulica = "Ulica 1",
                ID = "1"
            };

            Korisnik korisnik1 = new Korisnik();
            korisnik1.KorisnickoIme = "pera";
            korisnik1.Ime = "petar";
            korisnik1.Prezime = "peric";
            korisnik1.Jmbg = "123456";
            korisnik1.Lozinka = "pera";
            korisnik1.Email = "pera@gmail.com";
            korisnik1.Pol = EPol.M;
            korisnik1.TipKorisnika = ETipKorisnika.ADMINISTRATOR;
            korisnik1.Aktivan = true;
            //korisnik1.Adresa = adresa;

            Korisnik korisnik2 = new Korisnik
            {
                Email = "zika@gmail.com",
                Ime = "zika",
                Prezime = "zikic",
                KorisnickoIme = "ziza",
                Jmbg = "654321",
                Lozinka = "zika",
                Pol = EPol.Z,
                TipKorisnika = ETipKorisnika.LEKAR,
                //Adresa = adresa
            };

            Lekar lekar = new Lekar
            {
                Id = korisnik2.Id,
                DomZdravljaId = 4
         
            };

            Korisnici.Add(korisnik1);
            Korisnici.Add(korisnik2);

            Lekari = new ObservableCollection<Lekar>
            {
                lekar
            };

        }

        public int SacuvajEntitet(Object obj)
        {
            if(obj is Korisnik)
            {
              return _userService.saveUser(obj);
            }
            else if(obj is Lekar)
            {
              return _doctorService.saveUser(obj);
            }
            return -1;
        }

        public void CitanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                _userService.readUsers();
            }
            else if (filename.Contains("lekari"))
            {
                _doctorService.readUsers();
            }
        }
    
        public void DeleteUser(string username)
        {
            _userService.deleteUser();
        }
    }
}
