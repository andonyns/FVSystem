using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Repository
{
    public class BaseRepository
    {
        protected string connectionString;

        public BaseRepository(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                connectionString = config.GetConnectionString("DefaultConnection");
            } else
            {
                connectionString = Environment.GetEnvironmentVariable("CONN_STRING");
            }
        }
}
}
