using System;
using CodeFuseAI_Shared.Models;

namespace CodeFuseAI_BlogCart.Service.IService
{
    public interface IClientFrontendService
    {
        public Task<ClientFrontendDTO> Get(int clientId);
        public Task<IEnumerable<ClientFrontendDTO>> GetAll();
        public Task<int> GetClientIdFromDomain(string domain);
    }
}


