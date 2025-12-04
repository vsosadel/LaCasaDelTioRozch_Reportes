using Microsoft.AspNetCore.Authorization;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Utils.Filters
{
    public class AuthorizeOperationProcessor(string securitySchemeName) : IOperationProcessor
    {
        private readonly string _securitySchemeName = securitySchemeName;

        public bool Process(OperationProcessorContext context)
        {
            // Revisa [Authorize] en acción o controlador
            var hasAuthorize =
                context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>().Any() ||
                context.MethodInfo.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>().Any();

            // Revisa [AllowAnonymous] en acción o controlador
            var allowAnonymous =
                context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .OfType<AllowAnonymousAttribute>().Any() ||
                context.MethodInfo.GetCustomAttributes(true)
                    .OfType<AllowAnonymousAttribute>().Any();

            if (!hasAuthorize || allowAnonymous)
                return true; // No requiere seguridad

            context.OperationDescription.Operation.Security ??= [];

            context.OperationDescription.Operation.Security.Add(
                new NSwag.OpenApiSecurityRequirement
                {
            { _securitySchemeName, Array.Empty<string>() }
                });

            return true;
        }
    }
}