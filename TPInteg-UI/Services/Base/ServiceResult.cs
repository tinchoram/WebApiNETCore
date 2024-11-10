using System;

namespace TPInteg_UI.Services.Base
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int ErrorCode { get; set; }
        public Exception Exception { get; set; }
    }
}