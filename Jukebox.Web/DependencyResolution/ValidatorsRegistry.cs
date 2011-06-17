using Jukebox.Infrastructure.Validators;
using StructureMap.Configuration.DSL;

namespace Jukebox.Web
{
    public class ValidatorsRegistry : Registry
    {
        public ValidatorsRegistry()
        {
            For<ILibraryValidator>().Singleton().Use<LibraryValidator>();
        }
    }
}