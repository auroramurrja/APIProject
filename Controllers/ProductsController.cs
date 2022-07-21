﻿using APIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Controllers
{
    [ApiVersion("1.0")]
    //[Route("api/[controller]")]
    //[Route("v{v:apiVersion}/products")]
    [Route("products")]
    [ApiController]
    public class ProductsV1Controller : ControllerBase
    {
        private readonly ShopContext _shopContext;
        public ProductsV1Controller(ShopContext context)
        {
            _shopContext = context;
            //to ensure the db is created
            _shopContext.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _shopContext.Products;
            if(queryParameters.MinPrice != null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value
                    );
            }
            if(queryParameters.MaxPrice != null)
            {
                products = products.Where(
                    p=>p.Price <=queryParameters.MaxPrice.Value
                    );
            }
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                products = products.Where(x => (
                x.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower()) ||
                x.Sku.ToLower().Contains(queryParameters.SearchTerm.ToLower()))
                );
            }
            if(!String.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(
                    
                    p=>p.Sku.ToLower().Contains(queryParameters.Sku.ToLower()));
            }
            if(!String.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(
                    p=>p.Name.ToLower().Contains(queryParameters.Name.ToLower())
                    );
            }
            if (!String.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(
                    queryParameters.SortBy,
                    queryParameters.SortOrder
                    );
                }
            }
            products = products
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);
            return Ok(await products.ToListAsync());
        }

        [HttpGet, Route("/products/{id}")]//[HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            //if (!ModelState.IsValid) {
            //    return BadRequest();
            //}
            _shopContext.Products.Add(product);
            await _shopContext.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id },
                product
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            if(id!= product.Id)
            {
                return BadRequest();
            }
            _shopContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shopContext.Products.Any(p => p.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            _shopContext.Products.Remove(product);
            await _shopContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPost]
        [Route ("Delete")]
        public async Task<ActionResult> DeleteMultiple([FromQuery]int[] ids)
        {
            var products = new List<Product>();
            foreach(var id in ids)
            {
                var product = _shopContext.Products.FindAsync(id);
                if (product.Result == null)
                    return NotFound();
                products.Add(product.Result);
            }
            
            _shopContext.Products.RemoveRange(products);
            await _shopContext.SaveChangesAsync();
            return Ok(products);
        }
    }

    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    //[Route("v{v:apiVersion}/products")]
    [Route("products")]
    [ApiController]
    public class ProductsV2Controller : ControllerBase
    {
        private readonly ShopContext _shopContext;
        public ProductsV2Controller(ShopContext context)
        {
            _shopContext = context;
            //to ensure the db is created
            _shopContext.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _shopContext.Products.Where(p=> p.IsAvailable == true);
            if (queryParameters.MinPrice != null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value
                    );
            }
            if (queryParameters.MaxPrice != null)
            {
                products = products.Where(
                    p => p.Price <= queryParameters.MaxPrice.Value
                    );
            }
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                products = products.Where(x => (
                x.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower()) ||
                x.Sku.ToLower().Contains(queryParameters.SearchTerm.ToLower()))
                );
            }
            if (!String.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(

                    p => p.Sku.ToLower().Contains(queryParameters.Sku.ToLower()));
            }
            if (!String.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(
                    p => p.Name.ToLower().Contains(queryParameters.Name.ToLower())
                    );
            }
            if (!String.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(
                    queryParameters.SortBy,
                    queryParameters.SortOrder
                    );
                }
            }
            products = products
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);
            return Ok(await products.ToListAsync());
        }

        [HttpGet, Route("/products/{id}")]//[HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            //if (!ModelState.IsValid) {
            //    return BadRequest();
            //}
            _shopContext.Products.Add(product);
            await _shopContext.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id },
                product
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _shopContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shopContext.Products.Any(p => p.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            _shopContext.Products.Remove(product);
            await _shopContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult> DeleteMultiple([FromQuery] int[] ids)
        {
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = _shopContext.Products.FindAsync(id);
                if (product.Result == null)
                    return NotFound();
                products.Add(product.Result);
            }

            _shopContext.Products.RemoveRange(products);
            await _shopContext.SaveChangesAsync();
            return Ok(products);
        }
    }
}
