using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        public DataRepository(DataContext context, IMapper mapper, IHttpContextAccessor http)
        {
            _http = http;
            _mapper = mapper;
            _context = context;

        }
        public async Task<IEnumerable<Files>> GetFiles()
        {
            IEnumerable<Files> FileList = await _context.Files.ToListAsync();
            return FileList;
        }
        public async Task<IEnumerable<Log>> GetLogs()
        {
            IEnumerable<Log> LogList = await _context.Log.ToListAsync();
            return LogList;
        }
        public async Task<IEnumerable<Services>> GetServices()
        {
            IEnumerable<Services> ServiceList = await _context.Services.ToListAsync();
            return ServiceList;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> UserList = await _context.User.ToListAsync();
            return UserList;
        }

        public async Task<ServiceForReturn> Request(ServiceForRequest data)
        {
            Services newData = new Services();

            newData = _mapper.Map<Services>(data);
            newData.Status = "อยู่ระหว่างดำเนินการ";
            newData.Deleted = 0;
            newData.CreatedDate = DateTime.Now;
            newData.CreatedIp = _http.HttpContext.Connection.RemoteIpAddress.ToString();
            newData.CreatedUser = TokenUtil.GetUserNameFromToken(_http.HttpContext.Request);

            await _context.Services.AddAsync(newData);
            await _context.SaveChangesAsync();

            InsertLog(newData.CreatedUser, newData.ServiceType, "บริการ");

            ServiceForReturn serviceForReturn = _mapper.Map<ServiceForReturn>(newData);

            return serviceForReturn;
        }

        public async void InsertLog(string username, string ActionDetail, string PageAction)
        {
            Log logData = new Log();
            logData.Username = username;
            logData.ActionDetail = ActionDetail;
            logData.PageAction = PageAction;
            logData.Created = DateTime.Now;
            logData.IP = _http.HttpContext.Connection.RemoteIpAddress.ToString();
            await _context.Log.AddAsync(logData);
            await _context.SaveChangesAsync();
        }
    }
}