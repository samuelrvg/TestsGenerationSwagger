using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerConfigurationWAF.SwaggerModels
{
    public class CustomDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Aplicar filtro personalizado no documento Swagger
            // Adicionar endpoints da versão 1 ao documento Swagger da versão 2
            var endpointsV1 = swaggerDoc.Paths;

            if (context.DocumentName.ToString().Contains("imperva"))

            { }



            var endpointsV2 = new OpenApiPaths();

            foreach (var endpoint in endpointsV1)
            {
                endpointsV2.Add(endpoint.Key.Replace("/v1", "/v2"), endpoint.Value);
            }

            swaggerDoc.Paths = endpointsV2;
        }
    }
}
