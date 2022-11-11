using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public class Datacontext:DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options): base(options)
        {

        }
        public DbSet<UserModel> users { get; set; }
    }
}
