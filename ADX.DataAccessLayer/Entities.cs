/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities.DomainModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ADX.DataAccessLayer
{
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<MailTemplate> MailTemplates { get; set; }

        public DbSet<NotificationByUser> NotificationsByUser { get; set; }

        public DbSet<SystemParameter> SystemParameters { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<AspNetUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}