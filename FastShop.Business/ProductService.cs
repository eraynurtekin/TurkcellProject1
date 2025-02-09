﻿using AutoMapper;
using FastShop.DataAccess.Repositories;
using FastShop.Dtos.Requests;
using FastShop.Dtos.Responses;
using FastShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastShop.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private IMapper mapper;

        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task AddProduct(Product product)
        {
            await productRepository.Add(product);
        }

        public async Task AddProduct(AddProductRequest request)
        {
            request.CreatedDate = DateTime.Now;
            var product = mapper.Map<Product>(request);
            await productRepository.Add(product);

        }

        public async Task DeleteProduct(int id)
        {
           await productRepository.Delete(id);
           
        }

        public ICollection<ProductListResponse> GetAllProductsOrderBy()
        {
            var products =productRepository.GetAllProducts().OrderByDescending(x => x.CreatedDate).Where(y => y.IsActive == true);

            var productListResponse = mapper.Map<List<ProductListResponse>>(products);
            return productListResponse;
        }

        public ProductListResponse GetById(int id)
        {
            Product product = productRepository.GetProduct(id);
            var productListResponse = mapper.Map<ProductListResponse>(product);
            return productListResponse;
        }

        public async Task<ProductListResponse> GetProductById(int id)
        {
            Product product = await productRepository.GetEntityById(id);
            
            var productListResponse = mapper.Map<ProductListResponse>(product);
            
            return productListResponse;
        }

        public async Task<ICollection<ProductListResponse>> GetProducts()
        {
            var products = await productRepository.GetAllEntites();

            var productListResponse = mapper.Map<List<ProductListResponse>>(products);
            return productListResponse;
        }

        public async Task<bool> IsExist(int id)
        {
            return await productRepository.IsExists(id);
        }

        public int TotalOfProduct()
        {
           return productRepository.ProductCount();
        }

        public async Task UpdateProduct(UpdateProductRequest request)
        {
            request.CreatedDate = DateTime.Now;
            var product = mapper.Map<Product>(request);
            await productRepository.Update(product);
        
        }
    }
}
