using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using tutorial7.Repositories;

namespace tutorial7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController: ControllerBase
{

    private readonly IProductsRepository pRepository;
    private readonly IOrdersRepository oRepository;
    private readonly IWarehousesRepository wRepository;
    private readonly IProduct_WarehousesRepository pwRepository;

    public WarehousesController(IProductsRepository pRepository, IWarehousesRepository wRepository, IOrdersRepository oRepository, IProduct_WarehousesRepository pwRepository)
    {
        this.pRepository = pRepository;
        this.oRepository = oRepository;
        this.wRepository = wRepository;
        this.pwRepository = pwRepository;
    }

    [HttpPost]
    public async Task<IActionResult> addProduct([Required] int IdProduct, [Required] int IdWarehouse, [Required] int Amount, DateTime CreatedAt)
    {
        if (!await pRepository.DoesProductExist(IdProduct))
        {
            return NotFound();
        }

        if (!await wRepository.DoesWarehouseExist(IdWarehouse))
        {
            return NotFound();
        }

        if (Amount < 0)
        {
            return NotFound();
        }

        var order = await oRepository.DoesProductPurchaseExist(IdProduct, Amount, CreatedAt);
        if (order.IdOrder != null)
        {
            Console.WriteLine(order.IdOrder);
            if (await pwRepository.DoesOrderExist(order.IdOrder))
            {
                return NotFound();
            }
            oRepository.updateTime(order.IdOrder, DateTime.Now);
            pwRepository.AddRecord(IdWarehouse, IdProduct, order.IdOrder, Amount, await pRepository.GetProductPrice(IdProduct));
        }

        return Ok(order);
    }
}