using System;

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
}
