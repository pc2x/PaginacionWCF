using System;
using System.Collections.Generic;

namespace pc2x.Paginacion.Core.DomainModels
{
    public class FacturaModel
    {
        public int Id { get; set; }
        public DateTime FechaExpedicion { get; set; }

        public string LugarExpedicion { get; set; }

        public string Folio { get; set; }

        public string EmisorRfc { get; set; }

        public string ReceptorRfc { get; set; }

    }

    public class FacturaDetalleModel
    {
        public int Id { get; set; }
        public DateTime FechaEmision { get; set; }

        public string LugarExpedicion { get; set; }

        public string Folio { get; set; }

        public ContribuyenteModel Emisor { get; set; }

        public ContribuyenteModel Receptor { get; set; }

        public IEnumerable<ConceptoModel> Conceptos { get; set; }

    }
}
