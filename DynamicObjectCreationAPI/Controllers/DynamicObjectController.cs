using DynamicData.Abstract;
using DynamicData.Concrete;
using DynamicObjectCreationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Text.Json;

namespace DynamicObjectCreationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DynamicObjectController : ControllerBase
    {
        private readonly IDynamicTableRepository _dynamicTableRepository;

        private readonly IDynamicFieldRepository _dynamicFieldRepository;

        private readonly IDynamicProductRepository _dynamicProductRepository;

        private readonly IDynamicCategoryRepository _dynamicCategoryRepository;

        private readonly IDynamicSubCategoryRepository _dynamicSubCategoryRepository;

        private readonly IDynamicBrandRepository _dynamicBrandRepository;

        private readonly IDynamicOrderProductRepository _dynamicOrderProductRepository;

        private readonly IDynamicOrderRepository _dynamicOrderRepository;

        public DynamicObjectController(IDynamicTableRepository dynamicTableRepository, IDynamicFieldRepository dynamicFieldRepository, IDynamicProductRepository dynamicProductRepository, IDynamicCategoryRepository dynamicCategoryRepository, IDynamicSubCategoryRepository dynamicSubCategoryRepository, IDynamicBrandRepository dynamicBrandRepository, IDynamicOrderProductRepository dynamicOrderProductRepository, IDynamicOrderRepository dynamicOrderRepository)
        {
            _dynamicTableRepository = dynamicTableRepository;
            _dynamicFieldRepository = dynamicFieldRepository;
            _dynamicProductRepository = dynamicProductRepository;
            _dynamicCategoryRepository = dynamicCategoryRepository;
            _dynamicSubCategoryRepository = dynamicSubCategoryRepository;
            _dynamicBrandRepository = dynamicBrandRepository;
            _dynamicOrderProductRepository = dynamicOrderProductRepository;
            _dynamicOrderRepository = dynamicOrderRepository;
        }

        [HttpPost("CreateTable")]
        public async Task<IActionResult> CreateTable(Table table)
        {
            try
            {
                await _dynamicTableRepository.CreateTable(table);
                return Ok(await _dynamicTableRepository.Tables.OrderByDescending(p => p.Id).FirstOrDefaultAsync());
            }

            catch(MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPost("CreateField")]
        public async Task<IActionResult> CreateField(FieldViewModel field)
        {
            try
            {
                await _dynamicFieldRepository.CreateField(new Field { Id = field.Id, Name = field.Name, DataType = field.DataType, TableId = field.TableId });
                return Ok(await _dynamicFieldRepository.Fields.Include(f => f.Table).OrderByDescending(p => p.Id).FirstOrDefaultAsync());
            }
            
            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _dynamicProductRepository.Products.Include(p => p.SubCategory).ThenInclude(sc => sc.Category).Include(p => p.Brand).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetProductsBySubCategoryId/{subCategoryId}")]
        public async Task<IActionResult> GetProductsBySubCategoryId(int subCategoryId)
        {
            try
            {
                return Ok(await _dynamicProductRepository.Products.Where(p => p.SubCategoryId == subCategoryId).Include(p => p.SubCategory).ThenInclude(sc => sc.Category).Include(p => p.Brand).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetProductsByBrandId/{brandId}")]
        public async Task<IActionResult> GetProductsByBrandId(int brandId)
        {
            try
            {
                return Ok(await _dynamicProductRepository.Products.Where(p => p.BrandId == brandId).Include(p => p.SubCategory).ThenInclude(sc => sc.Category).Include(p => p.Brand).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetProduct/{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            try
            {
                return Ok(await _dynamicProductRepository.Products.Include(p => p.SubCategory).ThenInclude(sc => sc.Category).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == productId));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            try
            {
                await _dynamicProductRepository.CreateProduct(new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Ml1Name = model.Ml1Name,
                    Ml2Name = model.Ml2Name,
                    Price = model.Price,
                    Description = model.Description,
                    BrandId = model.BrandId,
                    SubCategoryId = model.SubCategoryId
                });

                return Ok(await _dynamicProductRepository.Products.OrderByDescending(p => p.Id).Include(p => p.SubCategory).ThenInclude(sc => sc.Category).Include(p => p.Brand).FirstOrDefaultAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPut("EditProduct")]
        public async Task<IActionResult> EditProduct(ProductViewModel model)
        {
            try
            {
                await _dynamicProductRepository.UpdateProduct(new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Ml1Name = model.Ml1Name,
                    Ml2Name = model.Ml2Name,
                    Price = model.Price,
                    Description = model.Description,
                    BrandId = model.BrandId,
                    SubCategoryId = model.SubCategoryId
                });

                return Ok(await _dynamicProductRepository.Products.Include(p => p.SubCategory).ThenInclude(sc => sc.Category).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == model.Id));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                await _dynamicProductRepository.DeleteProduct(productId);
                return NoContent();
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                return Ok(await _dynamicCategoryRepository.Categories.ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetCategory/{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            try
            {
                return Ok(await _dynamicCategoryRepository.Categories.FirstOrDefaultAsync(c => c.Id == categoryId));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(Category model)
        {
            try
            {
                await _dynamicCategoryRepository.CreateCategory(model);
                return Ok(await _dynamicCategoryRepository.Categories.OrderByDescending(c => c.Id).FirstOrDefaultAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPut("EditCategory")]
        public async Task<IActionResult> EditCategory(Category model)
        {
            try
            {
                await _dynamicCategoryRepository.UpdateCategory(new Category
                {
                    Id = model.Id,
                    Name = model.Name,
                    Ml1Name = model.Ml1Name,
                    Ml2Name = model.Ml2Name
                });

                return Ok(await _dynamicCategoryRepository.Categories.FirstOrDefaultAsync(c => c.Id == model.Id));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpDelete("DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                await _dynamicCategoryRepository.DeleteCategory(categoryId);
                return NoContent();
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetSubCategories")]
        public async Task<IActionResult> GetSubCategories()
        {
            try
            {
                return Ok(await _dynamicSubCategoryRepository.SubCategories.Include(sc => sc.Category).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetSubCategoriesByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(int categoryId)
        {
            try
            {
                return Ok(await _dynamicSubCategoryRepository.SubCategories.Where(sc => sc.CategoryId == categoryId).Include(sc => sc.Category).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetSubCategory/{subCategoryId}")]
        public async Task<IActionResult> GetSubCategory(int subCategoryId)
        {
            try
            {
                return Ok(await _dynamicSubCategoryRepository.SubCategories.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == subCategoryId));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPost("CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory(SubCategoryViewModel model)
        {
            try
            {
                await _dynamicSubCategoryRepository.CreateSubCategory(new SubCategory
                {
                    Id = model.Id,
                    Name = model.Name,
                    Ml1Name = model.Ml1Name,
                    Ml2Name = model.Ml2Name,
                    CategoryId = model.CategoryId
                });

                return Ok(await _dynamicSubCategoryRepository.SubCategories.OrderByDescending(s => s.Id).Include(s => s.Category).FirstOrDefaultAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPut("EditSubCategory")]
        public async Task<IActionResult> EditSubCategory(SubCategoryViewModel model)
        {
            try
            {
                await _dynamicSubCategoryRepository.UpdateSubCategory(new SubCategory
                {
                    Id = model.Id,
                    Name = model.Name,
                    Ml1Name = model.Ml1Name,
                    Ml2Name = model.Ml2Name,
                    CategoryId = model.CategoryId
                });

                return Ok(await _dynamicSubCategoryRepository.SubCategories.Include(s => s.Category).FirstOrDefaultAsync(sc => sc.Id == model.Id));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpDelete("DeleteSubCategory/{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategory(int subCategoryId)
        {
            try
            {
                await _dynamicSubCategoryRepository.DeleteSubCategory(subCategoryId);
                return NoContent();
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                return Ok(await _dynamicBrandRepository.Brands.ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetBrand/{brandId}")]
        public async Task<IActionResult> GetBrand(int brandId)
        {
            try
            {
                return Ok(await _dynamicBrandRepository.Brands.FirstOrDefaultAsync(b => b.Id == brandId));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPost("CreateBrand")]
        public async Task<IActionResult> CreateBrand(Brand model)
        {
            try
            {
                await _dynamicBrandRepository.CreateBrand(model);
                return Ok(await _dynamicBrandRepository.Brands.OrderByDescending(b => b.Id).FirstOrDefaultAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPut("EditBrand")]
        public async Task<IActionResult> EditBrand(Brand model)
        {
            try
            {
                await _dynamicBrandRepository.UpdateBrand(model);
                return Ok(await _dynamicBrandRepository.Brands.FirstOrDefaultAsync(b => b.Id == model.Id));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpDelete("DeleteBrand/{brandId}")]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            try
            {
                await _dynamicBrandRepository.DeleteBrand(brandId);
                return NoContent();
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                return Ok(await _dynamicOrderRepository.Orders.ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetOrder/{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            try
            {
                return Ok(await _dynamicOrderRepository.Orders.FirstOrDefaultAsync(o => o.Id == orderId));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(Order model)
        {
            try
            {
                await _dynamicOrderRepository.CreateOrder(model);
                return Ok(await _dynamicOrderRepository.Orders.OrderByDescending(o => o.Id).FirstOrDefaultAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPut("EditOrder")]
        public async Task<IActionResult> EditOrder(Order model)
        {
            try
            {
                await _dynamicOrderRepository.UpdateOrder(model);
                return Ok(await _dynamicOrderRepository.Orders.FirstOrDefaultAsync(o => o.Id == model.Id));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpDelete("DeleteOrder/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                await _dynamicOrderRepository.DeleteOrder(orderId);
                return NoContent();
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetOrderProducts")]
        public async Task<IActionResult> GetOrderProducts()
        {
            try
            {
                return Ok(await _dynamicOrderProductRepository.OrderProducts.Include(op => op.Order).Include(op => op.Product).Include(op => op.Product.Brand).Include(op => op.Product.SubCategory).ThenInclude(sc => sc.Category).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetOrderProductsByProductId/{productId}")]
        public async Task<IActionResult> GetOrderProductsByProductId(int productId)
        {
            try
            {
                return Ok(await _dynamicOrderProductRepository.OrderProducts.Where(op => op.ProductId == productId).Include(op => op.Order).Include(op => op.Product).Include(op => op.Product.Brand).Include(op => op.Product.SubCategory).ThenInclude(sc => sc.Category).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpGet("GetOrderProductsByOrderId/{orderId}")]
        public async Task<IActionResult> GetOrderProductsByOrderId(int orderId)
        {
            try
            {
                return Ok(await _dynamicOrderProductRepository.OrderProducts.Where(op => op.OrderId == orderId).Include(op => op.Order).Include(op => op.Product).Include(op => op.Product.Brand).Include(op => op.Product.SubCategory).ThenInclude(sc => sc.Category).ToListAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPost("CreateOrderProduct")]
        public async Task<IActionResult> CreateOrderProduct(OrderProductViewModel model)
        {
            try
            {
                await _dynamicOrderProductRepository.CreateOrderProduct(new OrderProduct
                {
                    Id = model.Id,
                    OrderId = model.OrderId,
                    ProductId = model.ProductId
                });

                return Ok(await _dynamicOrderProductRepository.OrderProducts.Include(op => op.Order).Include(op => op.Product).Include(op => op.Product.Brand).Include(op => op.Product.SubCategory).ThenInclude(sc => sc.Category).OrderByDescending(o => o.Id).FirstOrDefaultAsync());
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpPut("EditOrderProduct")]
        public async Task<IActionResult> EditOrderProduct(OrderProductViewModel model)
        {
            try
            {
                await _dynamicOrderProductRepository.UpdateOrderProduct(new OrderProduct
                {
                    Id = model.Id,
                    OrderId = model.OrderId,
                    ProductId = model.ProductId
                });

                return Ok(await _dynamicOrderProductRepository.OrderProducts.Include(op => op.Order).Include(op => op.Product).Include(op => op.Product.Brand).Include(op => op.Product.SubCategory).ThenInclude(sc => sc.Category).FirstOrDefaultAsync(op => op.Id == model.Id));
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }

        [HttpDelete("DeleteOrderProduct/{orderProductId}")]
        public async Task<IActionResult> DeleteOrderProduct(int orderProductId)
        {
            try
            {
                await _dynamicOrderProductRepository.DeleteOrderProduct(orderProductId);
                return NoContent();
            }

            catch (MySqlException)
            {
                return BadRequest("Error establishing your database connection. Please check your database connection.");
            }
        }
    }
}