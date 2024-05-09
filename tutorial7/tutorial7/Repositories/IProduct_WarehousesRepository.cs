using System.Data.SqlTypes;

namespace tutorial7.Repositories;

public interface IProduct_WarehousesRepository
{ 
    Task<bool> DoesOrderExist(int id);
    Task AddRecord(int IdWarehouse, int IdProduct, int IdOrder, int Amount, SqlDecimal Price);
}