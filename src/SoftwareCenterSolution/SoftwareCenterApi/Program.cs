using Marten;
using SoftwareCenterApi.Vendors.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // going to eat some of the time to start this api, and use some memory.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// above this line is configuring services and opting in to dotnent features

// ask my env for the connection string to my db
var connectionString = builder.Configuration.GetConnectionString("software") ?? 
    throw new Exception("No software connection string found!");

// look at a lot of places - and it always looks in all the places, even if it already exists
// 1. appsettings.json
// 2. appsettings.{ASPNETCORE_ENVIRONMENT}.json
// 3. looks in the "secrets" in visual studio. Not showing this.
// 4. look in an env var on the machine it is running on
//    In this example it would look for connectionstrings__software
// 5. it will look on the command line when you do "dotnet run"

// set up my "service" that will connect to the db
builder.Services.AddMarten(config => 
{
    config.Connection(connectionString);
}).UseLightweightSessions();
// It will provide an object that implements a context class.
// IDocumentSession

// AddScoped = One Per HttpRequest
//builder.Services.AddScoped<VendorCreateModelValidator>();
builder.Services.AddVendorServices();


var app = builder.Build();
// after this line is configuring the HTTP "middleware" - how are actual requests and responses generated.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers(); // this uses dotnet reflection to scan you application and read those
// routing attributes and create the "routing table" - phone book
// // Current Route Table:
// GET requests to /vendors
// - Create an instance of ther VendorsController
// - Call the GetAllVendors method
// POST /vendors
// - create an instance of the vendors controller
// - call the addvendor method
// - but this is going to need an IDocumentSession
// GET requests to /vendors/{id}

app.Run();

public partial class Program;
