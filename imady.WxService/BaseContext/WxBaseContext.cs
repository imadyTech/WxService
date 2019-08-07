using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imady.Domain;
using System.Reflection;

namespace imady.WxContext
{
    public class WxBaseContext : DbContext
    {

        public WxBaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// 如果构造函数接收到的是string类型的连接字符串，则试图生成option
        /// </summary>
        /// <param name="connectionstring"></param>
        public WxBaseContext(string connectionstring) : this(BuildContext(connectionstring))
        {
        }

        protected static DbContextOptions<WxBaseContext> BuildContext(string conn)
        {
            var optionbuilder = new DbContextOptionsBuilder<WxBaseContext>();
            optionbuilder.UseNpgsql (conn);
            //var context = new ApplicationContext<T>(optionbuilder.Options);
            //context.Database.EnsureCreated();
            return optionbuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        /*// 这是参考EfRepositoryPattern案例的代码，避免手工维护DbSet。但是不能在NetCore上实现。
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type[] typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
        */



        /* Frank: 2018-09-04 参考EfRepositoryPattern项目重新写ImadyBaseContext
         * 
         * 
        public ImadyBaseContext(DbContextOptions<ImadyBaseContext<T>> options) : base(options)
        {
        }

        public ImadyBaseContext(string connectionstring):this(BuildContext(connectionstring))
        {
        }

        protected static DbContextOptions<ImadyBaseContext<T>> BuildContext(string conn)
        {
            var optionbuilder = new DbContextOptionsBuilder<ImadyBaseContext<T>>();
            optionbuilder.UseSqlServer(conn);
            //var context = new ApplicationContext<T>(optionbuilder.Options);
            //context.Database.EnsureCreated();
            return optionbuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<T>().Property(t => t.AddedDate).IsRequired();
            modelBuilder.Entity<T>().Property(t => t.ModifiedDate).IsRequired();
            modelBuilder.Entity<T>().Property(t => t.UID).IsRequired();

            modelBuilder.Entity<T>().ToTable(model.TableName);

            base.OnModelCreating(modelBuilder);
            //model.BuildModel(modelBuilder.Entity<T>());

            //model.BuildModel(modelBuilder.Entity<Student>());

            //new StudentMap(modelBuilder.Entity<T>());

        }*/
    }
}
