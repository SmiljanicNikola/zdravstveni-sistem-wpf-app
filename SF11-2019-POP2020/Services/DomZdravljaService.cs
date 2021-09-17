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
    class DomZdravljaService : IDomZdravljaService
    {
        public void deleteDomoveZdravlja(int id)
        {
            DomZdravlja dz = Util.Instance.DomoviZdravlja.ToList().Find(DomZdravlja => DomZdravlja.Id.Equals(id));

            if (dz == null)
                throw new UserNotFoundException($"Ne postoji Dom Zdravlja sa tim id-om {id}");
            dz.Aktivan = false;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.DomoviZdravlja
                                        SET Aktivan = @Aktivan
                                        where id = @Id";

                command.Parameters.Add(new SqlParameter("Aktivan", dz.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", dz.Id));

                command.ExecuteNonQuery();

            }
        }

        public void readDomoveZdravlja()
        {
            Util.Instance.DomoviZdravlja = new ObservableCollection<DomZdravlja>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from DomoviZdravlja where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.DomoviZdravlja.Add(new DomZdravlja
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

            public int saveDomoveZdravlja(object obj)
            {
            DomZdravlja domZdravlja = obj as DomZdravlja;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.DomoviZdravlja(NazivInstitucije, adresaId, aktivan)
                       output inserted.id VALUES(@nazivInstitucije, @adresaId, @aktivan)";

                command.Parameters.Add(new SqlParameter("nazivInstitucije", domZdravlja.NazivInstitucije));
                command.Parameters.Add(new SqlParameter("adresaId", domZdravlja.Adresa.Id));
                command.Parameters.Add(new SqlParameter("Aktivan", domZdravlja.Aktivan));

                return (int)command.ExecuteScalar();


            }
        }

        public void updateDomoveZdravlja(object obj)
        {
            DomZdravlja domZdravlja = obj as DomZdravlja;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.DomoviZdravlja
                                        SET NazivInstitucije = @NazivInstitucije, AdresaId = @AdresaId, Aktivan = @Aktivan
                                        where Id = @Id";

                command.Parameters.Add(new SqlParameter("NazivInstitucije", domZdravlja.NazivInstitucije));
                command.Parameters.Add(new SqlParameter("AdresaId", domZdravlja.Adresa.Id));
                command.Parameters.Add(new SqlParameter("Aktivan", domZdravlja.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", domZdravlja.Id));


                command.ExecuteNonQuery();

            }
        }
    }
}
