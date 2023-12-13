using AutoMapper;
using FoodIntelligence.Data;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Service.Shared;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

namespace FoodIntelligence.Service.Services.PedidosServices
{
    public class PedidosService : IPedidosService
    {
        protected readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public PedidosService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<CustomHttpResponse> Create(PedidoDto newItem)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomHttpResponse> GetAll(string userId)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                List<Pedido> listOfEntites = _unitOfWork.PedidosRepository.GetAll().Where(x => x.Idusuario == userId).ToList();
                if (listOfEntites != null && listOfEntites.Count > 0 || listOfEntites.Count == 0)
                {
                    if (listOfEntites.Count > 0)
                    {
                        response.Data = listOfEntites.Select(_mapper.Map<PedidoDto>).ToList();
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

        public Task<CustomHttpResponse> Update(PedidoDto toEdit)
        {
            throw new NotImplementedException();
        }
        public async Task<CustomHttpResponse> AddItem(DetallesPedidoDto newItem, string userId)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                var entity = _unitOfWork.PedidosRepository.FindQueryable(x => x.Idusuario == userId && x.EstadoPedido == "Abierto").FirstOrDefault();
                var comida = _unitOfWork.ComidasRepository.GetById(newItem.Idcomida ?? 0);
                if (comida != null)
                {
                    newItem.PrecioUnitario = comida.Precio;
                    if (entity != null)
                    {
                        var detallePedido = _unitOfWork.DetallesPedidoRepository.FindQueryable(x => x.Idpedido == entity.Id && x.Idcomida == newItem.Idcomida).FirstOrDefault();
                        if (detallePedido != null)
                            if (newItem.Cantidad != 0)
                                detallePedido.Cantidad = newItem.Cantidad;
                            else
                            {
                                _unitOfWork.DetallesPedidoRepository.Delete(detallePedido);
                            }
                        else
                        {
                            newItem.Idpedido = entity.Id;
                            _unitOfWork.DetallesPedidoRepository.Add(_mapper.Map<DetallesPedido>(newItem));
                        }
                        response.Success = true;
                    }
                    else
                    {
                        Pedido newPedido = new Pedido();
                        newPedido.Idusuario = userId;
                        newPedido.FechaHoraPedido = DateTime.Now;
                        newPedido.EstadoPedido = "Abierto";
                        newPedido.DetallesPedidos.Add(_mapper.Map<DetallesPedido>(newItem));
                        _unitOfWork.PedidosRepository.Add(newPedido);
                    }
                }
                else
                    response.Success = false;
                response.StatusCode = HttpStatusCode.OK;
                _unitOfWork.DetallesPedidoRepository.SaveChanges();
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
        public async Task<CustomHttpResponse> GetCurrentPedido(string userId)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                var entity = _unitOfWork.PedidosRepository.GetAllInclude("DetallesPedidos.IdcomidaNavigation").FirstOrDefault(x => x.Idusuario == userId && x.EstadoPedido == "Abierto");
                if (entity != null)
                {
                    var data = _mapper.Map<PedidoDto>(entity);
                    data.DetallesPedido = entity.DetallesPedidos.Select(_mapper.Map<DetallesPedidoDto>).ToList();
                    response.Data = data;
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
    }
}
