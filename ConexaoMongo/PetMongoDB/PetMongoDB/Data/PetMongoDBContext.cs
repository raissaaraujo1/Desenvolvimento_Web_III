using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetMongoDB.Models;

namespace PetMongoDB.Data
{
    public class PetMongoDBContext : DbContext
    {
        public PetMongoDBContext (DbContextOptions<PetMongoDBContext> options)
            : base(options)
        {
        }

        public DbSet<PetMongoDB.Models.Pet> Pet { get; set; } = default!;
    }
}
