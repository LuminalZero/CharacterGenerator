using CharacterGenerator.DesktopApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CharacterGenerator.DesktopApp
{
    internal class ServiceConfigurator
    {
        internal static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddTransient<ICharacterGeneratorService, CharacterGeneratorService>();
        }
    }
}
