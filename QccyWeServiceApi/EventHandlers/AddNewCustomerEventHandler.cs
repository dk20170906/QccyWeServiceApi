using Dapper;
using EdaSample.Services.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QccyWeServiceApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QccyWeServiceApi.EventHandlers
{
    public class AddNewCustomerEventHandler : EdaSample.Common.Events.EventHandler<CustomerCreatedEvent>
    {
        private readonly ILogger logger;
        private readonly IOptions<WebApiConfig> config;

        public AddNewCustomerEventHandler(ILogger<AddNewCustomerEventHandler> logger, IOptions<WebApiConfig> config,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.config = config;
        }

        public override async Task<bool> HandleAsync(CustomerCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            const string sql = "INSERT INTO [dbo].[Customers] ([CustomerId], [CustomerName]) VALUES (@Id, @Name)";
            using (var connection = new SqlConnection(this.config.Value.SqlConnectionString))
            {
                var customer = new SysUser(@event.CustomerId, @event.CustomerName);
                await connection.ExecuteAsync(sql, customer);
                this.logger.LogInformation($"客户信息创建成功。");
            }

            return true;
        }
    }
}
