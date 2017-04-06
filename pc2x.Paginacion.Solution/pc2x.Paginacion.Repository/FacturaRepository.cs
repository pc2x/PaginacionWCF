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

        public FacturaDetalleModel GetDetail(string folio)
        {
            var model = new FacturaDetalleModel();

            using (var conexion = GetConnection())
            {
                //create command
                var command = new SqlCommand("select * from [ej1].[tvf_GetFacturaDetalle](@folio)", conexion);
                var p = command.Parameters.Add("@folio", SqlDbType.VarChar);
                p.Value = folio;

                conexion.Open();

                //execute reader
                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();
                    return model;
                }

                if (reader.Read())
                    model = new FacturaDetalleModel
                    {
                        Id = Convert.ToInt32(reader.GetValue("Id")),
                        Folio = Convert.ToString(reader.GetValue("Folio")),
                        LugarExpedicion = Convert.ToString(reader.GetValue("LugarExpedicion")),
                        FechaExpedicion = Convert.ToDateTime(reader.GetValue("FechaExpedicion")),
                        Emisor = new ContribuyenteModel
                        {
                            Id = Convert.ToInt32(reader.GetValue("Emisor_Id")),
                            Rfc = Convert.ToString(reader.GetValue("Emisor_RFC")),
                            Nombre = Convert.ToString(reader.GetValue("Emisor_Nombre")),
                            Domicilio = Convert.ToString(reader.GetValue("Emisor_Domicilio"))
                        },
                        Receptor = new ContribuyenteModel
                        {
                            Id = Convert.ToInt32(reader.GetValue("Receptor_Id")),
                            Rfc = Convert.ToString(reader.GetValue("Receptor_RFC")),
                            Nombre = Convert.ToString(reader.GetValue("Receptor_Nombre")),
                            Domicilio = Convert.ToString(reader.GetValue("Receptor_Domicilio"))
                        },
                    };

                if (reader.Read())
                {
                    reader.Close();
                    throw new DataException("Se encontró más de una factura con el mismo folio.");

                }

                reader.Close();
            }

            model.Conceptos = GetConceptos(folio);

            return model;
        }

        private IEnumerable<ConceptoModel> GetConceptos(string folio)
        {
            var conceptos = new List<ConceptoModel>();

            using (var conexion = GetConnection())
            {
                var command = new SqlCommand("select * from [ej1].[tvf_GetFacturaConceptos](@folio)", conexion);
                var p = command.Parameters.Add("@folio", SqlDbType.VarChar);
                p.Value = folio;
                conexion.Open();

                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();
                    return conceptos;
                }

                while (reader.Read())
                {
                    conceptos.Add(new ConceptoModel
                    {
                        Id = Convert.ToInt32(reader.GetValue("Id")),
                        Descripcion = Convert.ToString(reader.GetValue("Descripcion")),
                        Importe = Convert.ToDecimal(reader.GetValue("Importe")),
                        Cantidad = Convert.ToInt32(reader.GetValue("Cantidad"))
                    });
                }

                reader.Close();
                return conceptos;
            }
        }
    }
}
