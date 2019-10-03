using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineServer.Models
{
    public class MineSweeperContext : IdentityDbContext<Player>
    {
        public MineSweeperContext(DbContextOptions<MineSweeperContext> options) : base(options) { }
    }

}
