using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Dtos;
using api.Helpers;
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
        public async Task<PagedList<Services>> GetServices(UserParams userParams)
        {
            var ServiceList = _context.Services.AsQueryable();
            if (!string.IsNullOrEmpty(userParams.SearchKey))
            {
                ServiceList = ServiceList
                    .Where(c =>
                        c.PerId.Contains(userParams.SearchKey) ||
                        c.PrefixName.Contains(userParams.SearchKey) ||
                        c.FirstName.Contains(userParams.SearchKey) ||
                        c.LastName.Contains(userParams.SearchKey) ||
                        c.RegNumber.Contains(userParams.SearchKey) ||
                        c.ServiceType.Contains(userParams.SearchKey) ||
                        c.RecieveDoc.Contains(userParams.SearchKey) ||
                        c.RecieveBranch.Contains(userParams.SearchKey) ||
                        c.Status.Contains(userParams.SearchKey)
                    );
            }
            if (!string.IsNullOrEmpty(userParams.SearchStatus))
            {
                if (userParams.SearchStatus == "อยู่ระหว่างดำเนินการ")
                {
                    ServiceList = ServiceList.Where(c => c.Status == "อยู่ระหว่างดำเนินการ");
                }
                else if (userParams.SearchStatus == "รับไว้ดำเนินการ")
                {
                    ServiceList = ServiceList.Where(c => c.Status == "รับไว้ดำเนินการ");
                }
                else if (userParams.SearchStatus == "ดำเนินการเสร็จ")
                {
                    ServiceList = ServiceList.Where(c => c.Status == "ดำเนินการเสร็จ");
                }
            }
            ServiceList = ServiceList.Where(s => s.PerId == TokenUtil.GetUserNameFromToken(_http.HttpContext.Request));
            ServiceList = ServiceList.OrderByDescending(c => c.CreatedDate);
            return await PagedList<Services>.CreateAsync(ServiceList, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<Services>> GetAllServices(UserParams userParams)
        {
            var ServiceList = _context.Services.AsQueryable();
            if (!string.IsNullOrEmpty(userParams.SearchKey))
            {
                ServiceList = ServiceList
                    .Where(c =>
                        // c.regPid.Contains(complaintParams.SearchKey) ||
                        c.PrefixName.Contains(userParams.SearchKey) ||
                        c.FirstName.Contains(userParams.SearchKey) ||
                        c.LastName.Contains(userParams.SearchKey) ||
                        c.RegNumber.Contains(userParams.SearchKey) ||
                        c.ServiceType.Contains(userParams.SearchKey) ||
                        c.RecieveDoc.Contains(userParams.SearchKey) ||
                        c.RecieveBranch.Contains(userParams.SearchKey) ||
                        c.Status.Contains(userParams.SearchKey)
                    );
            }
            if (!string.IsNullOrEmpty(userParams.SearchStatus))
            {
                if (userParams.SearchStatus == "เปิดใช้งาน")
                {
                    ServiceList = ServiceList.Where(c => c.Deleted == 0);
                }
                else if (userParams.SearchStatus == "ปิดใช้งาน")
                {
                    ServiceList = ServiceList.Where(c => c.Deleted == 1);
                }
            }
            ServiceList = ServiceList.OrderByDescending(c => c.CreatedDate);
            return await PagedList<Services>.CreateAsync(ServiceList, userParams.PageNumber, userParams.PageSize);
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

        public async Task<ServiceForReturn> GetServiceDetail(int id)
        {
            Services services = await _context.Services.Where(s => s.Id == id).Include(s => s.Files).FirstOrDefaultAsync();
            ServiceForReturn serviceForReturn = _mapper.Map<ServiceForReturn>(services);
            return serviceForReturn;
        }

        public async Task<bool> UpdateService(ServiceForUpdate data)
        {
            Services services = await _context.Services.FirstOrDefaultAsync(s => s.Id == data.Id);
            if (services == null) return false;

            string username = TokenUtil.GetUserNameFromToken(_http.HttpContext.Request);

            services.UpdatedDate = DateTime.Now;
            services.UpdatedUser = username;
            services.UpdatedIp = _http.HttpContext.Connection.RemoteIpAddress.ToString();

            if (data.Files != null && data.Files.Count() > 0)
            {
                // Save Files
                data.Files.ToList().ForEach(r =>
                {
                    // Save file
                    Byte[] fileContent = Convert.FromBase64String(r.FileStream);
                    Stream stream = new MemoryStream(fileContent);
                    var fileNameHash = HashFileContent(stream);

                    string fileId = FileUtil.HashFile(fileNameHash + _configuration.GetSection("AppSettings:Token").Value);

                    string encryptFileName = fileNameHash + ".vf";
                    FileUtil.EncryptFile(fileId, encryptFileName, fileContent, username);

                    r.FileStream = null;
                    r.FileId = fileId;
                    r.EncryptFileName = encryptFileName;
                    r.CreatedUser = TokenUtil.GetUserNameFromToken(_http.HttpContext.Request);

                    r.CreatedIp = _http.HttpContext.Connection.RemoteIpAddress.ToString();
                });
            }

            services = _mapper.Map<ServiceForUpdate, Services>(data, services);

            return await _context.SaveChangesAsync() > 0;
        }

        public string delFile(int Id)
        {
            Files requestFile = _context.Files.Select(r => new Files() { Id = r.Id }).FirstOrDefault(r => r.Id == Id);
            if (requestFile == null)
            {
                return null;
            }

            _context.Remove(requestFile);

            _context.SaveChanges();
            return Id.ToString();
        }

        public FileForDownload getFileDownload(int fileId, string fileName)
        {
            Files requestFile = _context.Files.FirstOrDefault(f => f.Id == fileId);
            if (requestFile == null)
            {
                return null;
            }
            FileForDownload fileForDownload = _mapper.Map<FileForDownload>(requestFile);
            return fileForDownload;
        }
    }
}