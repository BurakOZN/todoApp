using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MssqlRepository.ReturnModel
{
    public abstract class BaseResponse
    {
        public State State { get; set; }
        public object Result { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return String.Format("State:{0},Result:{1},Header:{2},Message:{3}",
                         State, JsonConvert.SerializeObject(Result), Header, Message);
        }
    }
    public sealed class SuccessResponse : BaseResponse
    {
        public SuccessResponse()
        {
            base.State = State.Success;
        }
        public SuccessResponse(string header, string message, object data = null)
        {
            base.State = State.Success;
            base.Header = header;
            base.Message = message;
            base.Result = data;
        }
        public SuccessResponse(object data, string header = null, string message = null)
        {
            base.State = State.Success;
            base.Header = header;
            base.Message = message;
            base.Result = data;
        }
    }
    public sealed partial class ErrorResponse : BaseResponse
    {
        public ErrorResponse(State errorState, string header = null, string message = null, object data = null)
        {
            base.State = errorState;
            base.Header = header;
            base.Message = message;
            base.Result = data;
        }
        public ErrorResponse(object data, State errorState, string header = null, string message = null)
        {
            base.State = State.Success;
            base.Header = header;
            base.Message = message;
            base.Result = data;
        }
    }
}
