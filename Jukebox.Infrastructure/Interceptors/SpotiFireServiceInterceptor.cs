using System;
using Castle.Core.Interceptor;
using Jukebox.Infrastructure.Validators;
namespace Jukebox.Infrastructure.Interceptors
{
    public class SpotiFireServiceInterceptor : IInterceptor
    {
        private readonly ILibraryValidator _libraryValidator;

        public SpotiFireServiceInterceptor(ILibraryValidator libraryValidator)
        {
            _libraryValidator = libraryValidator;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            if (invocation.Method.Name.Equals("AddTrackFromSearch", StringComparison.InvariantCulture)) 
                _libraryValidator.Invalidate();
        }
    }
}
