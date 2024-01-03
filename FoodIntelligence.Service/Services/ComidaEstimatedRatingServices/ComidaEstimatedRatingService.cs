using AutoMapper;
using FoodIntelligence.Data;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Data.Models;
using FoodIntelligence.Data.Repositories.BaseRepositories;
using FoodIntelligence.Service.Shared;
using MLModel_ConsoleApp1;
using System.Net;
using System.Security.Claims;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FoodIntelligence.Service.Services.ComidaEstimatedRatingServices
{
    public class ComidaEstimatedRatingService : IComidaEstimatedRatingService
    {
        protected readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        public ComidaEstimatedRatingService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<CustomHttpResponse> CreateAll()
        {
            CustomHttpResponse response = new CustomHttpResponse();
            try
            {
                await TruncateTables();
                List<Comidum> listOfFood = _unitOfWork.ComidasRepository.GetAll().ToList();
                List<AspNetUsers> listOfUsers = _unitOfWork.UserRepository.GetAll().ToList();
                List<ComidaEstimatedRating> comidaEstimatedRatings = _unitOfWork.ComidaEstimatedRatingRepository.GetAll().ToList();
                if (comidaEstimatedRatings != null && comidaEstimatedRatings.Count > 0)
                {
                    _unitOfWork.ComidaEstimatedRatingRepository.DeleteRange(comidaEstimatedRatings);
                    _unitOfWork.ComidaEstimatedRatingRepository.SaveChanges();
                }
                foreach (AspNetUsers user in listOfUsers)
                {
                    foreach (var food in listOfFood)
                    {
                        var sampleData = new MLModel.ModelInput()
                        {
                            Idcomida = food.Id,
                            Idusuario = user.Id,
                            FechaHoraPedido = DateTime.Now,
                        };

                        //Load model and predict output
                        var result = MLModel.Predict(sampleData);
                        comidaEstimatedRatings.Add(new ComidaEstimatedRating
                        {
                            UsuarioId = user.Id,
                            ComidaId = food.Id,
                            Rating = result.Score != result.Score ? 0 : (decimal?)result.Score
                        });
                    }

                }
                _unitOfWork.ComidaEstimatedRatingRepository.AddRange(comidaEstimatedRatings);
                _unitOfWork.ComidaEstimatedRatingRepository.SaveChanges();
                await UpdateTables();
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
        public async Task<bool> TruncateTables()
        {
            IDbConnection db = new SqlConnection(_configuration.GetConnectionString("connMSSQL"));
            using (db)
            {
                db.Open();


                var validatioResult = db.Execute("SPTruncateTables");


                db.Close();
            }

            return true;
        }
        public async Task<bool> UpdateTables()
        {
            IDbConnection db = new SqlConnection(_configuration.GetConnectionString("connMSSQL"));
            using (db)
            {
                db.Open();


                var validatioResult = db.Execute("SPUpdateRestaurateRatingPerUser");


                db.Close();
            }

            return true;
        }
    }
}
