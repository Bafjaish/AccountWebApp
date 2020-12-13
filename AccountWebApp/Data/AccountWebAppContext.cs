using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AccountWebApp.Models;

namespace AccountWebApp.Data
{
    public class AccountWebAppContext : DbContext
    {
        public AccountWebAppContext (DbContextOptions<AccountWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AccountWebApp.Models.AccountViewModel> AccountViewModel { get; set; }
    }
}
