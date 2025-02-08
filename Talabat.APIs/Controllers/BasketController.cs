using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.APIs.Controllers;

    public class BasketController : BaseApiController
    {
    private readonly IBasektRepository _basketRepo;
    private readonly IMapper _mapper;

    public BasketController(IBasektRepository basketRepo,IMapper mapper)
    {
        _basketRepo = basketRepo;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
    {
        var basket = await _basketRepo.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
    {
        var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
        var CreatedOrUpdateBasket = await _basketRepo.UpdateBasketAsync(mappedBasket);
        if (CreatedOrUpdateBasket == null) return BadRequest(new ApisResponse(400));
        return Ok(CreatedOrUpdateBasket);
    }
    [HttpDelete]
    public async Task DeleteBasket(string id)
    {
        await _basketRepo.DeleteBasketAsync(id);
    }


}


  


