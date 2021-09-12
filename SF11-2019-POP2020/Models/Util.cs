using SF11_2019_POP2020.Services;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

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
        //IUserService _doctorService;
        IAdresaService _adresaService;
        IUserService _adminService;
        IDomZdravljaService _domZdravljaService;
        IUserService _pacijentService;
        ITerminService _terminService;
        ITerapijaService _terapijaService;
        ILekarService _lekarService;
        

        private Util()
        {
            _userService = new UserService();
            _adresaService = new AdresaService();
            _adminService = new AdminService();
            _domZdravljaService = new DomZdravljaService();
            _pacijentService = new PacijentService();
            _terminService = new TerminService();
            _terapijaService = new TerapijaService();
            _lekarService = new LekarService();
            
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
        //public ObservableCollection<Korisnik> KorisniciPacijenti { get; set; }
        public ObservableCollection<Termin> Termini { get; set; }
        public ObservableCollection<Terapija> Terapije { get; set; }
        
        public ObservableCollection<Pacijent> Pacijenti { get; set; }
        public ObservableCollection<Lekar> Doktori { get; set; }

        public void Initialize()
        {
            Korisnici = new ObservableCollection<Korisnik>();
            Lekari = new ObservableCollection<Lekar>();
            Pacijenti = new ObservableCollection<Pacijent>();
            Adrese = new ObservableCollection<Adresa>();
            KorisniciAdmini = new ObservableCollection<Korisnik>();
            DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            //KorisniciPacijenti = new ObservableCollection<Korisnik>();
            Termini = new ObservableCollection<Termin>();
            Terapije = new ObservableCollection<Terapija>();
            //SviKorisnici = new ObservableCollection<Korisnik>();
            Doktori = new ObservableCollection<Lekar>();


        }

        public int SacuvajEntitet(Object obj)
        {
            if (obj is Korisnik)
            {
                return _userService.saveUser(obj);
            }
            else if (obj is Lekar)
            {
                return _lekarService.saveDoktora(obj);
                
            }

            else if (obj is Adresa)
            {
                return _adresaService.saveAdresa(obj);
            }
            else if (obj is Termin)
            {
                return _terminService.saveTermin(obj);
            }

            else if (obj is DomZdravlja)
            {
                return _domZdravljaService.saveDomoveZdravlja(obj);
            }
            else if (obj is Terapija)
            {
                return _terapijaService.saveTerapije(obj);
            }
          
            return -1;
        }

        public void CitanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                //_sviKorisniciService.readUsers();
                _userService.readUsers();
                /*_adminService.readUsers();
                _pacijentService.readUsers();*/
            }
            else if (filename.Contains("lekari"))
            {
                _lekarService.readDoktore();
                
            }
            else if (filename.Contains("adrese"))
            {
                _adresaService.readAdrese();
            }
            else if (filename.Contains("domovizdravlja"))
            {
                _domZdravljaService.readDomoveZdravlja();
            }
            else if (filename.Contains("termini"))
            {
                _terminService.readTermine();
            }
            else if (filename.Contains("terapije"))
            {
                _terapijaService.readTerapije();
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
        public void DeleteTermin(int id)
        {
            _terminService.deleteTermin(id);
        }
        public void DeleteTerapija(int id)
        {
            _terapijaService.deleteTerapiju(id);
        }
        public void DeleteDomZdravlja(int id)
        {
            _domZdravljaService.deleteDomoveZdravlja(id);
        }
        public void DeleteUserZapravo(int id)
        {
            _userService.deleteUserZapravo(id);
        }
        public void DeleteDoktora(int id)
        {
            _lekarService.deleteDoktora(id);
        }

        public void UpdateEntiteta(Object obj)
        {
            if (obj is Korisnik)
            {
                _userService.updateUser(obj);
            }
            if (obj is Lekar)
            {
                _lekarService.updateDoktora(obj);
            }
            if (obj is Termin)
            {
                _terminService.updateTermin(obj);
            }
            if (obj is DomZdravlja)
            {
                _domZdravljaService.updateDomoveZdravlja(obj);
            }
            if (obj is Adresa)
            {
                _adresaService.updateAdresa(obj);
            }
            if (obj is Terapija)
            {
                _terapijaService.updateTerapije(obj);
            }
        }


        public void updateLicnihPodataka(object obj)
        {
            Korisnik korisnik = obj as Korisnik;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Korisnici SET Ime = @Ime, Prezime = @Prezime, Jmbg = @Jmbg, Email = @Email, AdresaId = @AdresaId, Lozinka = @Lozinka, Aktivan = @Aktivan
                                        where Jmbg = @Jmbg";

                command.Parameters.Add(new SqlParameter("Ime", korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("Jmbg", korisnik.Jmbg));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.Parameters.Add(new SqlParameter("AdresaId", korisnik.AdresaId));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));



                command.ExecuteNonQuery();

            }
        }

        public Korisnik nadjiUlogovanog(string jmbg)
        {
            foreach (Korisnik kor in Korisnici)
            {
                    if (kor.Jmbg == jmbg)
                    {
                        return kor;
                    }
               
            }

            return null;
        }


        public ObservableCollection<DomZdravlja> nadjiDomovePoMestu(string grad)
        {
            ObservableCollection<DomZdravlja> domovi = new ObservableCollection<DomZdravlja>();

            foreach (DomZdravlja dz in DomoviZdravlja)
            {
                if (dz.Adresa.Grad.ToLower().Contains(grad.ToLower())) domovi.Add(dz);
            }

            return domovi;
        }

        /*public ObservableCollection<Terapija> nadjiTerapijePoLekaru(string lekar)
        {
            ObservableCollection<Terapija> terapije = new ObservableCollection<Terapija>();

            foreach(Terapija ter in Terapije)
            {
                if ((ter.Lekar.Korisnik.Ime + ter.Lekar.Korisnik.Prezime).ToLower().Contains(lekar.ToLower())) terapije.Add(ter);
            }
            return terapije;
        }*/

        /*public DomZdravlja nadjiDomovePoMestu(string grad)
        {
            foreach (DomZdravlja dz in DomoviZdravlja)
            {
                if(dz.Adresa.Grad == grad)
                {
                    return dz;
                }
            }
            return null;

        }*/

        public Adresa adresaPoId(int id)
        {
            foreach(Adresa adr in Adrese)
            {
                if(adr.Id == id)
                {
                    return adr;
                }
            }
            return null;
        }

        public Korisnik korisnikPoId(int id)
        {
            foreach (Korisnik korisnik in Korisnici)
            {
                if (korisnik.Id == id)
                {
                    return korisnik;
                }
            }
            return null;
        }

        public Lekar lekarPoId(int id)
        {
            foreach(Lekar lekar in Lekari)
            {
                if(lekar.Id == id)
                {
                    return lekar;
                }
            }
            return null;
        }

        public DomZdravlja domZdravljaPoId(int id)
        {

            foreach (DomZdravlja domZdravlja in DomoviZdravlja)
            {
                if (domZdravlja.Id == id)
                {
                    return domZdravlja;
                }
            }
            return null;
        }



    }
}
