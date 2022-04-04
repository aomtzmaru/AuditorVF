using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Helpers;
using api.Models;

namespace api.Data
{
    public interface IDataRepository
    {
        Task<IEnumerable<Files>> GetFiles();
        Task<IEnumerable<Log>> GetLogs();
        Task<PagedList<Services>> GetServices(UserParams userParams);
        Task<PagedList<Services>> GetAllServices(UserParams userParams);
        Task<IEnumerable<User>> GetUsers();
        Task<ServiceForReturn> Request(ServiceForRequest data);
        Task<ServiceForReturn> GetServiceDetail(int id);
        Task<bool> UpdateService(ServiceForUpdate data);
        string delFile(int Id);
        FileForDownload getFileDownload(int fileId, string fileName);
    }
}