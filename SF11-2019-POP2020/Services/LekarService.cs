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
            Util.Instance.DomoviZdravlja = new ObservableCollection<DomZdravlja>();
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
