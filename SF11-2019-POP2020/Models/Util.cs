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
        //IUserService _pacijentService;AAAAAAAAA
        ITerminService _terminService;
        ITerapijaService _terapijaService;
        ILekarService _lekarService;
        IPacijentService _pacijentService;
        

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

        public ObservableCollection<Termin> PrivatniTermini { get; set; }

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

            PrivatniTermini = new ObservableCollection<Termin>();

            Terapije = new ObservableCollection<Terapija>();
            //SviKorisnici = new ObservableCollection<Korisnik>();
            //Doktori = new ObservableCollection<Lekar>();


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
            else if (obj is Pacijent)
            {
                return _pacijentService.savePacijenta(obj);
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
            else if (filename.Contains("pacijenti"))
            {
                _pacijentService.readPacijente();

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
        public void DeletePacijent(int id)
        {
            _pacijentService.deletePacijenta(id);
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
                command.Parameters.Add(new SqlParameter("AdresaId", korisnik.Adresa.Id));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));



                command.ExecuteNonQuery();

            }
        }

        public int saveUser(Korisnik korisnik)
        {
            
            /*readAdrese();
            Util.Instance.Adrese = adrese;*/

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Korisnici(ime, prezime, jmbg, email, adresaId, pol, lozinka, TipKorisnika, aktivan)
                       output inserted.id VALUES(@Ime, @Prezime, @Jmbg, @Email, @AdresaId, @Pol, @Lozinka, @TipKorisnika, @Aktivan)";

                command.Parameters.Add(new SqlParameter("Ime", korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("Jmbg", korisnik.Jmbg));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.Parameters.Add(new SqlParameter("AdresaId", korisnik.Adresa.Id));
                command.Parameters.Add(new SqlParameter("Pol", korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("TipKorisnika", korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));

                return (int)command.ExecuteScalar();


            }

            // return -1;
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

        /*public ObservableCollection<Termin> terminiIzabranogLekara(int id)
        {
            ObservableCollection<Termin> terminiIzabranog = new ObservableCollection<Termin>();
            foreach(Termin termin in Termini)
            {
                if(termin.Lekar.Id == id)
                {
                    terminiIzabranog.Add(termin);
                }
            }
            return terminiIzabranog;
        }*/

        public ObservableCollection<Termin> terminiIzabranogLekara(Lekar lekar)
        {
            ObservableCollection<Termin> terminiIzabranog = new ObservableCollection<Termin>();
            foreach (Termin termin in Termini)
            {
                if (termin.Lekar == lekar)
                {
                    terminiIzabranog.Add(termin);
                }
            }
            return terminiIzabranog;
        }

        public ObservableCollection<Termin> nadjiTerminePoDatumu (string datum)
        {
            ObservableCollection<Termin> pronadjeniTermini = new ObservableCollection<Termin>();

            foreach(Termin ter in Termini)
            {
                if (ter.Datum.ToString().Contains(datum.ToLower())) pronadjeniTermini.Add(ter);
            }
            return pronadjeniTermini;
        }


        public ObservableCollection<Terapija> nadjiTerapijePoJmbgPacijenta(string jmbg)
        {
            ObservableCollection<Terapija> pronadjeneTerapije = new ObservableCollection<Terapija>();
            foreach(Terapija terapija in Terapije)
            {
                if (terapija.Pacijent.Korisnik.Jmbg.Equals(jmbg)) pronadjeneTerapije.Add(terapija);
            }
            return pronadjeneTerapije;
        }
        public ObservableCollection<Lekar> nadjiLekarePoInstituciji(string institucija)
        {
            ObservableCollection<Lekar> pronadjeniLekari = new ObservableCollection<Lekar>();
            foreach(Lekar lekar in Lekari)
            {
                if (lekar.DomZdravlja.NazivInstitucije.Contains(institucija.ToLower())) pronadjeniLekari.Add(lekar);
            }
            return pronadjeniLekari;
        }


        public ObservableCollection<Korisnik> nadjiKorisnikePoUlozi(string uloga)
        {
            ObservableCollection<Korisnik> pronadjeni = new ObservableCollection<Korisnik>();
            foreach(Korisnik kor in Korisnici)
            {
                if (kor.TipKorisnika.ToString().Equals(uloga)) pronadjeni.Add(kor);
            }
            return pronadjeni;
        }

        /*public ObservableCollection<Termin> terminiByLekarJmbg(string jmbg)
        {
            ObservableCollection<Termin> privatniTermini = new ObservableCollection<Termin>();
            //ObservableCollection<Lekar> Lekari = Util.Instance.Lekari;

            foreach (Termin t in Termini)
            {
                foreach(Lekar l in Lekari) {
                    if(t.Lekar == l)
                    {
                        if(l.Korisnik.Jmbg == jmbg)
                        {
                            privatniTermini.Add(t);
                        }
                    }
                }
               
            }
            return privatniTermini;
        }*/

        public ObservableCollection<Termin> terminiByLekarJmbg(string jmbg)
        {
            ObservableCollection<Termin> privatniTermini = new ObservableCollection<Termin>();
            ObservableCollection<Lekar> Lekari = Util.Instance.Lekari;

            foreach (Termin t in Termini)
            {
              
                    if (t.Lekar.Korisnik.Jmbg == jmbg)
                    {
                        privatniTermini.Add(t);
                    }
               
            }
            return privatniTermini;
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

        public Korisnik korisnikPoJmbg(string jmbg)
        {
            foreach (Korisnik korisnikk in Korisnici)
            {
                if (korisnikk.Jmbg.Equals(jmbg))
                {
                    return korisnikk;
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

        public Pacijent pacijentPoId(int id)
        {
            foreach(Pacijent pacijent in Pacijenti)
            {
                if(pacijent.Id == id)
                {
                    return pacijent;
                }
            }
            return null;
        }

        public Pacijent pacijentPoJmbg(string jmbg)
        {
            foreach (Pacijent pacijent in Pacijenti)
            {
                if (pacijent.Korisnik.Jmbg == jmbg)
                {
                    return pacijent;
                }
            }
            return null;
        }

        public DomZdravlja domZdravljaPoId(int id)
        {             


            foreach (DomZdravlja dz in DomoviZdravlja)
            {
                if (dz.Id == id)
                {
                    return dz;
                }
            }
            return null;
        }



    }
}
