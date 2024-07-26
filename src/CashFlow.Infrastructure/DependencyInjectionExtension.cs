using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {

        public static void AddInfraestructure(this IServiceCollection services, IConfiguration config)
        {
            AddDbContext(services, config);
            AddRepositories(services);

        }


        public static void AddRepositories(IServiceCollection services) 
        {
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
        }

    
        public static void AddDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("connection");  
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));


            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
       
        }
    }
}
