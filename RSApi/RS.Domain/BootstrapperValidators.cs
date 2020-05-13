using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RS.Domain.Models.Views;
using RS.Domain.Validators;

namespace RS.Domain
{
    /// <summary>
    /// The BootstrapperValidators
    /// </summary>
    public static class BootstrapperValidators
    {
        /// <summary>
        /// Initializes the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<IValidator<AppUserModel>, UserValidator>();
        }
    }
}