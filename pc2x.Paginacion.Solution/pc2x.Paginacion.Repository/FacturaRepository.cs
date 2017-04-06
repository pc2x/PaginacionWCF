using pc2x.Paginacion.Core.DomainModels;
using pc2x.Paginacion.Core.RepositoryInterfaces;
using pc2x.Paginacion.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace pc2x.Paginacion.Repository
{
    public class FacturaRepository : IFacturaRepository
    {

        private static SqlConnection GetConnection()
        {
            var cs = ConfigurationManager.AppSettings["connectionString"];
            return new SqlConnection(cs);
        }

        public IEnumerable<FacturaModel> GetAll(int pageSize, int page)
        {
            using (var conexion = GetConnection())
            {
                //parameter @pageSize
                var pageSizeParam = new SqlParameter("@pageSize", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = pageSize
                };


                //parameter @pageNumber
                var pageParam = new SqlParameter("@pageNumber", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = page
                };

                //create store command
                var command = new SqlCommand("[ej1].[usp_GetFacturas]", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //add parameters to store
                command.Parameters.Add(pageSizeParam);
                command.Parameters.Add(pageParam);


                //openconnection
                conexion.Open();

                //execute reader
                var reader = command.ExecuteReader();

                var lista = new List<FacturaModel>();

                if (!reader.HasRows)
                {
                    reader.Close();
                    return lista;
                }

                while (reader.Read())
                {
                    lista.Add(new FacturaModel
                    {
                        Id = Convert.ToInt32(reader.GetValue("Id")),
                        Folio = Convert.ToString(reader.GetValue("Folio")),
                        EmisorRfc = Convert.ToString(reader.GetValue("RFC_EMISOR")),
                        ReceptorRfc = Convert.ToString(reader.GetValue("RFC_RECEPTOR")),
                        LugarExpedicion = Convert.ToString(reader.GetValue("LugarExpedicion")),
                        FechaExpedicion = Convert.ToDateTime(reader.GetValue("FechaExpedicion"))

                    });
                }

                reader.Close();
                return lista;
            }
        }

        public int Count()
        {
            using (var conexion = GetConnection())
            {
                //create command
                var command = new SqlCommand("select count(*) from [ej1].[Facturas]", conexion);
                conexion.Open();

                return Convert.ToInt32(command.ExecuteScalar());

            }
        }
    }
}
