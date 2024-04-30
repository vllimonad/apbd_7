namespace tutorial7.Repositories;

public interface IProductsRepository
{
    Task<bool> DoesProductExist(int id);

}