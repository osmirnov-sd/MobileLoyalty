using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POCOModels.Inner;
using System;
using System.Collections.Generic;
using System.Text;

namespace POCOModels.Postgresql
{
    public class PstgreTablesContext : DbContext
    {
        private readonly IOptions<PstgrSettings> _pstgrSettings;

        public PstgreTablesContext(IOptions<PstgrSettings> pstgrSettings) : base()
        {
            _pstgrSettings = pstgrSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_pstgrSettings.Value.ConnectionString);
    }
}

