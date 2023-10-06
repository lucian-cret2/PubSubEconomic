using Messaging;
using TicketingSystem.HostedServices;
using TicketingSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddHostedService<PaymentMessageProcessor>();
builder.Services.AddHostedService<RouteManagerMessageProcessor>();
builder.Services.AddTransient(typeof(IPubSub<>), typeof(PubSub<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
