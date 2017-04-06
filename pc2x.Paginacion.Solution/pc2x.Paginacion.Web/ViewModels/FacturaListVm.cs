using System.Collections.Generic;

namespace pc2x.Paginacion.Web.ViewModels
{
    public class FacturaListVm
    {
        public FacturaListVm()
        {
            Facturas = new List<FacturaVm>();
            Paginacion = new PaginacionInfoVm();
        }

        public IEnumerable<FacturaVm> Facturas;

        public PaginacionInfoVm Paginacion { get; set; }
    }
}