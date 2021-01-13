using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
        public void deleteUser()
        {
            
        }

        public void readUsers()
        {
            Util.Instance.Korisnici = new ObservableCollection<Korisnik>();
            Util.Instance.Lekari = new ObservableCollection<Lekar>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from korisnici where TipKorisnika like 'Lekar'";

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
                        AdresaId = reader.GetInt32(5),
                        Pol = EPol.M,
                        Lozinka = reader.GetString(7),
                        TipKorisnika = ETipKorisnika.LEKAR,
                        Aktivan = reader.GetBoolean(9)



                    }) ; 
                }
                reader.Close();

            }
        }

                public int saveUser(Object obj)
            {

            Lekar lekar = obj as Lekar;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Lekari(Id, DomZdravljaId)
                       output inserted.id VALUES(@Id, @DomZdravljaId)";

                command.Parameters.Add(new SqlParameter("Id", lekar.Id));
                command.Parameters.Add(new SqlParameter("DomZdravljaId", lekar.DomZdravljaId));
              

                return (int)command.ExecuteScalar();


            }

        }
    }
}
