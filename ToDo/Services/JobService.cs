﻿using ApiModels;
using Entity;
using Logger;
using Microsoft.Extensions.Configuration;
using MssqlRepository;
using MssqlRepository.Helper;
using MssqlRepository.ReturnModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogManager _logManager;
        private readonly IParameter _parameter;
        public JobService(IJobRepository JobRepository, IConfiguration configuration, ILogManager logManager, IParameter parameter)
        {
            _jobRepository = JobRepository;
            _configuration = configuration;
            _logManager = logManager;
            _parameter = parameter;
        }
        public async Task<BaseResponse> Add(AddJobRequest model)
        {
            var job = new Job();
            job.Name = model.Name;
            job.Description = model.Description;
            job.StartDate = model.StartDate;
            job.JobType = (JobType)model.Type;
            var result = await _jobRepository.Add(job);
            if (result.State != State.Success)
                _logManager.Error(result.ToString());
            return result;
        }
        public async Task<BaseResponse> GetAll()
        {
            var dbModels = await _jobRepository.Get(x => x.UserId == _parameter.UserId);
            if (dbModels.State != State.Success)
                _logManager.Error(dbModels.ToString());
            else
            {
                var results = ((List<Job>)dbModels.Result).Select(
                    x => new GetJobResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Type = (int)x.JobType,
                        StartDate = x.StartDate,
                    }
                    );
                return new SuccessResponse(results);
            }
            return dbModels;
        }
        public async Task<BaseResponse> GetWithType(int type)
        {
            var dbModels = await _jobRepository.Get(x => x.UserId == _parameter.UserId && x.JobType == (JobType)type);
            if (dbModels.State != State.Success)
                _logManager.Error(dbModels.ToString());
            else
            {
                var results = ((List<Job>)dbModels.Result).Select(
                    x => new GetJobResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Type = (int)x.JobType,
                        StartDate = x.StartDate,
                    }
                    );
                return new SuccessResponse(results);
            }
            return dbModels;
        }
        public async Task<BaseResponse> GetActive()
        {
            var now = DateTime.Now;
            var dbModels = await _jobRepository.Get(x => x.UserId == _parameter.UserId && !x.IsDone &&
            (
            (x.JobType == JobType.Daily && x.StartDate.Year == now.Year && x.StartDate.Month == now.Month && x.StartDate.Day == now.Day) ||
            (x.JobType == JobType.Weekly && x.StartDate.Year == now.Year && x.StartDate.DayOfYear <= now.DayOfYear && x.StartDate.DayOfYear + 7 > now.DayOfYear) ||
            (x.JobType == JobType.Weekly && x.StartDate.Year == now.Year && x.StartDate.Day <= now.Day && x.StartDate.Day + 30 > now.DayOfYear)
            ));
            if (dbModels.State != State.Success)
                _logManager.Error(dbModels.ToString());
            else
            {
                var results = ((List<Job>)dbModels.Result).Select(
                    x => new GetJobResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Type = (int)x.JobType,
                        StartDate = x.StartDate,
                    }
                    );
                return new SuccessResponse(results);
            }
            return dbModels;
        }
        public async Task<BaseResponse> Done(JobDoneRequest model)
        {
            var dbModel = await _jobRepository.Find(model.JobId);
            if (dbModel.State != State.Success)
            {
                _logManager.Error(dbModel.ToString());
                return dbModel;
            }
            var job = (Job)dbModel.Result;
            if (job.UserId != _parameter.UserId)
            {
                _logManager.Error("Unauthorized Access");
                return new ErrorResponse(State.ProcessError, "System Error", "System error please contact us");
            }
            job.IsDone = true;
            var result = await _jobRepository.Update(job);
            if (result.State != State.Success)
                _logManager.Error(result.ToString());
            return result;
        }
    }

}
