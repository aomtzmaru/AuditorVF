using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}