using System;
using System.Collections.Generic;

namespace pc2x.Paginacion.WCF.Core.Dtos
{
    public class FacturaDetalleDto
    {

        public string LugarExpedicion { get; set; }

        public DateTime FechaExpedicion { get; set; }

        public string Folio { get; set; }

        public ContribuyenteDto Emisor { get; set; }

        public ContribuyenteDto Receptor { get; set; }

        public IEnumerable<ConceptoDto> Conceptos { get; set; }
    }
}
