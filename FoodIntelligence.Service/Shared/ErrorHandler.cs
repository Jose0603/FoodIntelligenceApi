using Microsoft.Data.SqlClient;
using System.Net;

namespace FoodIntelligence.Service.Shared
{
    public static class ErrorHandler
    {
        public static void HandleErrorWithResponse(
            Exception ex, 
            CustomHttpResponse response, 
            string defaultMessage,
            HttpStatusCode statusCode
        )
        {
            //Setting Response
            response.Message = defaultMessage;
            response.Success = false;
            response.StatusCode = statusCode;

            //Check if a SQL Exception
            SqlException sqlEx = ex?.InnerException as SqlException;
            if(sqlEx != null)
            {
                //SQL Errors Handler
                HandleSQLExceptionsWithRespose(sqlEx, response);
            }
            else
            {
                //Not handle exceptions
                if(ex?.InnerException?.Message != null && ex?.InnerException?.Message != String.Empty)
                {
                    response.Errors.Add(ex?.InnerException?.Message);
                }
                else if(ex?.Message != null && ex?.Message != String.Empty)
                {
                    response.Errors.Add(ex?.Message);
                }
            }
        }
        private static void HandleSQLExceptionsWithRespose(SqlException sqlEx, CustomHttpResponse response)
        {
            if (sqlEx.Errors != null && sqlEx.Errors.Count > 0)
            {
                for (int i = 0; i < sqlEx.Errors.Count; i++)
                {
                    SqlError sqlError = sqlEx.Errors[i];
                    if(sqlError != null)
                    {
                        switch (sqlError.Number)
                        {             

                            default:
                            break;
                        }
                    }
                }
            }
        }

        internal static void HandleErrorWithResponse(Exception ex, CustomHttpResponse response, object getAll_PRSON_Gender, HttpStatusCode conflict)
        {
            throw new NotImplementedException();
        }
    }


}
