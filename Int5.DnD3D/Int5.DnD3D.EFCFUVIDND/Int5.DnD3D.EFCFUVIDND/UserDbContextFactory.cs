using Int5.DnD.EFCUVIDND.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Int5.DnD3D.EFCFUVIDND.DbSetup
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            string connString = "Data Source=user.db";
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseSqlite(connString);

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
