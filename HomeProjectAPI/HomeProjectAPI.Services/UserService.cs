using AutoMapper;
using HomeProjectAPI.API.Common.Settings;
using HomeProjectAPI.Services.Contracts;
using HomeProjectAPI.Services.Model;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Services.Sql.Models;

namespace HomeProjectAPI.Services
{
    public class UserService : IUserService
    {
        private AppSettings _settings;
        private readonly IMapper _mapper;
        private readonly EvdbContext _context;


        public UserService(IOptions<AppSettings> settings, IMapper mapper, EvdbContext context)
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
            var not = new Notlar() { Id = 1, Note = "asdasd", Title = "123123", CreateDate = DateTime.Now};
            int result = 0;
            _context.Notlars.Add(not);
            result = await _context.SaveChangesAsync();

            return new User
            {
                Id = result.ToString(),
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