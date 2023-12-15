using AutoMapper;
using FoodIntelligence.Data;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Service.Shared;
using System.Net;
using System.Security.Claims;

namespace FoodIntelligence.Service.Services.DetallePedidoServices
{
    public class DetallePedidoService : IDetallePedidoService
    {
        protected readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public DetallePedidoService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<CustomHttpResponse> Create(DetallesPedidoDto newItem)
        {
            throw new NotImplementedException();
        }
        public async Task<CustomHttpResponse> Delete(int id)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                DetallesPedido entity = _unitOfWork.DetallesPedidoRepository.GetById(id);
                if (entity != null)
                {
                    Pedido pedido = _unitOfWork.PedidosRepository.GetById(entity.Idpedido ?? 0);
                    pedido.MontoTotal -= entity.PrecioUnitario * entity.Cantidad;
                    _unitOfWork.DetallesPedidoRepository.Delete(entity);
                }
                _unitOfWork.DetallesPedidoRepository.SaveChanges();
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                ErrorHandler.HandleErrorWithResponse(
                    ex,
                    response,
                    "Ha ocurrido un error en la base de datos.",
                    HttpStatusCode.Conflict
                );
            }
            return response;
        }

        public async Task<CustomHttpResponse> GetAll(int restauranteId)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                List<DetallesPedido> listOfEntites = _unitOfWork.DetallesPedidoRepository.GetAll().Where(x => x.Idpedido == restauranteId).ToList();
                if (listOfEntites != null && listOfEntites.Count > 0 || listOfEntites.Count == 0)
                {
                    if (listOfEntites.Count > 0)
                    {
                        response.Data = listOfEntites.Select(_mapper.Map<DetallesPedidoDto>).ToList();
                    }
                    else if (listOfEntites.Count == 0)
                    {
                        response.Data = new List<object>();
                    }
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                ErrorHandler.HandleErrorWithResponse(
                    ex,
                    response,
                    "Ha ocurrido un error en la base de datos.",
                    HttpStatusCode.Conflict
                );
            }
            return response;
        }

        public Task<CustomHttpResponse> GetAllWithPagination(int PageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<CustomHttpResponse> GetAllWithPaginationAndSearchParam(int PageNumber, string SearchParam)
        {
            throw new NotImplementedException();
        }

        public Task<CustomHttpResponse> GetAllWithSearchParam(string SearchParam)
        {
            throw new NotImplementedException();
        }

        public Task<CustomHttpResponse> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomHttpResponse> Update(DetallesPedidoDto toEdit)
        {
            throw new NotImplementedException();
        }
    }
}
