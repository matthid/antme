using System;

namespace AntMe.Plugin.Mdx {
    /// <summary>
    /// Holds the settings for 3D-Visualizer
    /// </summary>
    [Serializable]
    public sealed class Visual3DConfiguration {
        /// <summary>
        /// Gets or sets the flag to show the instruction-window for next start.
        /// </summary>
        public bool ShowInstructionWindow;

        /// <summary>
        /// Creates a new instance of Configuration.
        /// </summary>
        public Visual3DConfiguration() {
            ShowInstructionWindow = true;
        }

        /// <summary>
        /// Creates a new instance of Configuration.
        /// </summary>
        /// <param name="showInstructionWindow">show Instruction-Window at the next start</param>
        public Visual3DConfiguration(bool showInstructionWindow) {
            ShowInstructionWindow = showInstructionWindow;
        }
    }
}