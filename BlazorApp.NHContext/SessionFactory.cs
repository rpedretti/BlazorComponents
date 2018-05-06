using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;

namespace BlazorApp.NHContext
{
    public class NHSessionFactory
    {
        private static readonly string DbFile = "locaDb.db";
        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFileWithPassword(DbFile, "test123"))
                .Mappings(m => 
                    m.FluentMappings.AddFromAssemblyOf<BaseNHContext>()
                )
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config)
        {
            if (!File.Exists(DbFile))
            {
                new SchemaExport(config).Create(false, true);
            }
        }
    }
}
