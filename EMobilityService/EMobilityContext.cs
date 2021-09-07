using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobilityService
{
    class EMobilityContext : DbContext
    {
        public EMobilityContext(DbContextOptions<EMobilityContext> options) : base(options)
        {
        }

    }
}
