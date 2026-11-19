using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Interface
{
    /// <summary>
    /// Defines the contract for dynamic image processing engines.
    /// </summary>
    public interface IImagePlugin
    {
        /// <summary>
        /// Gets the human-readable name of the image processing engine.
        /// </summary>
        /// <value>human-readable name of the image processing engine</value>
        string Name { get; }

        /// <summary>
        /// Gets the target file extension that this engine is capable of processing.
        /// </summary>
        /// <value> target file extension that this engine is capable of processing. </value>
        string SupportedExtension { get; }

        /// <summary>
        /// Processes the specified image file based on the plugin's operational logic.
        /// </summary>
        /// <param name="fileName">The name or path of the target image file.</param>
        /// <returns>True if the image was successfully processed; otherwise, false.</returns>
        bool ProcessImage(string fileName);
    }
}
