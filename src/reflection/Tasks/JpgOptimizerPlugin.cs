using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Interface;

namespace Reflection.Tasks
{
    /// <summary>
    /// A plugin that optimizes JPG images by handling metadata stripping and color profile compression.
    /// </summary>
    public class JpgOptimizerPlugin : IImagePlugin
    {
        /// <summary>
        /// Gets the name of the image processing plugin.
        /// </summary>
        /// <value>name of the image processing plugin</value>
        public string Name => "JPG Color Profile Optimizer";

        /// <summary>
        /// Gets the primary supported file extension for this plugin.
        /// </summary>
        /// <value>primary supported file extension for this plugin</value>
        public string SupportedExtension => ".jpg";

        /// <summary>
        /// Processes the target image file if it matches the supported JPEG extensions.
        /// </summary>
        /// <param name="fileName">The name or path of the file to process.</param>
        /// <returns>True if optimization succeeded; otherwise, false.</returns>
        public bool ProcessImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            string extension = Path.GetExtension(fileName);
            bool isJpg = extension.Equals(this.SupportedExtension, StringComparison.OrdinalIgnoreCase);
            bool isJpeg = extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase);

            if (!isJpg && !isJpeg)
            {
                Console.WriteLine($"   [Skip] {this.Name} cannot handle {extension} files.");
                return false;
            }

            Console.WriteLine($"   [Success] Stripping EXIF metadata headers and tuning quantization tables for: '{fileName}'");
            return true;
        }
    }
}
