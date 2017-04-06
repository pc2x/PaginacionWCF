using pc2x.Paginacion.Core.ServiceInterfaces;
using pc2x.Paginacion.Repository;
using pc2x.Paginacion.Service;
using pc2x.Paginacion.WCF.Core.Contracts;
using pc2x.Paginacion.WCF.Core.Dtos;
using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace pc2x.Paginacion.WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FacturacionService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FacturacionService.svc or FacturacionService.svc.cs at the Solution Explorer and start debugging.
    public class FacturacionService : IFacturacionService
    {
        private readonly IFacturaService _facturaService;
        public FacturacionService()
        {
            _facturaService = new FacturaService(new FacturaRepository());
        }
        public FacturasDto ObtenerFacturas(int pageSize, int page)
        {
            try
            {
                var t1 = Task.Run(() => _facturaService.GetAll(pageSize, page));
                var t2 = Task.Run(() => _facturaService.Count());

                Task.WhenAll(t1, t2);

                var list = t1.Result;
                var total = t2.Result;

                //here is where we can use AutoMapper
                return new FacturasDto
                {
                    Facturas = list.Select(m => new FacturaDto
                    {
                        Folio = m.Folio,
                        LugarExpedicion = m.LugarExpedicion,
                        FechaExpedicion = m.FechaExpedicion,
                        RfcEmisor = m.EmisorRfc,
                        RfcReceptor = m.ReceptorRfc,

                    }),

                    Paginacion = new PaginacionDto
                    {
                        CurrrentPage = page,
                        PageSize = pageSize,
                        TotalRecords = total,
                        TotalPages = (int)(total / pageSize)
                    }
                };
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException?.Message ?? e.Message);
            }

        }

        public FacturaDetalleDto ObtenerDetalleFactura(string folio)
        {
            try
            {
                var model = _facturaService.GetDetail(folio);

                //here is where we can use AutoMapper
                return new FacturaDetalleDto
                {
                    Folio = model.Folio,
                    FechaExpedicion = model.FechaExpedicion,
                    LugarExpedicion = model.LugarExpedicion,
                    Emisor = new ContribuyenteDto
                    {
                        Rfc = model.Emisor.Rfc,
                        Nombre = model.Emisor.Nombre,
                        Domicilio = model.Emisor.Domicilio

                    },
                    Receptor = new ContribuyenteDto
                    {
                        Rfc = model.Receptor.Rfc,
                        Nombre = model.Receptor.Nombre,
                        Domicilio = model.Receptor.Domicilio
                    },
                    Conceptos = model.Conceptos.Select(m => new ConceptoDto
                    {
                        Id = m.Id,
                        Importe = m.Importe,
                        Cantidad = m.Cantidad,
                        Descripcion = m.Descripcion
                    }),

                };
            }
            catch (Exception e)
            {
                throw new FaultException(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
