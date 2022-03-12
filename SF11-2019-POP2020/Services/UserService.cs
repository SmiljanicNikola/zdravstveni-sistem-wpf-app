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
        ObservableCollection<Adresa> adrese = new ObservableCollection<Adresa>();

        public void deleteUser(string jmbg)
        {
            Korisnik k = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.Jmbg.Equals(jmbg));

            if (k == null)
                throw new UserNotFoundException($"Ne postoji korisnik sa jmbg-om {jmbg}");
            k.Aktivan = false;

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


        public void readAdrese()
        {

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from adrese where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    adrese.Add(new Adresa
                    {
                        Id = reader.GetInt32(0),
                        Ulica = reader.GetString(1),
                        Broj = reader.GetString(2),
                        Grad = reader.GetString(3),
                        Drzava = reader.GetString(4),
                        Aktivan = reader.GetBoolean(5)
                    });
                }
                reader.Close();
            }
        }


        public void deleteUserZapravo(string jmbg)
        {
            throw new NotImplementedException();
        }


        public void deleteUserZapravo(int id)
        {
            Korisnik k = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.Id.Equals(id));

            if (k == null)
                throw new UserNotFoundException($"Ne postoji korisnik sa Id-om {id}");
            k.Aktivan = false;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"DELETE from dbo.Korisnici where id = @id";

                command.Parameters.Add(new SqlParameter("Aktivan", k.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", k.Id));

                command.ExecuteNonQuery();
            }
        }


        public void readUsers()
        {

            Util.Instance.Korisnici = new ObservableCollection<Korisnik>();
            readAdrese();
            Util.Instance.Adrese = adrese;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from korisnici where aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Korisnici.Add(new Korisnik
                    {
                        Id = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Jmbg = reader.GetString(3),
                        Email = reader.GetString(4),
                        Adresa = Util.Instance.adresaPoId(reader.GetInt32(5)),
                        Pol = (EPol)Enum.Parse(typeof(EPol), reader.GetString(6)),
                        Lozinka = reader.GetString(7),
                        TipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), reader.GetString(8)),
                        Aktivan = reader.GetBoolean(9)
                    });
                }
                reader.Close();
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
                command.Parameters.Add(new SqlParameter("AdresaId", korisnik.Adresa.Id));
                command.Parameters.Add(new SqlParameter("Pol", korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("TipKorisnika", korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));

                return (int)command.ExecuteScalar();
            }
        }

        
        public void updateUser(object obj)
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
    }
}