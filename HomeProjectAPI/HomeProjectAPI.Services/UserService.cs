using AutoMapper;
using HomeProjectAPI.API.Common.Settings;
using HomeProjectAPI.Services.Contracts;
using HomeProjectAPI.Services.Model;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using  SM = Services.Sql.Models;

namespace HomeProjectAPI.Services
{
    public class UserService : IUserService
    {
        private AppSettings _settings;
        private readonly IMapper _mapper;
        private readonly SM.EvdbContext _context;


        public UserService(IOptions<AppSettings> settings, IMapper mapper, SM.EvdbContext context)
        {
            _settings = settings?.Value;
            _mapper = mapper;
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string id)
        {
            return new User
            {
                Id = id,
                Firstname = "Firstname",
                Lastname = "Lastname",
                Address = new Address
                {
                    City = "City",
                    Country = "Country",
                    Street = "Street",
                    ZipCode = "ZipCode"
                }
            };
        }
    }
}