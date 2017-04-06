using System.Collections.Generic;
using System.Runtime.Serialization;

namespace pc2x.Paginacion.WCF.Core.Dtos
{
    [DataContract]
    public class FacturasDto
    {

        [DataMember]
        public IEnumerable<FacturaDto> Facturas;

        [DataMember]
        public PaginacionDto Paginacion { get; set; }
    }
}