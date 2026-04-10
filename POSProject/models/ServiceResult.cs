using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ServiceResult Ok(string message = "") =>
            new ServiceResult { Success = true, Message = message };

        public static ServiceResult Fail(string message) =>
            new ServiceResult { Success = false, Message = message };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "") =>
            new ServiceResult<T> { Success = true, Data = data, Message = message };

        public new static ServiceResult<T> Fail(string message) =>
            new ServiceResult<T> { Success = false, Message = message };
    }
}
