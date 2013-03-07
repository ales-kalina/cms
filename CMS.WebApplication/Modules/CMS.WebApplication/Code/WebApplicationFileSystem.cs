using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Xml.Linq;
using NuGet;

namespace CMS.WebApplication
{

    internal class WebApplicationFileSystem : PhysicalFileSystem, IProjectSystem
    {

        #region Constants

        private static readonly string BinFolderName = "bin";
        private static readonly string AppCodeFolderName = "App_Code";
        private static readonly string[] GeneratedFilesFolderNames = new[] { "Generated___Files" };
        private static readonly string[] SourceFileExtensions = new[] { ".cs", ".vb" };
        private static readonly string[] KnownPublicKeysTokens = new[] { "b03f5f7f11d50a3a", "b77a5c561934e089", "31bf3856ad364e35" };

        #endregion

        #region Constructors

        public WebApplicationFileSystem(string rootPath) : base(rootPath)
        {

        }

        #endregion

        #region IProjectSystem members

        public FrameworkName TargetFramework
        {
            get
            {
                AssemblyName name = new AssemblyName(typeof(string).Assembly.FullName);
                Version version = new Version(name.Version.Major, name.Version.Minor);
                return new FrameworkName(".NETFramework", version);
            }
        }

        public string ProjectName
        {
            get
            {
                return Root;
            }
        }

        public void AddReference(string referencePath, Stream stream)
        {
            string referenceName = Path.GetFileName(referencePath);
            string destinationPath = GetFullPath(GetReferencePath(referenceName));
            AddFile(destinationPath, stream);
        }

        public void AddFrameworkReference(string name)
        {
            var fullName = ResolvePartialAssemblyName(name);
            AddReferencesToConfig(this, fullName);
        }

        public bool ReferenceExists(string name)
        {
            string referencePath = GetReferencePath(name);
            return FileExists(referencePath);
        }

        public void RemoveReference(string name)
        {
            string referencePath = GetReferencePath(name);
            DeleteFile(referencePath);
        }

        public bool IsSupportedFile(string path)
        {
            return !Path.GetFileName(path).Equals("app.config", StringComparison.OrdinalIgnoreCase);
        }

        public string ResolvePath(string path)
        {
            if (RequiresAppCodeRemapping(path))
            {
                path = Path.Combine(AppCodeFolderName, path);
            }
            return path;
        }

        public bool IsBindingRedirectSupported
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region IPropertyProvider

        public dynamic GetPropertyValue(string propertyName)
        {
            if (propertyName == null)
            {
                return null;
            }
            if (propertyName.Equals("RootNamespace", StringComparison.OrdinalIgnoreCase))
            {
                return String.Empty;
            }
            return null;
        }

        #endregion

        #region PhysicalFileSystem members

        public override IEnumerable<string> GetDirectories(string path)
        {
            if (IsUnderAppCode(path))
            {
                return base.GetDirectories(path).Except(GeneratedFilesFolderNames, StringComparer.OrdinalIgnoreCase);
            }
            return base.GetDirectories(path);
        }
        
        #endregion

        #region Private members

        private string GetReferencePath(string name)
        {
            return Path.Combine(BinFolderName, name);
        }

        private string ResolvePartialAssemblyName(string name)
        {
            foreach (string publicKeyToken in KnownPublicKeysTokens)
            {
                var assemblyFullName = String.Format(CultureInfo.InvariantCulture, "{0}, Version={1}, Culture=neutral, PublicKeyToken={2}", name, VersionUtility.DefaultTargetFrameworkVersion, publicKeyToken);
                try
                {
                    Assembly.Load(assemblyFullName);
                    return assemblyFullName;
                }
                catch
                {
                    // Do nothing
                }
            }
            throw new InvalidOperationException(String.Format("Unknown framework assembly: {0}", name));
        }

        private void AddReferencesToConfig(IFileSystem fileSystem, string references)
        {
            string configurationFilePath = Path.Combine(fileSystem.Root, "web.config");
            XDocument document;
            if (fileSystem.FileExists(configurationFilePath))
            {
                using (Stream stream = fileSystem.OpenFile(configurationFilePath))
                {
                    document = XDocument.Load(stream, LoadOptions.PreserveWhitespace);
                }
            }
            else
            {
                document = new XDocument(new XElement("configuration"));
            }
            XElement assembliesElement = GetOrCreateChild(document.Root, "system.web/compilation/assemblies");
            IEnumerable<XElement> matchingElements =
                from item in assembliesElement.Elements()
                where !String.IsNullOrEmpty(item.GetOptionalAttributeValue("assembly"))
                let assemblyName = new AssemblyName(item.Attribute("assembly").Value).Name
                where String.Equals(assemblyName, references, StringComparison.OrdinalIgnoreCase)
                select item;
            
            if (!matchingElements.Any())
            {
                assembliesElement.Add(new XElement("add", new XAttribute("assembly", references)));
                SaveDocument(fileSystem, configurationFilePath, document);
            }
        }

        private void SaveDocument(IFileSystem fileSystem, string configurationFilePath, XDocument document)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                fileSystem.AddFile(configurationFilePath, stream);
            }
        }

        private XElement GetOrCreateChild(XElement element, string childName)
        {
            foreach (string item in childName.Split('/'))
            {
                XElement child = element.Element(item);
                if (child == null)
                {
                    child = new XElement(item);
                    element.Add(child);
                }
                element = child;
            }
            return element;
        }

        private bool RequiresAppCodeRemapping(string path)
        {
            return !IsUnderAppCode(path) && IsSourceFile(path);
        }

        private bool IsUnderAppCode(string path)
        {
            return path.StartsWith(AppCodeFolderName + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsSourceFile(string path)
        {
            return SourceFileExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }

        #endregion

    }

}