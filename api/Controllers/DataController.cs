using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Models;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private string userFromToken;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        public DataController(IDataRepository repo, IHttpContextAccessor http, IMapper mapper)
        {
            _mapper = mapper;
            _http = http;
            _repo = repo;
        }

        [HttpGet("GetFiles")]
        public async Task<IActionResult> GetFiles()
        {
            IEnumerable<Files> FileList = await _repo.GetFiles();
            return Ok(FileList);
        }

        [HttpGet("GetLogs")]
        public async Task<IActionResult> GetLogs()
        {
            IEnumerable<Log> LogList = await _repo.GetLogs();
            return Ok(LogList);
        }

        [HttpGet("GetServices")]
        public async Task<IActionResult> GetServices([FromQuery] UserParams userParams)
        {
            var ServiceList = await _repo.GetServices(userParams);
            IEnumerable<ServiceForReturn> serviceForReturns = _mapper.Map<IEnumerable<ServiceForReturn>>(ServiceList);
            Response.AddPagination(ServiceList.CurrentPage, ServiceList.PageSize, ServiceList.TotalCount, ServiceList.TotalPages);

            return Ok(serviceForReturns);
        }

        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices([FromQuery] UserParams userParams)
        {
            string role = TokenUtil.GetRoleFromToken(_http.HttpContext.Request);
            if (role != "admin") return Unauthorized();

            var ServiceList = await _repo.GetAllServices(userParams);
            IEnumerable<ServiceForReturn> serviceForReturns = _mapper.Map<IEnumerable<ServiceForReturn>>(ServiceList);
            Response.AddPagination(ServiceList.CurrentPage, ServiceList.PageSize, ServiceList.TotalCount, ServiceList.TotalPages);

            return Ok(serviceForReturns);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> UserList = await _repo.GetUsers();
            return Ok(UserList);
        }


        [HttpPost("AddRequest")]
        public async Task<IActionResult> AddRequest(ServiceForRequest data)
        {
            if (data == null) return BadRequest();

            ServiceForReturn dataReturn = await _repo.Request(data);

            return Ok(dataReturn);
        }
    }
}