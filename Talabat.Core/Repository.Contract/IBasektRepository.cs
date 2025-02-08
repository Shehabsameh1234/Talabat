using Talabat.Core.Entities;

namespace Talabat.Core.Repository.Contract
{
    public interface IBasektRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);

    }
}
