using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SF11_2019_POP2020.Models;
using SF11_2019_POP2020.MyExceptions;

namespace SF11_2019_POP2020.Services
{
    public class UserService : IUserService
    {
        public void deleteUser(string jmbg)
        {
            Korisnik k = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.Jmbg.Equals(jmbg));

            if (k == null)
                throw new UserNotFoundException($"Ne postoji korisnik sa jmbg-om {jmbg}");
            k.Aktivan = false;

            // updateUser(k);
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Korisnici
                                       
                                        SET Aktivan = @Aktivan
                                        where jmbg = @Jmbg";

                command.Parameters.Add(new SqlParameter("Aktivan", k.Aktivan));
                command.Parameters.Add(new SqlParameter("Jmbg", k.Jmbg));

                command.ExecuteNonQuery();

            }
        }

        public void readUsers()
        {
            Util.Instance.Korisnici = new ObservableCollection<Korisnik>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from korisnici";

            }


        }


        public int saveUser(Object obj)
        {
            Korisnik korisnik = obj as Korisnik;

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
                command.Parameters.Add(new SqlParameter("AdresaId", korisnik.AdresaId));
                command.Parameters.Add(new SqlParameter("Pol", korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("TipKorisnika", korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));

                return (int)command.ExecuteScalar();


            }

            // return -1;
        }

        public void updateUser(object obj)
        {
            Korisnik korisnik = obj as Korisnik;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                 conn.Open();
                 SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Korisnici
                                        SET Ime = @Ime
                                        SET Prezime = @Prezime
                                        SET Jmbg = @Jmbg
                                        SET Email = @Email
                                        SET AdresaId = @AdresaId
                                        SET Pol = @Pol
                                        SET Lozinka = @Lozinka
                                        SET TipKorisnika = @TipKorisnika
                                        SET Aktivan = @Aktivan
                                        where jmbg = @Jmbg";

                command.Parameters.Add(new SqlParameter("Ime", korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("Jmbg", korisnik.Jmbg));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.Parameters.Add(new SqlParameter("AdresaId", korisnik.AdresaId));
                command.Parameters.Add(new SqlParameter("Pol", korisnik.Pol));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("TipKorisnika", korisnik.TipKorisnika));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));
           


                command.ExecuteNonQuery();

            }
        }
    }
}