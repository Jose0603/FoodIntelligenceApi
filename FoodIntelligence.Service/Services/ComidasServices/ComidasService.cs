using AutoMapper;
using FoodIntelligence.Data;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Service.Shared;
using System.Net;
using System.Security.Claims;

namespace FoodIntelligence.Service.Services.ComidasServices
{
    public class ComidasService : IComidasService
    {
        protected readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public ComidasService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<CustomHttpResponse> Create(ComidumDto newItem)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomHttpResponse> GetAll(int restauranteId, string userId)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                List<Comidum> listOfEntites = _unitOfWork.ComidasRepository.GetAll().Where(x => x.Idrestaurante == restauranteId).ToList();
                if (listOfEntites != null && listOfEntites.Count > 0 || listOfEntites.Count == 0)
                {
                    if (listOfEntites.Count > 0)
                    {
                        var recomendaciones = _unitOfWork.ComidaEstimatedRatingRepository.GetAll().Where(x => x.UsuarioId == userId).ToList();
                        if (recomendaciones == null || recomendaciones.Count == 0)
                            response.Data = listOfEntites.Select(_mapper.Map<ComidumDto>).ToList();
                        else
                            response.Data = listOfEntites.Select(_mapper.Map<ComidumDto>)
                                .OrderByDescending(x => recomendaciones.First(rating => rating.ComidaId == x.Id).Rating).ToList();

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

        public Task<CustomHttpResponse> Update(ComidumDto toEdit)
        {
            throw new NotImplementedException();
        }
    }
}
