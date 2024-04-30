using tutorial7.Models.DTOs;

namespace tutorial7.Repositories;

public interface IOrdersRepository
{
    Task<OrderDTO> DoesProductPurchaseExist(int id, int amount, DateTime time);
}