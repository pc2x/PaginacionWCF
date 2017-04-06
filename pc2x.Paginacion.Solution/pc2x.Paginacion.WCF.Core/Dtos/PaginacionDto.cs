using System.Runtime.Serialization;

namespace pc2x.Paginacion.WCF.Core.Dtos
{
    [DataContract]
    public class PaginacionDto
    {
        [DataMember]
        public int TotalPages { get; set; }

        [DataMember]
        public int CurrrentPage { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public int PageSize { get; set; }
    }
}