using pc2x.Paginacion.Core.ServiceInterfaces;
using pc2x.Paginacion.Repository;
using pc2x.Paginacion.Service;
using pc2x.Paginacion.WCF.Core.Contracts;
using pc2x.Paginacion.WCF.Core.Dtos;
using System;
using System.Linq;
using System.ServiceModel;

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
                var list = _facturaService.GetAll(pageSize, page);
                var total = _facturaService.Count();

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
                throw new FaultException(e.Message);
            }

        }
    }
}
