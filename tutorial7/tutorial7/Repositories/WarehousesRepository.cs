using System.Data.SqlClient;

namespace tutorial7.Repositories;

public class WarehousesRepository: IWarehousesRepository
{
    
    private readonly IConfiguration _configuration;

    public WarehousesRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<bool> DoesWarehouseExist(int id)
    {
        var query = "select 1 from Warehouse where IdWarehouse = @id";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);

        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();
        return result is not null;
    }
}