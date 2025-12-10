using Confluent.Kafka;
using EstateAgency.Api;
using EstateAgency.Application.Contracts;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Persistence;
using EstateAgency.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    var basePath = AppContext.BaseDirectory;

    var xmlApi = Path.Combine(basePath, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    c.IncludeXmlComments(xmlApi, includeControllerXmlComments: true);

    var xmlApplication = Path.Combine(basePath, "EstateAgency.Application.Contracts.xml");
    if (File.Exists(xmlApplication))
    {
        c.IncludeXmlComments(xmlApplication);
    }
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

builder.Services.AddOptions<KafkaOptions>()
    .Bind(builder.Configuration.GetSection("Kafka"));

builder.Services.AddHostedService<KafkaConsumerWorker>();

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var kafkaConnection = config.GetConnectionString("KafkaConnection")
        ?? throw new InvalidOperationException("KafkaConnection string is missing");

    var kafkaOptions = sp.GetRequiredService<IOptions<KafkaOptions>>().Value;

    var consumerConfig = new ConsumerConfig
    {
        BootstrapServers = kafkaConnection,
        GroupId = kafkaOptions.GroupId,
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = false,
        FetchMinBytes = kafkaOptions.FetchMinBytes
    };

    return new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
});

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
