using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.DAL.DataContext;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name ="Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    o.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddDbContext<IdentityDataContext>(o => 
    o.UseSqlServer(config.GetConnectionString("SqlServerConnection")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<IdentityDataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi <IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
