using System.Data.SqlClient;

namespace tutorial7.Repositories; 

public class ProductsRepository: IProductsRepository
{

    private readonly IConfiguration _configuration;

    public ProductsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesProductExist(int id)
    {
        var query = "SELECT 1 FROM Product WHERE IdProduct = @ID";

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