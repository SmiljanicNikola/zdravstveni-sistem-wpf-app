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

        ObservableCollection<Pacijent> pacijenti = new ObservableCollection<Pacijent>();
        ObservableCollection<Lekar> lekari = new ObservableCollection<Lekar>();
        public void deleteTermin(int id)
        {
            Termin t = Util.Instance.Termini.ToList().Find(Termin => Termin.Id.Equals(id));

            if (t == null)
                throw new UserNotFoundException($"Ne postoji Termin sa tim id-om {id}");
            t.Aktivan = false;

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


        public void readDoktore()
        {
            ObservableCollection<Lekar> lekari = new ObservableCollection<Lekar>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Lekari where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lekari.Add(new Lekar
                    {
                        Id = reader.GetInt32(0),
                        Korisnik = Util.Instance.korisnikPoId(reader.GetInt32(1)),
                        DomZdravlja = Util.Instance.domZdravljaPoId(reader.GetInt32(2)),
                        Termini = reader.GetString(3),
                        Aktivan = reader.GetBoolean(4)
                    });
                }
                reader.Close();
            }
        }


        public void readTermine()
        {
            Util.Instance.Termini = new ObservableCollection<Termin>();
            readPacijente();
            readDoktore();
            Util.Instance.Pacijenti = pacijenti;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Termini where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Termin termin = new Termin();
                    termin.Id = reader.GetInt32(0);
                    termin.Lekar = Util.Instance.lekarPoId(reader.GetInt32(1));
                    termin.Datum = reader.GetDateTime(2).Date;
                    termin.StatusTermina = (EStatusTermina)Enum.Parse(typeof(EStatusTermina), reader.GetString(3));
                    if (!reader.IsDBNull(4))
                    {

                        termin.Pacijent = Util.Instance.pacijentPoId(reader.GetInt32(4));
                    }
                    else
                    {
                        termin.Pacijent = null;
                    }
                    termin.Aktivan = reader.GetBoolean(5);
                    Util.Instance.Termini.Add(termin);
                }
                reader.Close();
            }
        }


        public void readPacijente()
        {
            Util.Instance.Pacijenti = new ObservableCollection<Pacijent>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Pacijenti where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pacijenti.Add(new Pacijent
                    {
                        Id = reader.GetInt32(0),
                        Korisnik = Util.Instance.korisnikPoId(reader.GetInt32(1)),
                        Termini = reader.GetString(2),
                        Aktivan = reader.GetBoolean(3)
                    });
                }
                reader.Close();
            }
        }


        public int saveTermin(Object obj)
        {
            Termin termin = obj as Termin;
            Util.Instance.Pacijenti = pacijenti;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Termini(LekarId, Datum, StatusTermina, Aktivan)
                       output inserted.id VALUES(@LekarId, @Datum, @StatusTermina, @Aktivan)";

                command.Parameters.Add(new SqlParameter("LekarId", termin.Lekar.Id));
                command.Parameters.Add(new SqlParameter("Datum", termin.Datum));
                command.Parameters.Add(new SqlParameter("StatusTermina", termin.StatusTermina.ToString()));
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

                command.Parameters.Add(new SqlParameter("LekarId", termin.Lekar.Id));
                command.Parameters.Add(new SqlParameter("Datum", termin.Datum));
                command.Parameters.Add(new SqlParameter("StatusTermina", termin.StatusTermina));
                if (termin.Pacijent == null)
                {
                    command.Parameters.Add(new SqlParameter("PacijentId", DBNull.Value));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("PacijentId", termin.Pacijent.Id));
                }
             
                command.Parameters.Add(new SqlParameter("Aktivan", termin.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", termin.Id));

                command.ExecuteNonQuery();
            }
        }
    }
}
