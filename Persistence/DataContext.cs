using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, Int64>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            //trigger
            //builder.Entity<OD_AdminMenu>()
            //    .ToTable(tb => tb.HasTrigger("trg_Insert_Menu"));

            //builder.Entity<OD_AdminMenu>()
            //    .ToTable(tb => tb.HasTrigger("trg_Delete_Menu"));
        }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<NhomThuTuc> NhomThuTuc { get; set; }
        public DbSet<ThuTucHanhChinh> ThuTucHanhChinh { get; set; }
        public DbSet<CauHoi> CauHoi { get; set; }
        public DbSet<SubNhomThuTuc> SubNhomThuTuc { get; set; }
        public DbSet<ThuTucHanhChinhCompact> ThuTucHanhChinhCompact { get; set; }
        public DbSet<ThuTucHanhChinhExtend> ThuTucHanhChinhExtend { get; set; }
        public DbSet<YDinhNguoiDung> YDinhNguoiDung { get; set; }
        public DbSet<ThucTheDinhDanh> ThucTheDinhDanh { get; set; }
    }
}
