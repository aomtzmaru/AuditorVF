using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public interface IDataRepository
    {
        Task<IEnumerable<Files>> GetFiles();
        Task<IEnumerable<Log>> GetLogs();
        Task<IEnumerable<Services>> GetServices();
        Task<IEnumerable<User>> GetUsers();
        Task<ServiceForReturn> Request(ServiceForRequest data);
    }
}