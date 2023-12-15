using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Service.Services.DetallePedidoServices
{
    public interface IDetallePedidoService
    {
        Task<CustomHttpResponse> GetAll(int restauranteId);
        Task<CustomHttpResponse> GetAllWithSearchParam(string SearchParam);
        Task<CustomHttpResponse> GetAllWithPagination(int PageNumber);
        Task<CustomHttpResponse> GetAllWithPaginationAndSearchParam(int PageNumber, string SearchParam);
        Task<CustomHttpResponse> GetById(int Id);
        Task<CustomHttpResponse> Create(DetallesPedidoDto newItem);
        Task<CustomHttpResponse> Update(DetallesPedidoDto toEdit);
        Task<CustomHttpResponse> Delete(int Id);
    }
}
