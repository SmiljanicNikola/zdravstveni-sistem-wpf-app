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
    class TerminService : ITerminService
    {
        public void deleteTermin(int id)
        {
            Termin t = Util.Instance.Termini.ToList().Find(Termin => Termin.Id.Equals(id));

            if (t == null)
                throw new UserNotFoundException($"Ne postoji Termin sa tim id-om {id}");
            t.Aktivan = false;

            // updateUser(k);
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Termini
                                       
                                        SET Aktivan = @Aktivan
                                        where Id = @Id";

                command.Parameters.Add(new SqlParameter("Aktivan", t.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", t.Id));

                command.ExecuteNonQuery();

            }
        }

        public void readTermine()
        {
            Util.Instance.Termini = new ObservableCollection<Termin>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from termini where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Termini.Add(new Termin
                    {
                        Id = reader.GetInt32(0),
                        LekarId = reader.GetInt32(1),
                        Datum = reader.GetDateTime(2),              
                        StatusTermina = EStatusTermina.SLOBODAN,
                        PacijentId = reader.GetInt32(4),
                        Aktivan = reader.GetBoolean(5)



                    });
                }
                reader.Close();

            }

        }

        public int saveTermin(Object obj)
        {
            Termin termin = obj as Termin;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Termini(lekarId, datum, StatusTermina, pacijentId, aktivan)
                       output inserted.id VALUES(@LekarId, @Datum, @StatusTermina, @PacijentId, @aktivan)";

                command.Parameters.Add(new SqlParameter("LekarId", termin.LekarId));
                command.Parameters.Add(new SqlParameter("Datum", termin.Datum));
                command.Parameters.Add(new SqlParameter("StatusTermina", termin.StatusTermina.ToString()));
                command.Parameters.Add(new SqlParameter("PacijentId", termin.PacijentId));
                command.Parameters.Add(new SqlParameter("Aktivan", termin.Aktivan));

                return (int)command.ExecuteScalar();


            }
        }

        public void updateTermin(Object obj)
        {
            Termin termin = obj as Termin;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Termini SET LekarId = @LekarId, Datum = @Datum, StatusTermina = @StatusTermina, PacijentId = @PacijentId, Aktivan = @Aktivan
                                        where Id = @Id";

                command.Parameters.Add(new SqlParameter("LekarId", termin.LekarId));
                command.Parameters.Add(new SqlParameter("Datum", termin.Datum));
                command.Parameters.Add(new SqlParameter("StatusTermina", termin.StatusTermina));
                command.Parameters.Add(new SqlParameter("PacijentId", termin.PacijentId));
                command.Parameters.Add(new SqlParameter("Aktivan", termin.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", termin.Id));



                command.ExecuteNonQuery();

            }
        }
    }
}
