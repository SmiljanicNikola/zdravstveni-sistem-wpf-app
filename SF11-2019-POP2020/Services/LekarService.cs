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

    class LekarService : ILekarService
    {
        ObservableCollection<DomZdravlja> domovi = new ObservableCollection<DomZdravlja>();

        ObservableCollection<Adresa> adrese = new ObservableCollection<Adresa>();


        public void deleteDoktora(int id)
        {
            Lekar l = Util.Instance.Lekari.ToList().Find(lekar => lekar.Id == id);

            if (l == null)
            {
                throw new UserNotFoundException($"Ne postoji sa tim id-om");
            }
            l.Aktivan = false;

            using(SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Update dbo.Lekari
                                        SET Aktivan = @Aktivan
                                        where Id = @Id";

                command.Parameters.Add(new SqlParameter("Aktivan", l.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", l.Id));

                command.ExecuteNonQuery();


            }

        }

        public void deleteUserZapravo(int id)
        {
            throw new NotImplementedException();
        }

        public void readDoktore()
        {
            Util.Instance.Lekari = new ObservableCollection<Lekar>();

            readDomoveZdravlja();
            Util.Instance.DomoviZdravlja = domovi;
            //Util.Instance.Lekari = new ObservableCollection<Lekar>();


            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Lekari where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Lekari.Add(new Lekar
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

        public void readDomoveZdravlja()
        {
            Util.Instance.DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            readAdrese();
            Util.Instance.Adrese = adrese;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from DomoviZdravlja where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    domovi.Add(new DomZdravlja
                    {
                        Id = reader.GetInt32(0),
                        NazivInstitucije = reader.GetString(1),
                        Adresa = Util.Instance.adresaPoId(reader.GetInt32(2)),
                        Aktivan = reader.GetBoolean(3)
                    });
                }
                reader.Close();
            }
        }


        public void readAdrese()
        {

            Util.Instance.Adrese = new ObservableCollection<Adresa>();

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


        public int saveDoktora(object obj)
        {


                Lekar lekar = obj as Lekar;

                using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
                {
                    conn.Open();

                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = @"insert into dbo.Lekari(IdKorisnika, DomZdravljaId, Termini, Aktivan)
                       output inserted.id VALUES(@IdKorisnika, @DomZdravljaId, @Termini, @Aktivan)";

                    command.Parameters.Add(new SqlParameter("IdKorisnika", lekar.Korisnik.Id));
                    command.Parameters.Add(new SqlParameter("DomZdravljaId", lekar.DomZdravlja.Id));
                    command.Parameters.Add(new SqlParameter("Termini", "|"));
                    command.Parameters.Add(new SqlParameter("Aktivan", lekar.Aktivan));

                    return (int)command.ExecuteScalar();


                }

            }

        public void updateDoktora(object obj)
        {
            Lekar lekar = obj as Lekar;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Lekari SET DomZdravljaId = @DomZdravljaId, Aktivan = @Aktivan
                                        where Id = @Id";

                command.Parameters.Add(new SqlParameter("DomZdravljaId", lekar.DomZdravlja.Id));
                command.Parameters.Add(new SqlParameter("Aktivan", lekar.Aktivan));



                command.ExecuteNonQuery();

            }
        }
    }
}
