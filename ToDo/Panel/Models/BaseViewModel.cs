using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panel.Models
{
    public class BaseViewModel<T>
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public T Object { get; set; }
    }
}
