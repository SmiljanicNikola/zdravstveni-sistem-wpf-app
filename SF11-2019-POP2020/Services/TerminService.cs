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
        public void readDoktore()
        {
            ObservableCollection<Lekar> lekari = new ObservableCollection<Lekar>();

            //Util.Instance.Lekari = new ObservableCollection<Lekar>();


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
            //Util.Instance.Lekari = lekari;
            //ObservableCollection<Lekar> Lekari = Util.Instance.Lekari;



            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Termini where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Termini.Add(new Termin
                    {
                        Id = reader.GetInt32(0),
                        Lekar = Util.Instance.lekarPoId(reader.GetInt32(1)),
                        Datum = reader.GetDateTime(2),              
                        StatusTermina = (EStatusTermina)Enum.Parse(typeof(EStatusTermina), reader.GetString(3)),
                        Pacijent = Util.Instance.pacijentPoId(reader.GetInt32(4)),
                        Aktivan = reader.GetBoolean(5)

                    });
                }
                reader.Close();

            }

        }

        public void readPacijente()
        {
            Util.Instance.Pacijenti = new ObservableCollection<Pacijent>();
            //Util.Instance.DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            //Util.Instance.Lekari = new ObservableCollection<Lekar>();


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
                        //ListaTerapija = new ObservableCollection<Terapija>(),
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
                command.CommandText = @"insert into dbo.Termini(LekarId, Datum, StatusTermina, PacijentId, Aktivan)
                       output inserted.id VALUES(@LekarId, @Datum, @StatusTermina, @PacijentId, @Aktivan)";

                command.Parameters.Add(new SqlParameter("LekarId", termin.Lekar.Id));
                command.Parameters.Add(new SqlParameter("Datum", termin.Datum));
                command.Parameters.Add(new SqlParameter("StatusTermina", termin.StatusTermina.ToString()));
                command.Parameters.Add(new SqlParameter("PacijentId", termin.Pacijent.Id));
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
                command.Parameters.Add(new SqlParameter("PacijentId", termin.Pacijent.Id));
                command.Parameters.Add(new SqlParameter("Aktivan", termin.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", termin.Id));



                command.ExecuteNonQuery();

            }
        }
    }
}
