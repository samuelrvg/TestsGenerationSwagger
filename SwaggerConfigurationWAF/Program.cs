//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using SwaggerConfigurationWAF.SwaggerModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoAPI", Version = "v1" });
});

builder.Services.AddSwaggerGen(opt =>
{
    //opt.SwaggerDoc("v2", new OpenApiInfo { Title = "TodoAPI2", Version = "v2" });
    opt.DocumentFilter<AddServerDocumentFilter>();
    opt.OperationFilter<ParameterRemoveFilter>("ValidationResult");
    opt.DocumentFilter<AddCorsOptionsDocumentFilter>();
    opt.DocumentFilter<CustomDocumentFilter>();
    opt.CustomOperationIds(e => $"{e.RelativePath}{Guid.NewGuid()}");
    opt.CustomSchemaIds(x => x.Namespace + new Random().Next());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapSwagger("swagger/{documentName}/swagger.json");
app.MapSwagger("swagger/{documentName}/swaggerv2.json", c =>
{
    c.SerializeAsV2 = true;
});

app.Run();