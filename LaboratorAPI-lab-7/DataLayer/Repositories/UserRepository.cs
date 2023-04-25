using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(e => e.Email == email);
        }
        public User GetByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(e => e.Username == username);
        }
    }
}
