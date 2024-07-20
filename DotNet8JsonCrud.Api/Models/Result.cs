using DotNet8JsonCrud.Api.Enums;
using DotNet8JsonCrud.Api.Resources;

namespace DotNet8JsonCrud.Api.Models
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsError => !IsSuccess;
        public EnumStatusCode StatusCode { get; set; }

        public static Result<T> SuccessResult(string message = "Success.", EnumStatusCode statusCode = EnumStatusCode.Success) =>
            new()
            { IsSuccess = true, Message = message, StatusCode = statusCode };

        public static Result<T> SuccessResult(T data, string message = "Success.", EnumStatusCode statusCode = EnumStatusCode.Success) =>
            new()
            { IsSuccess = true, Message = message, StatusCode = statusCode, Data = data };

        public static Result<T> FailureResult(string message = "Fail.", EnumStatusCode statusCode = EnumStatusCode.BadRequest) =>
            new()
            { IsSuccess = false, Message = message, StatusCode = statusCode };

        public static Result<T> FailureResult(Exception ex) =>
            new()
            { IsSuccess = false, Message = ex.ToString(), StatusCode = EnumStatusCode.InternalServerError };

        public static Result<T> ExecuteResult(int result) =>
            result > 0 ? Result<T>.SuccessResult() : Result<T>.FailureResult();

        public static Result<T> SaveSuccessResult() =>
            Result<T>.SuccessResult(MessageResource.SaveSuccess);

        public static Result<T> UpdateSuccessResult() =>
            Result<T>.SuccessResult(MessageResource.UpdateSuccess);

        public static Result<T> NotFoundResult(string message = "No Data Found.") =>
            Result<T>.FailureResult(message, EnumStatusCode.NotFound);
    }
}
