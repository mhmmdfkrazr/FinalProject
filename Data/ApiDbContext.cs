using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PaymentApp.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public virtual DbSet<PaymentItem> PaymentDetail { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
    }
}