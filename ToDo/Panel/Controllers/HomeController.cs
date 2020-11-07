using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Panel.Helper;
using Panel.Models;

namespace Panel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiConnection _apiConnection;
        private string baseUrl = "https://localhost:5001/api/";
        public HomeController(IApiConnection apiConnection)
        {
            _apiConnection = apiConnection;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            if (IsAuth())
            {
                _apiConnection.AddHeader(new Dictionary<string, string>() { { "Authorization", "bearer " + GetToken() } });
                var response = await _apiConnection.Get<BaseResponse<List<GetJobResponse>>>(baseUrl + "Job/GetAll");
                if (response.IsSuccess)
                    if (response.Result.State == State.Success)
                        return View(new BaseResponse<List<GetJobResponse>>() { Result = response.Result.Result });
                    else
                        return View(new BaseResponse<List<GetJobResponse>>() { Message = response.Result.Message });
                else
                    return View(new BaseResponse<List<GetJobResponse>>() { Message = response.ErrorMessage });
            }
            else
                return View(new BaseResponse<List<GetJobResponse>>());
        }
        [HttpPost]
        public async Task<IActionResult> Index(FilterModel filterModel)
        {
            if (IsAuth())
            {
                _apiConnection.AddHeader(new Dictionary<string, string>() { { "Authorization", "bearer " + GetToken() } });
                var response = await _apiConnection.Post<BaseResponse<List<GetJobResponse>>>(baseUrl + "Job/GetWithFilter", filterModel);
                if (response.IsSuccess)
                    if (response.Result.State == State.Success)
                        return View(new BaseResponse<List<GetJobResponse>>() { Result = response.Result.Result });
                    else
                        return View(new BaseResponse<List<GetJobResponse>>() { Message = response.Result.Message });
                else
                    return View(new BaseResponse<List<GetJobResponse>>() { Message = response.ErrorMessage });

            }
            else
                return View(new BaseResponse<List<GetJobResponse>>() { Message = "Please login" });
        }

        [HttpGet]
        public async Task<IActionResult> AddJob()
        {
            return View(new BaseResponse<object>());
        }
        [HttpPost]
        public async Task<IActionResult> AddJob(AddJobRequest addJobRequest)
        {
            if (IsAuth())
            {
                _apiConnection.AddHeader(new Dictionary<string, string>() { { "Authorization", "bearer " + GetToken() } });
                var response = await _apiConnection.Post<BaseResponse<object>>(baseUrl + "Job/Add", addJobRequest);
                if (response.IsSuccess)
                    if (response.Result.State == State.Success)
                        return RedirectToAction("Index");
                    else
                        return View(new BaseResponse<List<GetJobResponse>>() { Message = response.Result.Message });
                else
                    return View(new BaseResponse<List<GetJobResponse>>() { Message = response.ErrorMessage });
            }
            else
                return View(new BaseResponse<List<GetJobResponse>>() { Message = "Please login" });
        }
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            var response = await _apiConnection.Post<BaseResponse<LoginResponse>>(baseUrl + "Account/Login", loginInfo);
            if (response.IsSuccess)
                if (response.Result.State == State.Success)
                {
                    var loginResponse = response.Result.Result;
                    HttpContext.Session.SetString("token", loginResponse.Token);
                }
                else
                {
                    var baseModel = new BaseViewModel<object>();
                    baseModel.ErrorMessage = response.Result.Header + ":-" + response.Result.Message;
                    return RedirectToAction("Index", baseModel);
                }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Signup(AddUserRequest addInfo)
        {
            var response = await _apiConnection.Post<BaseResponse<LoginResponse>>(baseUrl + "Account/AddUser", addInfo);
            if (response.IsSuccess)
                if (response.Result.State == State.Success)
                {
                    var loginResponse = response.Result.Result;
                    HttpContext.Session.SetString("token", loginResponse.Token);
                }
                else
                {
                    ViewBag.Error = response.Result.Header + ":-" + response.Result.Message;
                }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Done(JobDoneRequest doneRequest)
        {
            if (IsAuth())
            {
                _apiConnection.AddHeader(new Dictionary<string, string>() { { "Authorization", "bearer " + GetToken() } });
                var response = await _apiConnection.Post<BaseResponse<object>>(baseUrl + "Job/Done", doneRequest);
                if (response.IsSuccess)
                    if (response.Result.State == State.Success)
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Index", new BaseResponse<List<GetJobResponse>>() { Message = response.Result.Message });
                else
                    return RedirectToAction("Index", new BaseResponse<List<GetJobResponse>>() { Message = response.ErrorMessage });
            }
            else
                return RedirectToAction("Index", new BaseResponse<List<GetJobResponse>>() { Message = "Please login" });
        }
        private bool IsAuth()
        {
            var token = HttpContext.Session.GetString("token");
            return !string.IsNullOrEmpty(token);
        }
        private string GetToken()
        {
            var token = HttpContext.Session.GetString("token");
            return token;
        }
    }
}
