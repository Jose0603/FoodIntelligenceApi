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
                List<Pedido> listOfEntites = _unitOfWork.PedidosRepository.GetAllInclude("DetallesPedidos.IdcomidaNavigation.IdrestauranteNavigation").Where(x => x.Idusuario == userId && x.EstadoPedido != "Abierto").ToList();
                if (listOfEntites != null && listOfEntites.Count > 0 || listOfEntites.Count == 0)
                {
                    if (listOfEntites.Count > 0)
                    {
                        var Data = listOfEntites.Select(_mapper.Map<PedidoDto>).ToList();
                        foreach (var dat in Data)
                        {
                            var entity = listOfEntites.FirstOrDefault(x => x.Id == dat.Id);
                            if (entity.DetallesPedidos != null && entity.DetallesPedidos.Count > 0)
                            {
                                dat.CantidadTotal = entity.DetallesPedidos.Sum(x => x.Cantidad) ?? 0;
                                dat.RestauranteId = entity.DetallesPedidos.FirstOrDefault().IdcomidaNavigation.Idrestaurante;
                                dat.RestauranteName = entity.DetallesPedidos.FirstOrDefault().IdcomidaNavigation.IdrestauranteNavigation.NombreRestaurante;
                                dat.RestauranteImagen = entity.DetallesPedidos.FirstOrDefault().IdcomidaNavigation.IdrestauranteNavigation.LogoRestaurante;
                            }
                        }
                        response.Data = Data;
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

        public async Task<CustomHttpResponse> Update(PedidoDto toEdit)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                var entity = _unitOfWork.PedidosRepository.GetById(toEdit.Id);

                if (entity != null)
                {
                    if (entity.EstadoPedido != toEdit.EstadoPedido)
                        entity.EstadoPedido = toEdit.EstadoPedido;
                    if (entity.Rating != toEdit.Rating)
                        entity.Rating = toEdit.Rating;
                    if (entity.FechaHoraPedido != toEdit.FechaHoraPedido)
                        entity.FechaHoraPedido = toEdit.FechaHoraPedido;
                }
                _unitOfWork.PedidosRepository.SaveChanges();
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
        public async Task<CustomHttpResponse> UpdateRating(PedidoDto toEdit)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                var entity = _unitOfWork.PedidosRepository.GetById(toEdit.Id);

                if (entity != null)
                {
                    if (entity.Rating != toEdit.Rating)
                    {

                        entity.Rating = toEdit.Rating;
                        entity.isRated = true;
                    }
                }
                _unitOfWork.PedidosRepository.SaveChanges();
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
        public async Task<CustomHttpResponse> AddItem(DetallesPedidoDto newItem, string userId)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                var entity = _unitOfWork.PedidosRepository.GetAllInclude("DetallesPedidos.IdcomidaNavigation").FirstOrDefault(x => x.Idusuario == userId && x.EstadoPedido == "Abierto");
                var comida = _unitOfWork.ComidasRepository.GetById(newItem.Idcomida ?? 0);
                if (comida != null)
                {
                    newItem.PrecioUnitario = comida.Precio;
                    if (entity != null)
                    {
                        var detallePedido = _unitOfWork.DetallesPedidoRepository.FindQueryable(x => x.Idpedido == entity.Id && x.Idcomida == newItem.Idcomida).FirstOrDefault();
                        if (detallePedido != null)
                            if (newItem.Cantidad != 0)
                            {

                                detallePedido.Cantidad = newItem.Cantidad;
                            }
                            else
                            {
                                _unitOfWork.DetallesPedidoRepository.Delete(detallePedido);
                            }
                        else
                        {
                            if (entity.DetallesPedidos.FirstOrDefault(x => x.IdcomidaNavigation.Idrestaurante != comida.Idrestaurante) != null)
                                foreach (var diferenteRestaurante in entity.DetallesPedidos)
                                    _unitOfWork.DetallesPedidoRepository.Delete(diferenteRestaurante);

                            newItem.Idpedido = entity.Id;
                            entity.DetallesPedidos.Add(_mapper.Map<DetallesPedido>(newItem));
                            _unitOfWork.DetallesPedidoRepository.SaveChanges();
                        }
                        entity.MontoTotal = 0;
                        foreach (var item in entity.DetallesPedidos)
                        {
                            entity.MontoTotal += item.PrecioUnitario * item.Cantidad;
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
                        newPedido.MontoTotal = newPedido.DetallesPedidos.Sum(x => x.PrecioUnitario * x.Cantidad);
                        _unitOfWork.PedidosRepository.Add(newPedido);
                    }
                }
                else
                    response.Success = false;
                _unitOfWork.DetallesPedidoRepository.SaveChanges();
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
        public async Task<CustomHttpResponse> GetCurrentPedido(string userId)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                Expression<Func<Pedido, bool>> predicate = pedido => pedido.Idusuario == userId && pedido.EstadoPedido == "Abierto";

                var entity = _unitOfWork.PedidosRepository.SingleOrDefaultAsync(predicate, "DetallesPedidos.IdcomidaNavigation.IdrestauranteNavigation");

                if (entity.Result != null)
                {
                    var data = _mapper.Map<PedidoDto>(entity.Result);
                    data.DetallesPedido = entity.Result.DetallesPedidos.Select(_mapper.Map<DetallesPedidoDto>).ToList();
                    data.CantidadTotal = entity.Result.DetallesPedidos.Sum(x => x.Cantidad) ?? 0;
                    if (entity.Result.DetallesPedidos != null && entity.Result.DetallesPedidos.Count > 0)
                    {
                        data.RestauranteId = entity.Result.DetallesPedidos.FirstOrDefault().IdcomidaNavigation.Idrestaurante;
                        data.RestauranteName = entity.Result.DetallesPedidos.FirstOrDefault().IdcomidaNavigation.IdrestauranteNavigation.NombreRestaurante;
                    }
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
