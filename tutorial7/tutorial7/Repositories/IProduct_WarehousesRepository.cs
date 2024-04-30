namespace tutorial7.Repositories;

public interface IProduct_WarehousesRepository
{ 
    Task<bool> DoesOrderExist(int id);
}