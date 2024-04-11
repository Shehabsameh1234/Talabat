using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext storeContext)

        {
            if (storeContext.ProductBrands.Count()==0)
            {
                //1-get BrandsData (jsonFile Path)
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.Json");
                //2-Deserialize from json to list
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                //3-add to data base
                if (brands?.Count() > 0)
                {

                    foreach (var brand in brands)
                    {
                        storeContext.ProductBrands.Add(brand);
                    }
                    await storeContext.SaveChangesAsync();
                } 
            }
			if (storeContext.ProductCategories.Count() == 0)
			{
				//1-get BrandsData (jsonFile Path)
				var CategorysData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.Json");
				//2-Deserialize from json to list
				var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategorysData);

				//3-add to data base
				if (Categories?.Count() > 0)
				{

					foreach (var Category in Categories)
					{
						storeContext.ProductCategories.Add(Category);
					}
					await storeContext.SaveChangesAsync();
				}
			}
			if (storeContext.Products.Count() == 0)
			{
				//1-get BrandsData (jsonFile Path)
				var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.Json");
				//2-Deserialize from json to list
				var products = JsonSerializer.Deserialize<List<Product>>(productData);

				//3-add to data base
				if (products?.Count() > 0)
				{

					foreach (var product in products)
					{
						storeContext.Products.Add(product);
					}
					await storeContext.SaveChangesAsync();
				}
			}


		}
    }
}
