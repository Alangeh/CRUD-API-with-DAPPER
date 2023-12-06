using Dapper;
using Microsoft.Data.SqlClient;
using Web.Api.Models;
using Web.Api.Services;

namespace Web.Api.EndPoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("customers", async (SqlConnectionFactory sqlConnectionFactory) =>
            {

                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT * FROM Customers";

                var customers = await connection.QueryAsync<Customers>(sql);

                return Results.Ok(customers);
            });

        }
    }
}
