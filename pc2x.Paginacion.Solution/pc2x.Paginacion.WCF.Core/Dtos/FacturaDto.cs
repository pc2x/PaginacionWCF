using System;
using System.Runtime.Serialization;

namespace pc2x.Paginacion.WCF.Core.Dtos
{
    [DataContract]
    public class FacturaDto
    {
        [DataMember]
        public string Folio { get; set; }

        [DataMember]
        public string LugarExpedicion { get; set; }

        [DataMember]
        public DateTime FechaExpedicion { get; set; }

        [DataMember]
        public string RfcEmisor { get; set; }

        [DataMember]
        public string RfcReceptor { get; set; }

    }
}
