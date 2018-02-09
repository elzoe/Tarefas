using Hangfire;
using Hangfire.Mongo;
using Owin;
using System;
using System.Configuration;
using System.Web;

namespace WebTask
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConexaoMongodb"].ConnectionString;

            var migrationOptions = new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    Strategy = MongoMigrationStrategy.Migrate,
                    BackupStrategy = MongoBackupStrategy.Collections
                }
            };

            GlobalConfiguration.Configuration.UseMongoStorage(connectionString, "ederlopes", migrationOptions);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}