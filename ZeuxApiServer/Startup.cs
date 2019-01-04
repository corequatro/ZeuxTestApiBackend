using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ZeuxApiServer.Configuration;
using ZeuxApiServer.Dal;
using ZeuxApiServer.Interface;
using ZeuxApiServer.Interface.UserAssets;
using ZeuxApiServer.Interface.UserAuthService;
using ZeuxApiServer.Middleware;
using ZeuxApiServer.Services;

namespace ZeuxApiServer
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var jwtConfig = new JwtAuthentication();
            Configuration.GetSection(nameof(JwtAuthentication)).Bind(jwtConfig);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSwaggerService(services);
            services.AddMvc(config =>
                 {
                     var policy = new AuthorizationPolicyBuilder()
                         .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                         .RequireAuthenticatedUser()
                         .Build();
                     config.Filters.Add(new AuthorizeFilter(policy));
                 }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped(typeof(IDal<>), typeof(DalDummy<>));
            services.AddScoped<IUserAssetsService, UserAssetsService>();
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.Configure<JwtAuthentication>(Configuration.GetSection("JwtAuthentication"));
            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseOptions();
            ConfigureSwaggerApp(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseMvc();
        }
    }
}
