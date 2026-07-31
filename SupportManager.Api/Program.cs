using Scalar.AspNetCore;
using SupportManager.Api.Services;
using SupportManager.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document,context, cancellatin) =>
    {
        document.Info.Version = "1.0";
        document.Info.Title = "Ticket System API";
        document.Info.Description = "This API uses N-Layers infraestructure (Data,API, Web)";
        document.Info.Contact = new Microsoft.OpenApi.OpenApiContact
        {
          Name = "Gonzalo Perez",
          Email = "gonzalo.anuar13@outlook.com"  
        };
        return Task.CompletedTask;
    });
});
builder.Services.AddControllers(); //register controllers
builder.Services.AddValidation();

builder.Services.AddScoped<ITicketRepositories, TicketRepository>();
builder.Services.AddScoped<IDocumentosRepository, DocumentosRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IDocumentosService, DocumentosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

