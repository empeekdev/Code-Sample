namespace TravelCompany.WebApi.DTO
{
    /// <summary>
    /// Base response. By default returns Message "OK" and Data object.
    /// </summary>
    /// <typeparam name="T">DTO model type</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// Base response. By default returns Message "OK" and Data object.
        /// </summary>
        /// <param name="data">Data to return</param>
        /// <param name="message">Message of response</param>        
        public BaseResponse(T data, string message = "OK", int errorCode = -1)
        {
            Data = data;
            Message = message;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Data of base response to return.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Message of base response
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error code of response
        /// </summary>
        public int ErrorCode { get; set; }
    }
}
