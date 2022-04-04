using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private string userFromToken;
        public DataController(IDataRepository repo)
        {
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
        public async Task<IActionResult> GetServices()
        {
            IEnumerable<Services> ServiceList = await _repo.GetServices();
            return Ok(ServiceList);
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