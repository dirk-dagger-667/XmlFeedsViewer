namespace XMLFeedsViewer.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;

    using Hangfire;

    using Ninject.Syntax;
    using Ninject.Web.Common;

    public static class IBindingInSyntaxExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> InRequestOrBackgroundJobScope<T>(this IBindingInSyntax<T> syntax)
        {
            if (syntax == null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            return syntax.InNamedOrBackgroundJobScope(context => context.Kernel.Components
                .GetAll<INinjectHttpApplicationPlugin>()
                .Select(c => c.GetRequestScope(context))
                .FirstOrDefault(s => s != null));
        }
    }
}