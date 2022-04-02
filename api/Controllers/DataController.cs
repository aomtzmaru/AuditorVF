using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _repo;

        public DataController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetPerson")]
        public async Task<IActionResult> GetPerson()
        {
            IEnumerable<Person> person = await _repo.GetPerson();
            return Ok(person);
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
        // Update
    }
}