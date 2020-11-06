using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ToDo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Add(AddJobRequest model)
        {
            var result = await _jobService.Add(model);
            return Ok(result);
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _jobService.GetAll();
            return Ok(result);
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> GetWithType(int type)
        {
            var result = await _jobService.GetWithType(type);
            return Ok(result);
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> GetActive()
        {
            var result = await _jobService.GetActive();
            return Ok(result);
        }
        [HttpPut, Authorize]
        public async Task<IActionResult> Done(JobDoneRequest jobDone)
        {
            var result = await _jobService.Done(jobDone);
            return Ok(result);
        }
    }
}
