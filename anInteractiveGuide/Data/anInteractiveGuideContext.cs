using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using anInteractiveGuide.Models;

namespace anInteractiveGuide.Data
{
    public class anInteractiveGuideContext : DbContext
    {
        public anInteractiveGuideContext (DbContextOptions<anInteractiveGuideContext> options)
            : base(options)
        {
        }

        public DbSet<anInteractiveGuide.Models.dise> dise { get; set; } = default!;

        public DbSet<anInteractiveGuide.Models.organs>? organs { get; set; }

        public DbSet<anInteractiveGuide.Models.users>? users { get; set; }
    }
}
