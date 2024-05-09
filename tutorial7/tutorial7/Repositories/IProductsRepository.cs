using System.Data.SqlTypes;

namespace tutorial7.Repositories;

public interface IProductsRepository
{
    Task<bool> DoesProductExist(int id);
    Task<SqlDecimal> GetProductPrice(int id);

}