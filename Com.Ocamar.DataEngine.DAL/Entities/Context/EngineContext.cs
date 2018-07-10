using Com.Ocamar.DataEngine.DAL.Entities.Model;
using Com.Ocamar.DataEngine.DAL.Entities.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.DAL.Entities.Context
{
    public class EngineContext : DbContext
    {
        static EngineContext()
        {
            Database.SetInitializer<EngineContext>(null);
        }

        public EngineContext() : base("EngineContext")
        {
        }
        public EngineContext(string connectionString) : base(connectionString)
        {

        }
        public DbSet<StationManager> StationManager { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StationManagerMapping());
        }
    }
}
