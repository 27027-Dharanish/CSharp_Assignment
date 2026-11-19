using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reflection.Interface;

namespace Reflection.Tasks
{
    /// <summary>
    /// Scans, loads, and executes dynamic image processing plugins at runtime.
    /// </summary>
    public static class ImagePluginEngine
    {
        /// <summary>
        /// Discovers compatible plugins inside a directory and processes a target image file.
        /// </summary>
        /// <param name="pluginDirectoryPath">The folder path containing the plugin .dll files.</param>
        /// <param name="targetImageFile">The path or name of the image file to process.</param>
        public static void DiscoverAndProcess(string pluginDirectoryPath, string targetImageFile)
        {
            if (!Directory.Exists(pluginDirectoryPath))
            {
                Console.WriteLine($"[Error] Plugins folder missing at: {pluginDirectoryPath}");
                return;
            }

            string targetInterfaceName = typeof(IImagePlugin).FullName ?? string.Empty;
            string fileExtension = Path.GetExtension(targetImageFile);
            bool pluginFound = false;
            string[] dllFiles = Directory.GetFiles(pluginDirectoryPath, "*.dll");
            foreach (string file in dllFiles)
            {
                try
                {
                    Assembly pluginAssembly = Assembly.LoadFile(Path.GetFullPath(file));
                    if (ScanAssemblyForPlugins(pluginAssembly, targetInterfaceName, fileExtension, targetImageFile))
                    {
                        pluginFound = true;
                    }
                }
                catch
                {
                    // Fail silently for folder file conflicts
                }
            }

            if (!pluginFound)
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                ScanAssemblyForPlugins(currentAssembly, targetInterfaceName, fileExtension, targetImageFile);
            }
        }

        private static bool ScanAssemblyForPlugins(Assembly assembly, string interfaceName, string fileExtension, string targetImageFile)
        {
            bool found = false;

            foreach (Type exportedType in assembly.GetExportedTypes())
            {
                if (string.IsNullOrEmpty(exportedType.FullName) || exportedType.IsAbstract || !exportedType.IsClass)
                {
                    continue;
                }

                Type? interfaceMatch = exportedType.GetInterface(interfaceName);
                if (interfaceMatch != null)
                {
                    object? rawInstance = Activator.CreateInstance(exportedType);
                    if (rawInstance is IImagePlugin imagePlugin)
                    {
                        found = true;
                        Console.WriteLine($"\n[Discovered Engine]: {imagePlugin.Name} (Handles: {imagePlugin.SupportedExtension})");

                        bool canHandleJpg = imagePlugin.SupportedExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) &&
                                            (fileExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || fileExtension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase));

                        bool matchesDirectly = imagePlugin.SupportedExtension.Equals(fileExtension, StringComparison.OrdinalIgnoreCase);

                        if (matchesDirectly || canHandleJpg)
                        {
                            imagePlugin.ProcessImage(targetImageFile);
                        }
                        else
                        {
                            Console.WriteLine($"   [Skip] This plugin handles {imagePlugin.SupportedExtension} and cannot process '{fileExtension}' files.");
                        }
                    }
                }
            }

            return found;
        }
    }
}
