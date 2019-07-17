using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POCOModels.Inner;
using System;
using System.Collections.Generic;
using System.Text;

namespace POCOModels.Postgresql
{
    public class PstgreAuthContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {


        private readonly IOptions<PstgrSettings> _pstgrSettings;

        public PstgreAuthContext(IOptions<PstgrSettings> pstgrSettings) : base()
        {
            _pstgrSettings = pstgrSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_pstgrSettings.Value.ConnectionString);
    }
}
