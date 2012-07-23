using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

using AntMe.SharedComponents.Plugin;
using AntMe.SharedComponents.States;

namespace AntMe.Plugin.Mdx {
    /// <summary>
    /// 3D-Visualizer for AntMe!-Simulations.
    /// </summary>
    [Preselected]
    public sealed class Plugin : IConsumerPlugin {
        #region private Variables

        private readonly string name = Resource.PluginName;
        private readonly string description = Resource.PluginDescription;
        private readonly Version version = Assembly.GetExecutingAssembly().GetName().Version;
        private readonly Guid guid = new Guid("bf4a95c0-8008-4bd4-8ba4-1f96eee22d95");

        private readonly InstructionPanel control;
        private readonly RenderForm renderForm;

        private bool running;
        private bool paused;

        private Visual3DConfiguration config = new Visual3DConfiguration();

        #endregion

        #region Construction and init

        /// <summary>
        /// Creates a new instance of Plugin
        /// </summary>
        public Plugin() {
            renderForm = new RenderForm();
            control = new InstructionPanel();
        }

        #endregion

        #region IPlugIn Member

        /// <summary>
        /// Starts rendering
        /// </summary>
        public void Start() {
            if (State == PluginState.Ready || State == PluginState.Paused) {
                // If needed, show Instruction-Window.
                if (config.ShowInstructionWindow) {
                    InstructionForm instructions = new InstructionForm();
                    config.ShowInstructionWindow = (instructions.ShowDialog(control) == DialogResult.Retry);
                }

                // Start rendierung
                renderForm.Show();
                renderForm.Init();
                running = true;
                paused = false;
            }
        }

        /// <summary>
        /// Stops rendering
        /// </summary>
        public void Stop() {
            if (State == PluginState.Running || State == PluginState.Paused) {
                renderForm.Uninit();
                renderForm.Hide();
                running = false;
                paused = false;
            }
        }

        /// <summary>
        /// suspend rendering
        /// </summary>
        public void Pause() {
            Start();
            if (State == PluginState.Running) {
                paused = true;
            }
        }

        /// <summary>
        /// Gives an interrupt back to main application
        /// </summary>
        public bool Interrupt {
            get {
                return !renderForm.Visible;
            }
        }

        /// <summary>
        /// Gets current state of plugin
        /// </summary>
        public PluginState State {
            get {
                if (running) {
                    if (paused) {
                        return PluginState.Paused;
                    }
                    else {
                        return PluginState.Running;
                    }
                }
                else {
                    return PluginState.Ready;
                }
            }
        }

        /// <summary>
        /// Gets user-control for antme-tab
        /// </summary>
        public Control Control {
            get { return control; }
        }

        /// <summary>
        /// Gets or sets settings for this plugin.
        /// </summary>
        public byte[] Settings {
            get {
                XmlSerializer serializer = new XmlSerializer(typeof (Visual3DConfiguration));
                MemoryStream puffer = new MemoryStream();
                serializer.Serialize(puffer, config);
                return puffer.ToArray();
            }
            set {
                if (value != null && value.Length > 0) {
                    XmlSerializer serializer = new XmlSerializer(typeof (Visual3DConfiguration));
                    MemoryStream puffer = new MemoryStream(value);
                    config = (Visual3DConfiguration) serializer.Deserialize(puffer);
                }
            }
        }

        /// <summary>
        /// Sends startup-parameter to this plugin.
        /// </summary>
        /// <param name="parameter">startup-parameter</param>
        public void StartupParameter(string[] parameter) {}

        /// <summary>
        /// Tells the plugin the visibility of user-control.
        /// </summary>
        /// <param name="visible">is visible</param>
        public void SetVisibility(bool visible) {}

        /// <summary>
        /// Call for user-interface-updates.
        /// </summary>
        /// <param name="state">current state</param>
        public void UpdateUI(SimulationState state) {
            renderForm.SimulationState = state;
        }

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        public string Name {
            get { return name; }
        }

        /// <summary>
        /// Gets the description for that plugin.
        /// </summary>
        public string Description {
            get { return description; }
        }

        /// <summary>
        /// Gets the version of that plugin.
        /// </summary>
        public Version Version {
            get { return version; }
        }

        /// <summary>
        /// Gets the Guid of that plugin.
        /// </summary>
        public Guid Guid {
            get { return guid; }
        }

        #endregion

        #region IConsumerPlugin Member

        /// <summary>
        /// First push of new state to this consumer.
        /// </summary>
        /// <param name="state">state</param>
        public void CreatingState(ref SimulationState state) {}

        /// <summary>
        /// Second push of new state to this consumer.
        /// </summary>
        /// <param name="state">state</param>
        public void CreateState(ref SimulationState state) {}

        /// <summary>
        /// third push of new state to this consumer.
        /// </summary>
        /// <param name="state"></param>
        public void CreatedState(ref SimulationState state) {}

        #endregion
    }
}