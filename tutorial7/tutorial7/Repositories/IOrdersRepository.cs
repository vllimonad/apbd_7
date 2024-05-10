using tutorial7.Models.DTOs;

namespace tutorial7.Repositories;

public interface IOrdersRepository
{
    Task<int> DoesProductPurchaseExist(int id, int amount, DateTime time);
    Task updateTime(int id, DateTime time);
}