using System;
using System.Collections.Generic;
using TravelCompany.Core.Enums;
using TravelCompany.Core.Validation;

namespace TravelCompany.Core.Models
{
    public class Result
    {
        public ResultStatusEnum Status { get; }

        private string _message;
        public string Message
        {
            get => !string.IsNullOrEmpty(_message) ? _message : Status.ToString();
            private set => _message = value;
        }

        public List<ValidationError> Errors { get; }

        /// <summary>
        /// Equivalent to "Status == ResultStatusEnum.Success"
        /// </summary>
        public bool Successed => Status == ResultStatusEnum.Success;

        /// <summary>
        /// Equivalent to "Status != ResultStatusEnum.Success"
        /// </summary>
        public bool NotSuccessed => Status != ResultStatusEnum.Success;

        protected Result(ResultStatusEnum resultStatus, string message, List<ValidationError> errors)
        {
            Status = resultStatus;
            Message = message;
            Errors = errors;
        }

        public static Result Success() => new Result(ResultStatusEnum.Success, message: null, errors: null);

        public static Result GeneralError(string message) =>
            new Result(ResultStatusEnum.GeneralError, message, errors: null);

        public static Result GeneralError(Exception exception) =>
            new Result(ResultStatusEnum.GeneralError, exception.Message, errors: null);

        public static Result ValidationError(ValidationError error, string message = "Validation error") =>
            new Result(ResultStatusEnum.ValidationError, message, new List<ValidationError> { error });

        public static Result ValidationError(List<ValidationError> errors, string message = "Validation error") =>
            new Result(ResultStatusEnum.ValidationError, message, errors);

        public static Result NotFound(string message = "Not found") =>
            new Result(ResultStatusEnum.NotFound, message, errors: null);

        public static Result<TResult> Success<TResult>(TResult obj) =>
            new Result<TResult>(ResultStatusEnum.Success, message: null, errors: null, obj);

        public static Result<TResult> GeneralError<TResult>(string message) =>
            new Result<TResult>(ResultStatusEnum.GeneralError, message, errors: null, obj: default);

        public static Result<TResult> GeneralError<TResult>(Exception exception) =>
            new Result<TResult>(ResultStatusEnum.GeneralError, exception.Message, errors: null, obj: default);

        public static Result<TResult> ValidationError<TResult>(ValidationError error, string message = "Validation error") =>
            new Result<TResult>(ResultStatusEnum.ValidationError, message, errors: new List<ValidationError> { error }, obj: default);

        public static Result<TResult> ValidationError<TResult>(List<ValidationError> errors, string message = "Validation error") =>
            new Result<TResult>(ResultStatusEnum.ValidationError, message, errors, obj: default);

        public static Result<TResult> NotFound<TResult>(string message = "Not found") =>
            new Result<TResult>(ResultStatusEnum.NotFound, message, errors: null, obj: default);
    }

    public class Result<T> : Result
    {
        public T Obj { get; }

        internal Result(ResultStatusEnum resultStatus, string message, List<ValidationError> errors, T obj)
            : base(resultStatus, message, errors)
        {
            Obj = obj;
        }
    }
}
