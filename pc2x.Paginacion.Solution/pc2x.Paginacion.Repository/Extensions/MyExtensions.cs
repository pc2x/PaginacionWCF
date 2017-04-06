using System.Data.SqlClient;

namespace pc2x.Paginacion.Repository.Extensions
{
    public static class MyExtensions
    {
        public static object GetValue(this SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetValue(dataReader.GetOrdinal(columnName));
        }
    }
}
