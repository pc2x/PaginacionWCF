using pc2x.Paginacion.Core.DomainModels;
using System.Collections.Generic;

namespace pc2x.Paginacion.Core.RepositoryInterfaces
{
    public interface IFacturaRepository
    {
        IEnumerable<FacturaModel> GetAll(int pageSize, int page);
        int Count();
    }
}
