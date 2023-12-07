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

            builder.MapGet("customers/{id}", (int id, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT * FROM Customers" +
                "WHERE Id = @CustomerId";

                var customer = await connection.QuerySingleOrDefaultAsync<Customers>(sql, new { CustomerId = id });

                return customer is not null ? Results.Ok(customer) : Results.NotFound();
            });

            builder.MapPost("customers", async (Customer customer, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = "INSERT INTO Customers (FirstName, LastName, Email, DateOfBirth)" +
                "VALUES (@FirstName, @LastName, @Email, @DateOfBirth)";

                await connection.ExecuteAsync(sql, customer);

                return Results.Ok(customer);
            });

        }
    }
}
