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
    class TerapijaService : ITerapijaService
    {
        public void deleteTerapiju(int id)
        {
            Terapija terap = Util.Instance.Terapije.ToList().Find(Terapija => Terapija.Id.Equals(id));

            if (terap == null)
                throw new UserNotFoundException($"Ne postoji Terapija sa tim id-om {id}");
            terap.Aktivan = false;

            // updateUser(k);
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Terapije
                                       
                                        SET Aktivan = @Aktivan
                                        where id = @Id";

                command.Parameters.Add(new SqlParameter("Aktivan", terap.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", terap.Id));

                command.ExecuteNonQuery();

            }
        }



        public void readTerapije()
        {
            Util.Instance.Terapije = new ObservableCollection<Terapija>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Terapije where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Terapije.Add(new Terapija
                    {
                        Id = reader.GetInt32(0),
                        Opis = reader.GetString(1),
                        Lekar = Util.Instance.lekarPoId(reader.GetInt32(2)),
                        Pacijent = Util.Instance.pacijentPoId(reader.GetInt32(3)),
                        Aktivan = reader.GetBoolean(4)
                    });
                }
                reader.Close();
            }
        }



        public int saveTerapije(object obj)
        {
            Terapija terapija = obj as Terapija;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Terapije(Opis, LekarId, PacijentId, Aktivan)
                       output inserted.id VALUES(@Opis, @LekarId, @PacijentId, @Aktivan)";

                command.Parameters.Add(new SqlParameter("Opis", terapija.Opis));
                command.Parameters.Add(new SqlParameter("LekarId", terapija.Lekar.Id));
                command.Parameters.Add(new SqlParameter("PacijentId", terapija.Pacijent.Id));
                command.Parameters.Add(new SqlParameter("Aktivan", terapija.Aktivan));

                return (int)command.ExecuteScalar();


            }
        }



        public void updateTerapije(object obj)
        {

            Terapija terapija = obj as Terapija;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Terapije SET Opis = @Opis, LekarId = @LekarId, PacijentId = @PacijentId, Aktivan = @Aktivan                            
                                        where Id = @Id";

                command.Parameters.Add(new SqlParameter("Id", terapija.Id));
                command.Parameters.Add(new SqlParameter("Opis", terapija.Opis));
                command.Parameters.Add(new SqlParameter("LekarId", terapija.Lekar.Id));
                command.Parameters.Add(new SqlParameter("Pacijent", terapija.Pacijent.Id));
                command.Parameters.Add(new SqlParameter("Aktivan", terapija.Aktivan));



                command.ExecuteNonQuery();

            }


        }
    }
}
