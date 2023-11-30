using AutoMapper;
using FoodIntelligence.Data;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Service.Shared;
using System.Net;
using System.Security.Claims;

namespace FoodIntelligence.Service.Services.RestaurantesServices
{
    public class RestaurantesService : IRestaurantesService
    {
        protected readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public RestaurantesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<CustomHttpResponse> Create(RestauranteDto newItem)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomHttpResponse> GetAll()
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                List<Restaurante> listOfEntites = _unitOfWork.RestaurantesRepository.GetAllInclude("Comida.DetallesPedidos.IdpedidoNavigation").ToList();
                if (listOfEntites != null && listOfEntites.Count > 0 || listOfEntites.Count == 0)
                {
                    if (listOfEntites.Count > 0)
                    {
                        foreach (var item in listOfEntites
                                .Where(x => x.Comida != null && x.Comida.Any(q => q.DetallesPedidos != null && q.DetallesPedidos.Any())))
                        {
                            item.Rating = item.Comida
                                .Where(q => q.DetallesPedidos != null && q.DetallesPedidos.Any())
                                .SelectMany(comida => comida.DetallesPedidos
                                    .Where(pedido => pedido.IdpedidoNavigation != null)
                                    .Select(pedido => pedido.IdpedidoNavigation.Rating))
                                .DefaultIfEmpty(0) // Handle the case where there are no ratings
                                .Average();

                        }
                        response.Data = listOfEntites.Select(_mapper.Map<RestauranteDto>).ToList();

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

        public Task<CustomHttpResponse> Update(RestauranteDto toEdit)
        {
            throw new NotImplementedException();
        }
    }
}
