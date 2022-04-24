﻿using FastShop.Dtos.Requests;
using FastShop.Dtos.Responses;
using FastShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastShop.Business
{
    public interface IProductService
    {
        Task<ICollection<ProductListResponse>> GetProducts();
        Task AddProduct (AddProductRequest product);
        Task<bool> IsExist(int id);
        Task<ProductListResponse> GetProductById(int id);
        Task UpdateProduct(UpdateProductRequest product);
        Task DeleteProduct(int id);
    }
}
