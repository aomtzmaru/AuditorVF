using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;
        public DataRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<IEnumerable<Person>> GetPerson() {
            IEnumerable<Person> PersonList = await _context.Person.ToListAsync();
            return PersonList;
        }
        public async Task<IEnumerable<Files>> GetFiles() {
            IEnumerable<Files> FileList = await _context.Files.ToListAsync();
            return FileList;
        }
        public async Task<IEnumerable<Log>> GetLogs() {
            IEnumerable<Log> LogList = await _context.Log.ToListAsync();
            return LogList;
        }
        public async Task<IEnumerable<Services>> GetServices() {
            IEnumerable<Services> ServiceList = await _context.Services.ToListAsync();
            return ServiceList;
        }
        public async Task<IEnumerable<User>> GetUsers() {
            IEnumerable<User> UserList = await _context.User.ToListAsync();
            return UserList;
        }

    }
}