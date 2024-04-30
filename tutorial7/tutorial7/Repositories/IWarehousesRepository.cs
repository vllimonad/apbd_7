namespace tutorial7.Repositories;

public interface IWarehousesRepository
{
    Task<bool> DoesWarehouseExist(int id);
}