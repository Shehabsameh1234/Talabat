using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.APIs.Controllers;

    public class BasketController : BaseApiController
    {
    private readonly IBasektRepository _basketRepo;

    public BasketController(IBasektRepository basketRepo)
    {
        _basketRepo = basketRepo;
    }
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
    {
        var basket = await _basketRepo.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var CreatedOrUpdateBasket = await _basketRepo.UpdateBasketAsync(basket);
        if (CreatedOrUpdateBasket == null) return BadRequest(new ApisResponse(400));
        return Ok(CreatedOrUpdateBasket);
    }
    [HttpDelete]
    public async Task DeleteBasket(string id)
    {
        await _basketRepo.DeleteBasketAsync(id);
    }


}


  


