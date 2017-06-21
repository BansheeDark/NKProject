using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Configuration;

namespace Shell.Models.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {

        public Model()
            : base("Shell.Properties.Settings.ConString")
        {
        }

        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groups>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Groups>()
                .Property(e => e.Semester)
                .IsFixedLength();

            modelBuilder.Entity<Students>()
                .Property(e => e.CodeGroups)
                .IsFixedLength();

            modelBuilder.Entity<Students>()
                .Property(e => e.Gender)
                .IsFixedLength();
        }
    }
}
