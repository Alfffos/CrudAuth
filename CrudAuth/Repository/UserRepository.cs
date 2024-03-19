using Agenda_api.Models.DTOs;
using CrudAuth.Models;
using CrudAuth.Models.Entities;
using CrudAuth.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CrudAuth.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AplicationDbContext _context;
        public UserRepository(AplicationDbContext context)
        {
            _context = context;
        }


        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user); 
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
          
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<User> GetUserByName(string username)
        {
            User userExist = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            
            if(userExist == null)
            {
                return null;
            }
            
            return userExist;
        }

        public async Task<User> ValidateUser(AutenticationRequestBody autenticationRequestBody)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == autenticationRequestBody.UserName && x.Password == autenticationRequestBody.Password);
            
        }
    }
}
