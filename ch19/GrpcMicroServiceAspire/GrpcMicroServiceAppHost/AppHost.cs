var builder = DistributedApplication.CreateBuilder(args);
var sql = builder.AddSqlServer("sql");
var db = sql.AddDatabase("DefaultConnection");

var grpcMicroService = builder.AddProject<Projects.GrpcMicroService>("grpcmicroservice")
    .WithReference(db)
    .WaitFor(db);
var fakeSource = builder.AddProject<Projects.FakeSource>("fakesource")
    .WithReference(grpcMicroService)
    .WaitFor(grpcMicroService);
builder.Build().Run();
