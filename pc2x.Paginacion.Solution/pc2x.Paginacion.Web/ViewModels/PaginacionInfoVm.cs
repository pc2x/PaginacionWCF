namespace pc2x.Paginacion.Web.ViewModels
{
    public class PaginacionInfoVm
    {
        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public int PreviousPage => CurrentPage - 1;

        public int NextPage => CurrentPage + 1;
    }
}