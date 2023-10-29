using AutoMapper;
using FoodIntelligence.Data;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Service.Shared;
using System.Net;
using System.Security.Claims;

namespace FoodIntelligence.Service.CategoriasComidaServices
{
    public class UserInfoService : IUserInfoService
    {
        protected readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public UserInfoService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomHttpResponse> Update(UserDto toEdit)
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {

                var allRecords = _unitOfWork.UserRepository.GetAll();

                var recordToEdit = allRecords.FirstOrDefault(x => x.Id == toEdit.Id);

                if (recordToEdit == null)
                {
                    response.Message = "No se encontro el usuario";
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                recordToEdit.FirstName = toEdit.FirstName;
                recordToEdit.LastName = toEdit.LastName;
                if (!allRecords.Any(x => x.Email == toEdit.Email))
                    recordToEdit.Email = toEdit.Email;
                recordToEdit.PhoneNumber = toEdit.PhoneNumber;

                await _unitOfWork.CommitAsync();

                response.Data = toEdit;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Se actualizo correctamente.";
                response.Success = true;

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
