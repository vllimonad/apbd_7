using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tutorial7.Repositories;

namespace tutorial7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController: ControllerBase
{

    private readonly IProductsRepository pRepository;
    private readonly IOrdersRepository oRepository;
    private readonly IWarehousesRepository wRepository;
    private readonly IProduct_WarehousesRepository pwRepository;

    public WarehouseController(IProductsRepository pRepository, IWarehousesRepository wRepository, IOrdersRepository oRepository, IProduct_WarehousesRepository pwRepository)
    {
        this.pRepository = pRepository;
        this.oRepository = oRepository;
        this.wRepository = wRepository;
        this.pwRepository = pwRepository;
    }

    [HttpGet]
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
            if (await pwRepository.DoesOrderExist(1))
            {
                return NotFound();
            }
        }
        return Ok();
    }
}