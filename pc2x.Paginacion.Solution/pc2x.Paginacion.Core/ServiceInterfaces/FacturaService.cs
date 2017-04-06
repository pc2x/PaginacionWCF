using pc2x.Paginacion.Core.DomainModels;
using System.Collections.Generic;

namespace pc2x.Paginacion.Core.ServiceInterfaces
{
    public interface IFacturaService
    {
        IEnumerable<FacturaModel> GetAll(int pageSize, int page);

        int Count();

        FacturaDetalleModel GetDetail(string folio);
    }
}
