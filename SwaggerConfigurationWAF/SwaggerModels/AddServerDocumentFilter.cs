using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerConfigurationWAF.SwaggerModels
{
    /// <summary>
    /// Filtro que adiciona o server na página swagger.
    /// </summary>
    public class AddServerDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Cria o server.
        /// </summary>
        /// <param name="swaggerDoc">Documento</param>
        /// <param name="context">Contexto</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers = new List<OpenApiServer>()
            {
                new OpenApiServer()
                {
                    Url = $"/{swaggerDoc.Info.Version}"
                }
            };
        }
    }
}
