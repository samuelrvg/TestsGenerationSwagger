using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerConfigurationWAF.SwaggerModels
{
    /// <summary>
    /// Define a classe AddOptionsRequests
    /// </summary>
    public class AddCorsOptionsDocumentFilter : IDocumentFilter
    {
        private const string AcaOrigin = "Access-Control-Allow-Origin";
        private const string AcaMethods = "Access-Control-Allow-Methods";
        private const string AcaHeaders = "Access-Control-Allow-Headers";

        /// <summary>
        /// Adiciona a operation para mapear as requisições do tipo options.
        /// </summary>
        /// <param name="swaggerDoc">O OpenApiDocument.</param>
        /// <param name="context">O DocumentFilterContext.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {

            foreach (var path in swaggerDoc.Paths)
            {
                var operation = path.Value.Operations.Values.First();
                var corsOptionOperation = BuildCorsOptionOperation(operation);
                path.Value.Operations.Add(OperationType.Options, corsOptionOperation);
            }
        }

        /// <summary>
        /// Constroi a operation para o as requests options
        /// <param name="operation">Um OpenApiOperation que é injetado.</param>
        /// <returns>Um objeto do tipo OpenApiOperation com as informações que apareceram no swagger</returns>
        /// </summary>
        private static OpenApiOperation BuildCorsOptionOperation(OpenApiOperation operation)
        {
            var response = new OpenApiResponse
            {
                Description = "Sucesso ao realizar operação",
                Headers = new Dictionary<string, OpenApiHeader>
            {
                { AcaOrigin, new OpenApiHeader {Description = "URI que pode acessar o recurso." } },
                { AcaMethods, new OpenApiHeader {Description = "Metodos permitidos ao acessar o recurso" } },
                { AcaHeaders, new OpenApiHeader {Description = "Indica quais cabeçalhos HTTP podem ser usados ​​ao fazer o request." } },
            }
            };

            return new OpenApiOperation
            {
                Summary = "CORS configs request",

                Parameters = operation.Parameters.Where(x => x.In == ParameterLocation.Path).ToList(),
                // agrupamento em cada path
                // Tags = new List<OpenApiTag> { new OpenApiTag { Name = operation.Tags?.FirstOrDefault().Name ?? "Cors Options" } },
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Cors Options" } },
                Responses = new OpenApiResponses
            {
                { "200", response }
            },
            };
        }
    }
}