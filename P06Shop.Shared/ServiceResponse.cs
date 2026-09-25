using System;
using System.Collections.Generic;
using System.Text;

namespace P06Shop.Shared
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; } = default;

        public bool Success { get; set; } = false;

        public string Message { get; set; } = string.Empty;
    }
}
