namespace pc2x.Paginacion.Web.ViewModels
{
    public class PaginacionInfoVm
    {
        public int TotalPages { get; set; }

        public int CurrrentPage { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public int PreviousPage => CurrrentPage - 1;

        public int NextPage => CurrrentPage + 1;
    }
}