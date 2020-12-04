using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF11_2019_POP2020.Models;
using SF11_2019_POP2020.MyExceptions;

namespace SF11_2019_POP2020.Services
{
    public class DoctorService : IUserService
    {
        public void deleteUser(string username)
        {
            
        }

        public void readUsers(string filename)
        {
            Util.Instance.Lekari = new ObservableCollection<Lekar>();
            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] lekarIzFajla = line.Split(';');

                    Korisnik korisnik = Util.Instance.Korisnici.ToList().Find(kori => kori.KorisnickoIme.Equals(lekarIzFajla[1]));
                    //Korisnik korisnik = NadjiKorisnika(lekarIzFajla[1]);
                    Lekar lekar = new Lekar
                    {
                        DomZdravlja = lekarIzFajla[0],
                        Korisnicko = korisnik
                    };

                    Util.Instance.Lekari.Add(lekar);
                }
            }
        }

        public void saveUsers(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Lekar lekar in Util.Instance.Lekari)
                {
                    file.WriteLine(lekar.LekarUpisUFajl());
                }
            }
        }
    }
}
