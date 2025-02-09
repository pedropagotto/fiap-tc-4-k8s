var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var postgres = builder.AddPostgres("postgres", username, password, 5432)
    .WithDataVolume();

var postgresdb = postgres.AddDatabase("techchallenge01");

var authProject = builder.AddProject<Projects.AuthApi>("auth")
    .WithReference(postgresdb);

var readApi = builder.AddProject<Projects.ContactReadApi>("readApi")
    .WithReference(postgresdb);

var contactsApi = builder.AddProject<Projects.ContactApi>("cotactsApi")
    .WithReference(postgresdb);;

var contactWorker = builder.AddProject<Projects.ContactWorker>("contactWorker")
    .WithReference(postgresdb);

builder.AddContainer("grafana", "grafana/grafana")
    .WithBindMount("../grafana/config", "/etc/grafana", isReadOnly: true)
    .WithBindMount("../grafana", "/var/lib/grafana")
    .WithHttpEndpoint(targetPort: 3000, name: "http");

builder.AddContainer("prometheus", "prom/prometheus")
    .WithBindMount("../prometheus/prometheus.yml", "/etc/prometheus/prometheus.yml", isReadOnly: true)
    .WithHttpEndpoint(port: 9090, targetPort: 9090)
    .WithReference(readApi);

builder.Build().Run();
