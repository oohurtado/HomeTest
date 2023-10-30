using Home.Source.Common;
using Home.Source.Extensions;
using Microsoft.Data.SqlClient;

namespace Home.Source.Helpers
{
    public class BaseResponseHelper
    {        
        public static BaseResponse<T> GetCustomError<T>(string str)
        {
            var request = new BaseResponse<T>();
            request.Errors?.Add(str);
            return request;      
        }

        public static BaseResponse GetExceptionError(Exception ex)
        {
            var request = new BaseResponse();
            SqlException? e = ex.InnerException as SqlException;

            switch (e?.Number)
            {
                case 547:
                    request.Errors?.Add(AppResponse.RecordWithTies_OnDelete.GetBasicDescription());
                    break;
                case 2601:
                case 2627:
                    request.Errors?.Add(AppResponse.RecordDuplicated_OnInsertUpdate.GetBasicDescription());
                    break;
                default:
                    request.Errors?.Add(AppResponse.UnknownError.GetBasicDescription());
                    break;
            }

            return request;
        }

        public static BaseResponse<T> RecordNotFound_OnGet<T>()
        {
            var request = new BaseResponse<T>();
            request.Errors?.Add(AppResponse.RecordNotFound_OnGet.GetBasicDescription());
            return request;
        }

        public static BaseResponse RecordNotFound_OnCreate()
        {
            return new BaseResponse()
            {
                Errors = new List<string>()
                    {
                        AppResponse.RecordNotFound_OnCreate.GetBasicDescription()
                    }
            };
        }

        public static BaseResponse RecordNotFound_OnDelete()
        {
            return new BaseResponse()
            {
                Errors = new List<string>()
                    {
                        AppResponse.RecordNotFound_OnDelete.GetBasicDescription()
                    }
            };
        }

        public static BaseResponse RecordNotFound_OnUpdate()
        {
            return new BaseResponse()
            {
                Errors = new List<string>()
                    {
                        AppResponse.RecordNotFound_OnUpdate.GetBasicDescription()
                    }
            };
        }
    }
}
