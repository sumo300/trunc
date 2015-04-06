using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Web.Compilation;

namespace Trunc.Resources
{
    public class AppResourceProvider : IResourceProvider
    {
        private readonly string _resourceClassName;
        private ResourceManager _resourceManager;

        public AppResourceProvider(string className) { _resourceClassName = className; }

        #region IResourceProvider Members

        public object GetObject(string resourceKey, CultureInfo culture)
        {
            EnsureResourceManager();
            if (culture == null)
            {
                culture = CultureInfo.CurrentUICulture;
            }
            return _resourceManager.GetObject(resourceKey, culture);
        }

        public IResourceReader ResourceReader
        {
            get
            {
                // Not needed for global resources
                throw new NotSupportedException();
            }
        }

        #endregion

        private void EnsureResourceManager()
        {
            Assembly assembly = typeof(Resources.ResourceInAppToGetAssembly).Assembly;
            String resourceFullName = String.Format("{0}.Resources.{1}", assembly.GetName().Name, _resourceClassName);
            _resourceManager = new ResourceManager(resourceFullName, assembly) {IgnoreCase = true};
        }
    }

    public class AppResourceProviderFactory : ResourceProviderFactory
    {
        // Thank you, .NET, for providing no way to override global resource providing w/o also overriding local resource providing
        private static readonly Type ResXProviderType =
            typeof(ResourceProviderFactory).Assembly.GetType("System.Web.Compilation.ResXResourceProviderFactory");

        private readonly ResourceProviderFactory _defaultFactory;

        public AppResourceProviderFactory() { _defaultFactory = (ResourceProviderFactory)Activator.CreateInstance(ResXProviderType); }

        public override IResourceProvider CreateGlobalResourceProvider(string classKey) { return new AppResourceProvider(classKey); }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            return _defaultFactory.CreateLocalResourceProvider(virtualPath);
        }
    }
}