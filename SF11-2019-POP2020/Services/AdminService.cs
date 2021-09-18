using SF11_2019_POP2020.Models;
using SF11_2019_POP2020.MyExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    class AdminService : IUserService
    {

        public void deleteUser(string jmbg)
        {
            Korisnik k = Util.Instance.KorisniciAdmini.ToList().Find(korisnik => korisnik.Jmbg.Equals(jmbg));

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

     

        public void deleteUserZapravo(int id)
        {
            throw new NotImplementedException();
        }

        public void readUsers()
        {

            Util.Instance.KorisniciAdmini = new ObservableCollection<Korisnik>();
            //Util.Instance.Lekari = new ObservableCollection<Lekar>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from korisnici where TipKorisnika like 'ADMINISTRATOR' and Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.KorisniciAdmini.Add(new Korisnik
                    {
                        Id = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Jmbg = reader.GetString(3),
                        Email = reader.GetString(4),
                        Adresa = Util.Instance.adresaPoId(reader.GetInt32(5)),
                        Pol = EPol.M,
                        Lozinka = reader.GetString(7),
                        TipKorisnika = ETipKorisnika.ADMINISTRATOR,
                        Aktivan = reader.GetBoolean(9)



                    });
                }
                reader.Close();

            }

        }
        public int saveUser(Object obj)
        {
            return -1;

        }

        public void updateUser(object obj)
        {
            throw new NotImplementedException();
        }

    }
 }

