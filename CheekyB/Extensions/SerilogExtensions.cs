using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using CheekyServices.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace CheekyB.Extensions;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
    {
        var serilogOptions = builder.Configuration.GetSection("Serilog:SerilogOptionsConfigurations").Get<SerilogOptionsConfigurations>();
        var applicationInsightOptions = builder.Configuration.GetSection("ApplicationInsightConfigurations").Get<ApplicationInsightConfigurations>();

        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration, sectionName: sectionName);

            loggerConfiguration
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            if (serilogOptions.UseConsole)
            {
                loggerConfiguration.WriteTo.Async(writeTo => writeTo.Console(outputTemplate: serilogOptions.LogTemplate));
            }
            if (serilogOptions.UseApplicationInsight)
            {
                loggerConfiguration.WriteTo.Async(writeTo =>
                    writeTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = applicationInsightOptions.ApplicationInsightsId }, TelemetryConverter.Traces));
            }

            if (!string.IsNullOrEmpty(serilogOptions.SeqUrl))
            {
                loggerConfiguration.WriteTo.Seq(serilogOptions.SeqUrl);
            }
        });

        return builder;
    }
}