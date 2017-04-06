using System;

namespace pc2x.Paginacion.Web.ViewModels
{
    public class FacturaVm
    {
        public string Folio { get; set; }

        public string LugarExpedicion { get; set; }

        public DateTime FechaExpedicion { get; set; }

        public string RfcEmisor { get; set; }

        public string RfcReceptor { get; set; }
    }
}