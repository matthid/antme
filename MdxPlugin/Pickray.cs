using Microsoft.DirectX;

namespace AntMe.Plugin.Mdx {
    /// <summary>
    /// Pickray for selection
    /// </summary>
    internal struct Pickray {
        /// <summary>
        /// Gets or sets the current Ray-Direction.
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        /// Gets or sets the Ray-Origin.
        /// </summary>
        public Vector3 Origin;
    }
}