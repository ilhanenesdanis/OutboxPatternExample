using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var sql = builder.AddSqlServer("sql", password: sqlPassword)
.WithLifetime(ContainerLifetime.Persistent)
.WithDataVolume();

var db = sql.AddDatabase("database");

builder.AddProject<Projects.Outbox_API>("OutboxAPI").WithReference(db).WaitFor(db);

builder.Build().Run();
