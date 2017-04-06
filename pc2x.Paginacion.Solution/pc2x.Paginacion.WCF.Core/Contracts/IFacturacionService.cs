using pc2x.Paginacion.WCF.Core.Dtos;
using System.ServiceModel;

namespace pc2x.Paginacion.WCF.Core.Contracts
{
    [ServiceContract]
    public interface IFacturacionService
    {
        [OperationContract]
        FacturasDto ObtenerFacturas(int pageSize, int page);
    }
}
