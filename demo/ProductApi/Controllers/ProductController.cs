using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("productapi/[controller]")]
    public class ProductController : ControllerBase
    {
        private List<ProductDto> productDtos = new List<ProductDto>();
        public ProductController()
        {
            productDtos.Add(new ProductDto { Id = 1,Name="酒精",Price=22.5m });
            productDtos.Add(new ProductDto { Id = 2, Name = "84消毒液", Price = 19.9m });
            productDtos.Add(new ProductDto { Id = 3, Name = "医用口罩", Price = 55 });
        }

        /// <summary>
        /// 获取单个商品信息
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public ProductDto Get(long id)
        {
            return productDtos.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// 获取所有商品信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IEnumerable<ProductDto> GetAll()
        {
            return productDtos;
        }
    }
}