using System;
using System.Collections.Generic;

namespace pc2x.Paginacion.Core.DomainModels
{
    public class FacturaDetalleModel
    {
        public FacturaDetalleModel()
        {
            Emisor = new ContribuyenteModel();
            Receptor = new ContribuyenteModel();
            Conceptos = new List<ConceptoModel>();
        }

        public int Id { get; set; }

        public string LugarExpedicion { get; set; }

        public DateTime FechaExpedicion { get; set; }

        public string Folio { get; set; }

        public ContribuyenteModel Emisor { get; set; }

        public ContribuyenteModel Receptor { get; set; }

        public IEnumerable<ConceptoModel> Conceptos { get; set; }

    }
}