namespace FormiculaThreeJsDemonstration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Kinematicula.LogicViewing;
using ThreeJsViewerApi;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

        services.AddOpenApi();

        var scene = SeedScene.CreateAndPopulateScene();
        var logicView = new LogicView(scene);
        services.AddSingleton<ILogicView>(logicView);
        services.AddSingleton(typeof(IThreeJsViewerLogic), typeof(ThreeJsViewerLogic));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            if (env.IsDevelopment())
            {
                endpoints.MapOpenApi();
            }
            endpoints.MapDefaultControllerRoute();
        });
    }
}