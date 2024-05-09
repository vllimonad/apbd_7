using System.Data;
using System.Data.SqlClient;
using tutorial7.Models.DTOs;

namespace tutorial7.Repositories;

public class OrdersRepository: IOrdersRepository
{
    
    private readonly IConfiguration _configuration;

    public OrdersRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<OrderDTO> DoesProductPurchaseExist(int id, int amount, DateTime time)
    {
        var query = "select IdOrder from \"Order\" where IdProduct = @id and Amount = @amount and CreatedAt < @time;";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@time", time);

        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();

        if (!reader.HasRows) throw new Exception();
        
        var orderDTO = new OrderDTO()
        {
            IdOrder = reader.GetInt32("IdOrder")
        };
        return orderDTO;
    }

    public async Task updateTime(int id, DateTime time)
    {
        var query = "update \"Order\" set FulfilledAt = @time where IdOrder = @id";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@time", time);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
    
}