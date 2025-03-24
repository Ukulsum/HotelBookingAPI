using Microsoft.Data.SqlClient;

namespace HotelBookingAPI.Extensions
{
    public static class DataReaderExtensions
    {
        public static T GetValueByColumn<T>(this SqlDataReader reader, string columnName)
        {
            //Getting Column Index
            //This line retrieves the zero-based
        }
    }
}
