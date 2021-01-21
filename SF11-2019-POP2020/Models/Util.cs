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
        IAdresaService _adresaService;
        IUserService _adminService;
        IDomZdravljaService _domZdravljaService;
        IUserService _pacijentService;

        private Util()
        {
            _userService = new UserService();
            _doctorService = new DoctorService();
            _adresaService = new AdresaService();
            _adminService = new AdminService();
            _domZdravljaService = new DomZdravljaService();
            _pacijentService = new PacijentService();
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
        
        public ObservableCollection<Adresa> Adrese { get; set; }
        public ObservableCollection<Korisnik> KorisniciAdmini { get; set; }

        public ObservableCollection<DomZdravlja> DomoviZdravlja { get; set; }
        public ObservableCollection<Korisnik> KorisniciPacijenti { get; set; }


        public void Initialize()
        {
            Korisnici = new ObservableCollection<Korisnik>();
            Lekari = new ObservableCollection<Lekar>();
            Adrese = new ObservableCollection<Adresa>();
            KorisniciAdmini = new ObservableCollection<Korisnik>();
            DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            KorisniciPacijenti = new ObservableCollection<Korisnik>();



            /*Adresa adresa = new Adresa
            {
                Grad = "Grad 1",
                Broj = "Broj 1",
                Drzava = "Drzava 1",
                Ulica = "Ulica 1",
                Id = 1
            };*/

            /* Korisnik korisnik1 = new Korisnik();
             korisnik1.Ime = "petar";
             korisnik1.Prezime = "peric";
             korisnik1.Jmbg = "123456";
             korisnik1.Lozinka = "pera";
             korisnik1.Email = "pera@gmail.com";
             korisnik1.Pol = EPol.M;
             korisnik1.TipKorisnika = ETipKorisnika.ADMINISTRATOR;
             korisnik1.Aktivan = true;
             //korisnik1.Adresa = adresa;*/

            Korisnik korisnik2 = new Korisnik
            {
                Email = "zika@gmail.com",
                Ime = "zika",
                Prezime = "zikic",
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

            /*Korisnici.Add(korisnik1);*/
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
            
            else if(obj is Adresa)
            {
                return _adresaService.saveAdresa(obj);
            }
            return -1;
        }

        public void CitanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                _userService.readUsers();
                _adminService.readUsers();
                _pacijentService.readUsers();
            }
            else if (filename.Contains("lekari"))
            {
                _doctorService.readUsers();
            }
            else if (filename.Contains("adrese"))
            {
                _adresaService.readAdrese();
            }
            else if(filename.Contains("domovizdravlja"))
            {
                _domZdravljaService.readDomoveZdravlja();
            }
        }
    
        public void DeleteUser(string jmbg)
        {
            _userService.deleteUser(jmbg);
           
        }
        public void DeleteAdresa(int id)
        {
            _adresaService.deleteAdresa(id);
        }
        public void DeleteAdmin(string jmbg)
        {
            _adminService.deleteUser(jmbg);
        }
        public void DeletePacijent(string jmbg)
        {
            _pacijentService.deleteUser(jmbg);
        }

        public void UpdateEntiteta(Object obj)
        {
            if(obj is Korisnik)
            {
                _userService.updateUser(obj);
            }
            if(obj is Lekar)
            {
                _doctorService.updateUser(obj);
            }
        }

    }
}
