using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SpecDriven.Runner.Config;

var builder = Host.CreateApplicationBuilder(args);

// 🔧 bind appsettings.json -> oggetto tipizzato
builder.Services.Configure<PipelineSettings>(
    builder.Configuration.GetSection("PipelineSettings")
);

// servizi
builder.Services.AddSingleton<PipelineExecutor>();

var app = builder.Build();

// esegui
var executor = app.Services.GetRequiredService<PipelineExecutor>();

await executor.RunAsync();