using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panel.Models
{
    public class BaseResponse<T>
    {
        public State State { get; set; }
        public T Result { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
    }
   

    public enum State
    {
        Success = 0,
        ProcessError = 1,
        SaveError = 2,
        ProcessUnsuccess = 3,
        NotFound = 4,
        DuplicateError = 5,
        ConnectionError = 6,
    }
}
