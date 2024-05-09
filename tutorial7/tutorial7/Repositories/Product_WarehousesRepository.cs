using System.Data.SqlClient;
using System.Data.SqlTypes;

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
    
    public async Task AddRecord(int IdWarehouse, int IdProduct, int IdOrder, int Amount, SqlDecimal Price)
    {
        var query = @"insert into Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                    "values(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, GETDATE());";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);
        command.Parameters.AddWithValue("@IdProduct", IdProduct);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);
        command.Parameters.AddWithValue("@Amount", Amount);
        command.Parameters.AddWithValue("@Price", Amount*Price);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}