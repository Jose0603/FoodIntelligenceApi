using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Service.Services.PedidosServices
{
    public interface IPedidosService
    {
        Task<CustomHttpResponse> GetAll(string userId);
        Task<CustomHttpResponse> GetAllWithSearchParam(string SearchParam);
        Task<CustomHttpResponse> GetAllWithPagination(int PageNumber);
        Task<CustomHttpResponse> GetAllWithPaginationAndSearchParam(int PageNumber, string SearchParam);
        Task<CustomHttpResponse> GetById(int Id);
        Task<CustomHttpResponse> Create(PedidoDto newItem);
        Task<CustomHttpResponse> Update(PedidoDto toEdit);
        Task<CustomHttpResponse> AddItem(DetallesPedidoDto newItem, string userId);
    }
}
