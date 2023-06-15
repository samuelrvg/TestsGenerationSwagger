using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace SwaggerConfigurationWAF.SwaggerModels
{
    /// <summary>
    /// Define um filtro para remoção dos parâmetros de uma operação.
    /// </summary>
    public class ParameterRemoveFilter : IOperationFilter
    {
        private readonly string parameterName;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="ParameterRemoveFilter"/>.
        /// </summary>
        /// <param name="parameterName">O nome do parâmetro.</param>
        public ParameterRemoveFilter(string parameterName)
        {
            this.parameterName = parameterName;
        }

        /// <summary>
        /// Aplica o filtro removendo os parâmetros que satisfazem o critério.
        /// </summary>
        /// <param name="operation">A operação.</param>
        /// <param name="context">O contexto.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Where(p => p.Name.Contains(parameterName, StringComparison.OrdinalIgnoreCase))
              .ToList()
              .ForEach(parameter => operation.Parameters.Remove(parameter));
        }
    }
}