public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddCors(options =>
                    {
                        options.AddPolicy("AllowNetlify", builder =>
                        {
                            builder.WithOrigins("https://sequence-alignment-tool.netlify.app") // Allow Netlify's URL
                                   .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
                                   .AllowAnyHeader(); // Allow any headers (Content-Type, Authorization, etc.)
                        });
                    });
                    services.AddControllers();
                });

                webBuilder.Configure(app =>
                {
                    app.UseCors("AllowNetlify");  // Use the "AllowNetlify" CORS policy
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
