﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Repositories;
using StoreApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using StoreApi.Models.Enums;
using StoreApi.Repositories.Interfaces;

namespace StoreApi.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        public IProductRepository _products { get; set; }
        public ProductController(IProductRepository products)
        {
            _products = products;
        }
        // GET /Product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _products.Get();
        }

        // GET /Product/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            var product = _products.Get(id);
            if (product == null)
            {
                return StatusCode(404,"Product not found");
            }
            return new ObjectResult(product);
        }

        // POST /Product
        [HttpPost]
        public IActionResult Post([FromBody]Product p)
        {
            var added = _products.Create(p);

            return new OkObjectResult(added);
        }

        // PUT /Product/5
        [HttpPut]
        public IActionResult Put([FromBody]Product p)
        {

            var product = _products.Get(p.Id);
            if (product == null)
            {
                return StatusCode(404,"Product not found");
            }

            _products.Update(p.Id, p);
            return new OkObjectResult(p);
        }

        // DELETE /Product/5
        [HttpDelete]
        public IActionResult Delete([FromBody]Product p)
        {
            var product = _products.Get(p.Id);
            if (product == null)
            {
                return StatusCode(404,"Product not found");
            }

            _products.Remove(product.Id);
            return new OkObjectResult(product);
        }

    }
}

