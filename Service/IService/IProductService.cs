using System;
using SharedServices.Models;

namespace CodeFuseAI_BlogCart.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAll();
        public Task<ProductDTO> Get(int productId);
    }
}

