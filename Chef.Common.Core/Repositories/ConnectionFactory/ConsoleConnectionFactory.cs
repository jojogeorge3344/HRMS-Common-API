using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Chef.Common.Repositories;

public class ConsoleConnectionFactory : IConnectionFactory, IDisposable
{
    private readonly Guid _id;
    private readonly IConfiguration configuration;

    public ConsoleConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;

        _id = Guid.NewGuid();
        Connection.Open();
    }

    public IDbConnection Connection
    {
        get
        {
            return new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    public IDbTransaction Transaction { get; set; }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}