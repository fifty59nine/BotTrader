using Microsoft.EntityFrameworkCore;
using BotTrader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotTrader.ContextFolder
{
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
                  DataBase=_BotDatabase;
                  Trusted_Connection=True;"
                );
        }
    }
}
