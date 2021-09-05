using DotNetCollege.EFCore.Sample13.BL.DTO;
using DotNetCollege.EFCore.Sample13.BL.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Linq;

namespace DotNetCollege.EFCore.Sample13.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CategoryFacade categoryFacade;
        private readonly ProductRepository productRepository;

        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        public IndexModel(ILogger<IndexModel> logger, CategoryFacade categoryFacade, ProductRepository productRepository)
        {
            _logger = logger;
            this.categoryFacade = categoryFacade;
            this.productRepository = productRepository;
        }

        public void OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Categories = categoryFacade.GetCategories();

                if (Categories.Any())
                {
                    var tenantCategory = Categories.FirstOrDefault();

                    for (int i = 0; i < 10; i++)
                    {
                        productRepository.AddProductForCategory(new ProductDTO() { Name = "New Milk Product Number " + i }, tenantCategory.Id);
                    }

                    //Same categories with new products
                    Categories = categoryFacade.GetCategories();
                    var tenantCategory2 = Categories.FirstOrDefault();
                    categoryFacade.AlterProductsInCategory(tenantCategory2);
                }
            }

        }
    }
}
