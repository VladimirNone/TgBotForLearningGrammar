using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarDatabase
{
    public class GrammarDbContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<GrammarRule> GrammarRules { get; set; }

        public GrammarDbContext(DbContextOptions<GrammarDbContext> options)
            : base(options)
        {
        }

    }
}
