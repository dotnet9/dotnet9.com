var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "Dotnet9.WebAPI",
    LogFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}Dotnet9.WebAPI.log"
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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