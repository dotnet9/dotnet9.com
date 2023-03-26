using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

// If this service does not need to call other services, you can delete the following line.
builder.Services.AddDaprClient();
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<OrderActor>();
});
builder.Services
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = "";
        options.RequireHttpsMetadata = false;
        options.Audience = "";
    });
var app = builder.Services
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxxxxxxxxx\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    })
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<Program>();
    })
    .AddDomainEventBus(dispatcherOptions =>
    {
        dispatcherOptions
            .UseIntegrationEventBus<IntegrationEventLogService>(options => options.UseDapr().UseEventLog<ShopDbContext>())
            .UseEventBus(eventBusBuilder =>
            {
                eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
                eventBusBuilder.UseMiddleware(typeof(LogMiddleware<>));
            })
            .UseUoW<ShopDbContext>(dbOptions => dbOptions.UseSqlite("DataSource=:memory:"))
            //.UseEventLog<ShopDbContext>()
            .UseRepository<ShopDbContext>();
    })
    .AddServices(builder);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Used for Dapr Pub/Sub.
app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
    // Used for Dapr Actor
    endpoints.MapActorsHandlers();
});
app.UseHttpsRedirection();

app.Run();