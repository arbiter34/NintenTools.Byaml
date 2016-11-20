using System.Collections.Generic;

namespace Syroot.NintenTools.Byaml.Serialization
{
    /// <summary>
    /// Represents options to control the serialization process of a <see cref="ByamlSerializer"/>.
    /// </summary>
    public class ByamlSerializerSettings
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ByamlSerializerSettings"/> class.
        /// </summary>
        public ByamlSerializerSettings()
        {
            SupportPaths = true;
            Version = ByamlVersion.Version1;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets a value indicating whether <see cref="IEnumerable{ByamlPathPoint}"/> instances will be
        /// supported or expected in the BYAML file.
        /// </summary>
        public bool SupportPaths { get; set; }

        /// <summary>
        /// Gets or sets the version of the BYAML file to write or expect.
        /// </summary>
        public ByamlVersion Version { get; set; }

    }

    /// <summary>
    /// Represents the supported BYAML file versions.
    /// </summary>
    public enum ByamlVersion
    {
        /// <summary>
        /// Represents version 1 of the BYAML file format.
        /// </summary>
        Version1 = 1
    }
}