using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.DAL.Entities.Model.Mapping
{
    public class StationManagerMapping : EntityTypeConfiguration<StationManager>
    {
        public StationManagerMapping()
        {
            this.ToTable("StationManager");

            HasKey(t => t.ID);

            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.IsEnable).HasColumnName("IsEnable");
            Property(t => t.PartNo).HasColumnName("PartNo");
            Property(t => t.Port).HasColumnName("Port");
            Property(t => t.IP).HasColumnName("IP");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
