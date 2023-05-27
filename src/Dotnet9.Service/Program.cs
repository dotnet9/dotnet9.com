var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    // local need dapr sidecar
    // builder.Services.AddDaprStarter();   
}

builder.Services.AddMasaConfiguration(new List<Assembly>()
{
    typeof(SiteOptions).Assembly
});
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
builder.Services.AddDaprClient();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services
    .AddMapster()
    .AddSequentialGuidGenerator()
    .Configure<AuditEntityOptions>(options => options.UserIdType = typeof(int))
    .AddMasaDbContext<Dotnet9DbContext>(dbContextBuilder =>
    {
        dbContextBuilder
            .UseNpgsql()
            .UseFilter();
    })
    .AddMultilevelCache(distributedCacheOptions => { distributedCacheOptions.UseStackExchangeRedisCache(); })
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
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
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxxxxxxxxx\"",
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
                new string[]
                {
                }
            }
        });
    })
    .AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters()
    .AddDomainEventBus(dispatcherOptions =>
    {
        dispatcherOptions
            .UseIntegrationEventBus<IntegrationEventLogService>(options =>
                options.UseDapr().UseEventLog<Dotnet9DbContext>())
            .UseEventBus(eventBusBuilder =>
            {
                eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
                eventBusBuilder.UseMiddleware(typeof(LoggingMiddleware<>));
            })
            .UseUoW<Dotnet9DbContext>()
            .UseRepository<Dotnet9DbContext>();
    })
    .AddAutoInject()
    .AddServices(builder);

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    ISeedService seedService = serviceScope.ServiceProvider.GetRequiredService<ISeedService>();
    await seedService.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});
app.UseHttpsRedirection();

app.Run();