using CampusHub.Api;
using CampusHub.Api.Configuration;
using CampusHub.Services.Logger;
using CampusHub.Services.Settings;
using CampusHub.Settings;
using CampusHub.Context;
using CampusHub.Context.Seeder;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppCors();
services.AddAppHealthChecks();

services.AddAppVersioning();

services.AddAppSwagger(mainSettings, swaggerSettings, identitySettings);

services.AddAppAutoMappers();
services.AddAppValidator();

services.AddAppControllerAndViews();

services.RegisterServices(builder.Configuration);


var app = builder.Build();

var logger = app.Services.GetRequiredService<IAppLogger>();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseErrorHandlingMiddleware();

app.UseAppControllerAndViews();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services);

logger.Information("CampusHub.API has started");

app.Run();

logger.Information("CampusHub.API has stopped");
