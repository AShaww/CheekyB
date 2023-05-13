using Microsoft.ApplicationInsights.Extensibility;
using CheekyServices.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;

namespace CheekyB.Extensions;

public static class SerilogExtensions
{
   public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
    {
        var serilogOptions = builder.Configuration.GetSection("Serilog:SerilogOptionsConfigurations").Get<SerilogOptionsConfigurations>();
        var applicationInsightOptions = builder.Configuration.GetSection("ApplicationInsightConfigurations").Get<ApplicationInsightConfigurations>();

        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration, new ConfigurationReaderOptions { SectionName = sectionName });

            loggerConfiguration
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            if (serilogOptions?.UseConsole == true)
            {
                loggerConfiguration.WriteTo.Async(writeTo => writeTo.Console(outputTemplate: serilogOptions.LogTemplate));
            }
            
            if (serilogOptions?.UseApplicationInsight  == true)
            {
                loggerConfiguration.WriteTo.Async(writeTo =>
                    writeTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = applicationInsightOptions?.ApplicationInsightsId }, TelemetryConverter.Traces));
            }

            if (!string.IsNullOrEmpty(serilogOptions?.SeqUrl))
            {
                loggerConfiguration.WriteTo.Seq(serilogOptions.SeqUrl);
            }
        });

        return builder;
    }
}