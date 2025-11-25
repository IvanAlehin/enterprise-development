using EstateAgency.Application.Contracts;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Persistence;
using EstateAgency.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseInlineDefinitionsForEnums();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))
    ));

builder.Services.AddScoped<IRepository<EstateAgency.Domain.Entities.Application>, DbRepository<EstateAgency.Domain.Entities.Application>>();
builder.Services.AddScoped<IRepository<RealEstate>, DbRepository<RealEstate>>();
builder.Services.AddScoped<IRepository<Counterparty>, DbRepository<Counterparty>>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    db.Seed();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estate Agency");
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
