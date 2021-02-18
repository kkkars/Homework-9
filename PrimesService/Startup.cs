using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Primes.Primes;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace PrimesService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();

                    await context.Response.WriteAsync("Primes Web Service by Bilotska Karyna");

                    logger.LogDebug($"Status code: {{{context.Response.StatusCode}}}");
                });

                endpoints.MapGet("/primes/{number:int}", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>(); 

                    var number = int.Parse((string)context.Request.RouteValues["number"]);

                    var isPrime = IsPrime(number);

                    context.Response.StatusCode = isPrime
                    ? (int)HttpStatusCode.OK
                    : (int)HttpStatusCode.NotFound;

                    logger.LogDebug($"The number \'{number}\' is prime: {isPrime}");
                });

                endpoints.MapGet("/primes", async context =>
                {
                    var primes = new List<int>();

                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();

                    var fromString = context.Request.Query["from"].FirstOrDefault();
                    var toString = context.Request.Query["to"].FirstOrDefault();

                    if (int.TryParse(fromString, out var from) && int.TryParse(toString, out var to))
                    {
                        primes = GetPrimeNumbers(from, to);

                        context.Response.StatusCode = (int)HttpStatusCode.OK;

                        var response = "[" + string.Join(',', primes) + "]";
                        await context.Response.WriteAsync(response);

                        logger.LogDebug($"Found {primes.Count} on the range [{from};{to}]");
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        logger.LogDebug($"Failed");
                    }
                });
            });
        }
    }
}
