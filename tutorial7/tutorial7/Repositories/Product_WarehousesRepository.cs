using System.Data.SqlClient;

namespace tutorial7.Repositories;

public class Product_WarehousesRepository: IProduct_WarehousesRepository
{
    private readonly IConfiguration _configuration;

    public Product_WarehousesRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesOrderExist(int id)
    {
        var query = "SELECT 1 FROM Product_Warehouse WHERE IdOrder = @ID";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();
        return result is not null;
    }
}