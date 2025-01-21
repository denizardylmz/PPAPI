﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using HomeProjectAPI.API.Common.Attributes;
using HomeProjectAPI.API.Common.Extensions;
using HomeProjectAPI.API.Common.Middlewares;
using HomeProjectAPI.API.Common.Settings;
using HomeProjectAPI.API.Swagger;
using HomeProjectAPI.IoC.Configuration.DI;
using Microsoft.EntityFrameworkCore;
using Services.Sql.Models;

#pragma warning disable CS1591
namespace HomeProjectAPI.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; private set; }

        private IConfigurationSection _appsettingsConfigurationSection;
        private AppSettings _appSettings;
        private ApplicationInsights _applicationInsightsSettings;

        private readonly ILogger _logger;
        private IServiceProvider _serviceProvider;

        public Startup(IConfiguration configuration, IWebHostEnvironment env, IServiceProvider serviceProvider,
            ILogger<Startup> logger)
        {
            HostingEnvironment = env;
            Configuration = configuration;
            _serviceProvider = serviceProvider;
            _logger = logger;

            //AppSettings
            _appsettingsConfigurationSection = Configuration.GetSection(nameof(AppSettings));
            if (_appsettingsConfigurationSection == null)
                throw new Exception("No appsettings has been found");

            _appSettings = _appsettingsConfigurationSection.Get<AppSettings>();

            //Application Insights
            var applicationInsightsConfiturationSection = Configuration.GetSection(nameof(ApplicationInsights));
            if (applicationInsightsConfiturationSection == null)
                throw new Exception("No appsettings has been found");

            _applicationInsightsSettings = applicationInsightsConfiturationSection.Get<ApplicationInsights>();

            _logger.LogDebug("Startup::Constructor::Settings loaded");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogTrace("Startup::ConfigureServices");

            if (_applicationInsightsSettings.Enabled)
            {
                //App monitoring
                //Based on Microsoft.ApplicationInsights.AspNetCore
                //https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core?tabs=netcorenew%2Cnetcore6&WT.mc_id=AZ-MVP-5004280
                services.AddApplicationInsightsTelemetry(options =>
                {
                    options.ConnectionString = _applicationInsightsSettings.ConnectionString;
                });

                //AI logging provider (ILogger, to build queries later)
                //https://learn.microsoft.com/en-us/azure/azure-monitor/app/ilogger?tabs=dotnet6&WT.mc_id=AZ-MVP-5004280
                //https://learn.microsoft.com/en-us/azure/azure-monitor/app/api-filtering-sampling?tabs=javascriptwebsdkloaderscript#add-properties-itelemetryinitializer&WT.mc_id=AZ-MVP-5004280
                //Based on Microsoft.Extensions.Logging.ApplicationInsight
                services.AddLogging(builder =>
                {
                    // Only Application Insights is registered as a logger provider
                    builder.AddApplicationInsights(
                        configureTelemetryConfiguration: (config) =>
                            config.ConnectionString = _applicationInsightsSettings.ConnectionString,
                        configureApplicationInsightsLoggerOptions: (options) =>
                        {
                            options.TrackExceptionsAsExceptionTelemetry = true;
                        }
                    );
                });

                _logger.LogTrace("Startup::ConfigureService::Configuring Application Insights");
            }

            try
            {
                if (_appSettings.IsValid())
                {
                    _logger.LogDebug("Startup::ConfigureServices::valid AppSettings");

                    services.Configure<AppSettings>(_appsettingsConfigurationSection);
                    _logger.LogDebug("Startup::ConfigureServices::AppSettings loaded for DI");

                    services.AddControllers()
                        .AddNewtonsoftJson();


                    //API versioning
                    services.AddApiVersioning(
                            o =>
                            {
                                //o.Conventions.Controller<UserController>().HasApiVersion(1, 0);
                                o.ReportApiVersions = true;
                                o.AssumeDefaultVersionWhenUnspecified = true;
                                o.DefaultApiVersion = new ApiVersion(1, 0);
                                o.ApiVersionReader = new UrlSegmentApiVersionReader();
                            }
                        )
                        .AddMvc()
                        //Known bug: https://github.com/dotnet/aspnet-api-versioning/issues/917
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        .AddApiExplorer(
                            options =>
                            {
                                options.GroupNameFormat = "'v'VVV";
                                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                                // can also be used to control the format of the API version in route homeprojectapis
                                options.SubstituteApiVersionInUrl = true;
                            });


                    //OPENAPI (SWAGGER)
                    if (_appSettings.Swagger.Enabled)
                    {
                        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

                        services.AddSwaggerGen(options =>
                        {
                            options.OperationFilter<SwaggerDefaultValues>();

                            //1-Get all the assemblies of the project to add the related XML Comments
                            Assembly currentAssembly = Assembly.GetExecutingAssembly();
                            AssemblyName[] referencedAssemblies = currentAssembly.GetReferencedAssemblies();
                            IEnumerable<AssemblyName> allAssemblies = null;

                            if (referencedAssemblies != null && referencedAssemblies.Any())
                                allAssemblies = referencedAssemblies.Union(new AssemblyName[]
                                    { currentAssembly.GetName() });
                            else
                                allAssemblies = new AssemblyName[] { currentAssembly.GetName() };

                            IEnumerable<string> xmlDocs = allAssemblies
                                .Select(a =>
                                    Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                                .Where(f => File.Exists(f));

                            //2-Add the path to the XML comments for the assemblies having enabled the doc generation
                            if (xmlDocs != null && xmlDocs.Any())
                            {
                                foreach (var item in xmlDocs)
                                {
                                    options.IncludeXmlComments(item);
                                }
                            }
                        });
                    }

                    services.AddProblemDetails();

                    //MAPPINGS
                    services.ConfigureMappings();

                    //HEALTH CHECKS CONFIGURATION
                    services.AddHealthChecks(Configuration);

                    //BUSINESS SETTINGS
                    services.ConfigureBusinessServices(Configuration);

                    services.AddDbContext<EvdbContext>(option =>
                        option.UseNpgsql((_appSettings.ConnectionStrings.EvdbConnection)));

                    //REPOSITORIES
                    services.ConfigureRepositories(Configuration);

                    //BUSINESS VALIDATORS
                    services.ConfigureValidators();

                    _logger.LogDebug("Startup::ConfigureServices::ApiVersioning, Swagger and DI settings");
                }
                else
                    _logger.LogDebug("Startup::ConfigureServices::invalid AppSettings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            _logger.LogTrace("Startup::Configure");
            _logger.LogDebug($"Startup::Configure::Environment:{env.EnvironmentName}");

            try
            {
                //HEALTH CHECKS ENDPOINT (using middleware, not endpoints)
                app.UseHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    AllowCachingResponses = false,
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                status = report.Status.ToString(),
                                errors = report.Entries.Select(e => new
                                    { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    _logger.LogInformation("Developer exception page loaded.");
                }
                else
                {
                    _logger.LogInformation("Setting not development exception handling settings.");
                    //Both alternatives are usable for general error handling:
                    // - middleware
                    // - UseExceptionHandler()

                    app.UseMiddleware(typeof(ErrorHandlingMiddleware));

                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                app.UseRequestLocalization();

                //SWAGGER
                if (_appSettings.IsValid())
                {
                    if (_appSettings.Swagger.Enabled)
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI(options =>
                        {
                            foreach (var description in provider.ApiVersionDescriptions)
                            {
                                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                    description.GroupName.ToUpperInvariant());
                                //options.RoutePrefix = string.Empty;
                            }
                        });
                    }
                }

                //CONTENT DEBUG MIDDLEWARE
                //NOTE: uncomment this to enable custom debug and logging middleware
                //app.UseMiddleware<ContentDebugMiddlewareSimple>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}