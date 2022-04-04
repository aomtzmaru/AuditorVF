using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Models;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _provider;
        public DataRepository(
            DataContext context, IMapper mapper,
            IHttpContextAccessor http,
            IConfiguration configuration,
            IDataProtectionProvider provider
        )
        {
            _configuration = configuration;
            _http = http;
            _mapper = mapper;
            _context = context;
            _provider = provider.CreateProtector(_configuration.GetSection("AppSettings:Token").Value);
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
            Services newData  = _mapper.Map<Services>(data);
            newData.Status = "อยู่ระหว่างดำเนินการ";
            newData.Deleted = 0;
            newData.CreatedDate = DateTime.Now;
            newData.CreatedIp = _http.HttpContext.Connection.RemoteIpAddress.ToString();
            newData.CreatedUser = TokenUtil.GetUserNameFromToken(_http.HttpContext.Request);

            if (newData.Files != null && newData.Files.Count() > 0)
            {
                // Save Files
                newData.Files.ToList().ForEach(r =>
                {
                    // Save file
                    Byte[] fileContent = Convert.FromBase64String(r.FileStream);
                    Stream stream = new MemoryStream(fileContent);
                    var fileNameHash = HashFileContent(stream);

                    string fileId = FileUtil.HashFile(fileNameHash + _configuration.GetSection("AppSettings:Token").Value);

                    string encryptFileName = fileNameHash + ".vf";
                    FileUtil.EncryptFile(fileId, encryptFileName, fileContent, newData.CreatedUser);

                    r.FileStream = null;
                    r.FileId = fileId;
                    r.EncryptFileName = encryptFileName;
                    r.CreatedUser = TokenUtil.GetUserNameFromToken(_http.HttpContext.Request);

                    r.CreatedIp = _http.HttpContext.Connection.RemoteIpAddress.ToString();
                });
            }

            await _context.Services.AddAsync(newData);
            await _context.SaveChangesAsync();

            InsertLog(newData.CreatedUser, newData.ServiceType, "บริการ");

            ServiceForReturn serviceForReturn = _mapper.Map<ServiceForReturn>(newData);

            return serviceForReturn;
        }

        public static string HashFileContent(Stream fileStream)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(fileStream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
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