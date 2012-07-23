using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using AntMe.SharedComponents.States;

using Resource=AntMe.Plugin.Mdx.Resource;

using System.Diagnostics;

namespace AntMe.Plugin.Mdx {
    using SlimDX;
    using SlimDX.Direct3D9;
    using SlimDX.DirectInput;

    using Capabilities = SlimDX.Direct3D9.Capabilities;
    using Device = SlimDX.Direct3D9.Device;
    using DeviceType = SlimDX.Direct3D9.DeviceType;
    using FillMode = SlimDX.Direct3D9.FillMode;

    internal partial class RenderForm : Form {
        #region Constants

        private const float VIEWRANGE_MAX = 50000.0f;
        private const float VIEWRANGE_MIN = 1.0f;

        #endregion

        private readonly PresentParameters presentParameters;
        private Device renderDevice;
        private Capabilities deviceCaps;

        private readonly string[] names;
        private readonly Random random = new Random();
        private readonly Dictionary<int, string> antNames = new Dictionary<int, string>();

        private Matrix projectionMatrix;

        private Camera camera;
        private ModellManager modelManager;
        Stopwatch watch = new Stopwatch();

        private SimulationState simulationState;
        SlimDX.Direct3D9.Direct3D Manager = new Direct3D();

        public RenderForm() {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            watch.Start();

            // Read list of names
            names = Models.vornamen.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            // Read device-caps
            deviceCaps = Manager.GetDeviceCaps(0, DeviceType.Hardware);

            // Setup render-device
            presentParameters = new PresentParameters();
            presentParameters.Windowed = true;
            presentParameters.SwapEffect = SwapEffect.Discard;
            presentParameters.PresentationInterval = PresentInterval.Immediate;
            presentParameters.BackBufferFormat = Manager.Adapters[0].CurrentDisplayMode.Format;
            presentParameters.BackBufferCount = 1;
            presentParameters.EnableAutoDepthStencil = true;
            presentParameters.AutoDepthStencilFormat = Format.D16; 

            // If possible, use bether depth-buffer
            if (
                Manager.CheckDepthStencilMatch
                    (
                    0,
                    DeviceType.Hardware,
                    Manager.Adapters[0].CurrentDisplayMode.Format,
                    Manager.Adapters[0].CurrentDisplayMode.Format,
                    Format.D24X8)) {
                presentParameters.AutoDepthStencilFormat = Format.D24X8;
            }

            // Check for multisampling
            if (
                Manager.CheckDeviceMultisampleType
                    (
                    0,
                    DeviceType.Hardware,
                    Manager.Adapters[0].CurrentDisplayMode.Format,
                    true,
                    MultisampleType.TwoSamples)) {
                presentParameters.Multisample = MultisampleType.TwoSamples;
            }
        }

        /// <summary>
        /// Initialize the RenderDevice.
        /// </summary>
        public void Init() {
            // Fenstergr��e anpassen
            presentParameters.BackBufferHeight = ClientSize.Height;
            presentParameters.BackBufferWidth = ClientSize.Width;

            // Device erzeugen
            CreateFlags flags;
            if (deviceCaps.DeviceCaps.HasFlag(DeviceCaps.HWTransformAndLight)) {
                flags = CreateFlags.HardwareVertexProcessing;
            }
            else {
                flags = CreateFlags.SoftwareVertexProcessing;
            }
            if (deviceCaps.DeviceCaps.HasFlag(DeviceCaps.PureDevice)) {
                flags |= CreateFlags.PureDevice;
            }

            // Versuche ein DirectX-Device zu erzeugen. Falls dies fehlschl�gt wird alternativ ein
            // Referenz-Device erzeugt.
            try {
                renderDevice = new Device(Manager, 0, DeviceType.Hardware, this.Handle, flags, presentParameters);
            }
            catch (Exception) {
                renderDevice =
                    new Device
                        (Manager, 0, DeviceType.Reference, this.Handle, CreateFlags.SoftwareVertexProcessing, presentParameters);
            }

            // Events abgreifen
            // BUG: POSSIBLE BUG
            ////renderDevice.DeviceReset += device_reset;

            // Kamera erstellen
            camera = new Camera(this);

            // Grundeinstellungen des Ger�ts laden
            device_reset(null, null);

            // Initialisieren von Manager und Kamera
            modelManager = new ModellManager(renderDevice);
        }

        /// <summary>
        /// Uninit the RenderDevice.
        /// </summary>
        public void Uninit() {
            // Switch back to window-Screen
            Fullscreen = false;

            // free model-resources
            if (modelManager != null) {
                modelManager.Dispose();
                modelManager = null;
            }

            // free Device
            if (renderDevice != null) {
                // BUG: POSSIBLE BUG
                //renderDevice.DeviceReset -= device_reset;
                renderDevice.Dispose();
                renderDevice = null;
            }
        }

        private void render(object sender, PaintEventArgs e) {
            if (Visible && renderDevice != null) {

                if (watch.ElapsedMilliseconds > 40) {
                    watch.Reset();
                    watch.Start();

                    Selection selectedItem = new Selection();

                    // Selektionsinfos zur�cksetzen
                    selectedItem.SelectionType = SelectionType.Nothing;
                    selectedItem.Item = null;
                    float distanceToSelectedItem = VIEWRANGE_MAX*VIEWRANGE_MAX;

                    renderDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.FromArgb(51, 153, 255), 1.0f, 0);
                    renderDevice.BeginScene();

                    //// Falls schon ein Zustand da ist kann gezeichnet werden
                    if (simulationState != null) {
                        SimulationState currentState = simulationState;

                        // Update Camera
                        camera.Update(currentState.PlaygroundWidth, currentState.PlaygroundHeight);
                        renderDevice.SetTransform(TransformState.View, camera.ViewMatrix);
                        Pickray pickray = camera.Pickray;
                        Point mousePosition = camera.MousePosition;

                        // render Playerground
                        modelManager.SetPlaygroundSize(currentState.PlaygroundWidth, currentState.PlaygroundHeight);
                        modelManager.RenderPlayground();

                        // render these preaty little, blue items...
                        float distance;
                        for (int i = 0; i < currentState.BugStates.Count; i++) {
                            if ((distance = modelManager.RenderBug(currentState.BugStates[i], pickray, false)) > 0) {
                                // select, if pickray collides with item
                                if (distance < distanceToSelectedItem) {
                                    distanceToSelectedItem = distance;
                                    selectedItem.Item = currentState.BugStates[i];
                                    selectedItem.SelectionType = SelectionType.Bug;
                                }
                            }
                        }

                        // Render sugar
                        for (int i = 0; i < currentState.SugarStates.Count; i++) {
                            if ((distance = modelManager.RenderSugar(currentState.SugarStates[i], pickray, false)) >
                                0) {
                                // select, if pickray collides with item
                                if (distance < distanceToSelectedItem) {
                                    distanceToSelectedItem = distance;
                                    selectedItem.Item = currentState.SugarStates[i];
                                    selectedItem.SelectionType = SelectionType.Sugar;
                                }
                            }
                        }

                        // Render Fruit
                        for (int i = 0; i < currentState.FruitStates.Count; i++) {
                            if ((distance = modelManager.RenderFruit(currentState.FruitStates[i], pickray, false)) >
                                0) {
                                // select, if pickray collides with item
                                if (distance < distanceToSelectedItem) {
                                    distanceToSelectedItem = distance;
                                    selectedItem.Item = currentState.FruitStates[i];
                                    selectedItem.SelectionType = SelectionType.Fruit;
                                }
                            }
                        }

                        // Colony-specific stuff
                        int count = 0;
                        for (int teamIndex = 0; teamIndex < currentState.TeamStates.Count; teamIndex++) {
                            for (int colonyIndex = 0;
                                 colonyIndex < currentState.TeamStates[teamIndex].ColonyStates.Count;
                                 colonyIndex++) {
                                ColonyState colony = currentState.TeamStates[teamIndex].ColonyStates[colonyIndex];

                                // Ensure available materials for that colony
                                modelManager.PrepareColony(count);

                                // Render Anthills
                                for (int anthillIndex = 0; anthillIndex < colony.AnthillStates.Count; anthillIndex++) {
                                    if (
                                        (distance =
                                         modelManager.RenderAnthill(
                                             count,
                                             colony.AnthillStates[anthillIndex],
                                             pickray,
                                             false)) >
                                        0) // select, if pickray collides with item
                                    {
                                        if (distance < distanceToSelectedItem) {
                                            distanceToSelectedItem = distance;
                                            selectedItem.Item = colony.AnthillStates[anthillIndex];
                                            selectedItem.SelectionType = SelectionType.Anthill;
                                            selectedItem.AdditionalInfo = colony.ColonyName;
                                        }
                                    }
                                }

                                // Render Ants
                                for (int antIndex = 0; antIndex < colony.AntStates.Count; antIndex++) {
                                    if (
                                        (distance =
                                         modelManager.RenderAnt(
                                             count,
                                             colony.AntStates[antIndex],
                                             pickray,
                                             false)) > 0) {
                                        // select, if pickray collides with item
                                        if (distance < distanceToSelectedItem) {
                                            distanceToSelectedItem = distance;
                                            selectedItem.Item = colony.AntStates[antIndex];
                                            selectedItem.SelectionType = SelectionType.Ant;
                                            selectedItem.AdditionalInfo = colony.ColonyName;
                                        }
                                    }
                                }

                                count++;
                            }
                        }

                        // Render Marker
                        // This must happen at the end, cause of alpha-tranperency
                        count = 0;
                        for (int teamIndex = 0; teamIndex < currentState.TeamStates.Count; teamIndex++) {
                            TeamState team = currentState.TeamStates[teamIndex];
                            for (int colonyIndex = 0; colonyIndex < team.ColonyStates.Count; colonyIndex++) {
                                ColonyState colony = team.ColonyStates[colonyIndex];
                                for (int markerIndex = 0; markerIndex < colony.MarkerStates.Count; markerIndex++) {
                                    MarkerState marker = colony.MarkerStates[markerIndex];
                                    modelManager.RenderMarker(count, marker);
                                }
                                count++;
                            }
                        }

                        // Render Statistics in the upper left corner
                        modelManager.RenderInfobox(currentState);

                        // Render Info-Tag at selected item
                        if (selectedItem.SelectionType != SelectionType.Nothing) {
                            string line1;
                            string line2;
                            switch (selectedItem.SelectionType) {
                                case SelectionType.Ant:

                                    AntState ameise = (AntState) selectedItem.Item;
                                    string name;
                                    if (!antNames.ContainsKey(ameise.Id)) {
                                        name = names[random.Next(names.Length)];
                                        antNames.Add(ameise.Id, name);
                                    }
                                    else {
                                        name = antNames[ameise.Id];
                                    }

                                    line1 = string.Format(Resource.HovertextAntLine1, name, selectedItem.AdditionalInfo);
                                    line2 = string.Format(Resource.HovertextAntLine2, ameise.Vitality);
                                    break;
                                case SelectionType.Anthill:
                                    line1 = Resource.HovertextAnthillLine1;
                                    line2 = string.Format(Resource.HovertextAnthillLine2, selectedItem.AdditionalInfo);
                                    break;
                                case SelectionType.Bug:
                                    BugState bugState = (BugState) selectedItem.Item;
                                    line1 = Resource.HovertextBugLine1;
                                    line2 = string.Format(Resource.HovertextBugLine2, bugState.Vitality);
                                    break;
                                case SelectionType.Fruit:
                                    FruitState fruitState = (FruitState) selectedItem.Item;
                                    line1 = Resource.HovertextFruitLine1;
                                    line2 = string.Format(Resource.HovertextFruitLine2, fruitState.Amount);
                                    break;
                                case SelectionType.Sugar:
                                    SugarState sugar = (SugarState) selectedItem.Item;
                                    line1 = Resource.HovertextSugarLine1;
                                    line2 = string.Format(Resource.HovertextSugarLine2, sugar.Amount);
                                    break;
                                default:
                                    line1 = String.Empty;
                                    line2 = String.Empty;
                                    break;
                            }

                            // Text an Mausposition ausgeben
                            if (line1 != String.Empty || line2 != String.Empty) {
                                modelManager.RenderInfoTag(mousePosition, line1, line2);
                            }
                        }
                    }

                    renderDevice.EndScene();
                    renderDevice.Present();
                }

                Application.DoEvents();
                Invalidate();
            }
        }

        #region Device-Events

        private void device_reset(object sender, EventArgs e) {
            renderDevice.SetRenderState(RenderState.ZWriteEnable, true);
            renderDevice.SetRenderState(RenderState.CullMode, Cull.Counterclockwise);
            renderDevice.SetRenderState(RenderState.Lighting, true);
            renderDevice.SetRenderState(RenderState.FillMode, FillMode.Solid);

            // Matrizen zur Objekttransformation festlegen
            renderDevice.SetTransform(TransformState.World, Matrix.Identity);
            renderDevice.SetTransform(TransformState.View, camera.ViewMatrix);
            renderDevice.SetTransform(TransformState.Projection,
                projectionMatrix =
                Matrix.PerspectiveFovLH
                    (
                    (float) Math.PI/4,
                    ClientSize.Width/(float) ClientSize.Height,
                    VIEWRANGE_MIN,
                    VIEWRANGE_MAX));

            // Licht einschalten
            var light = new Light(); 
            light.Type = LightType.Directional;
            light.Ambient = Color.FromArgb(60, 60, 60);
            light.Diffuse = Color.Black;
            light.Specular = Color.FromArgb(100, 100, 100);
            light.Direction = Vector3.Normalize(new Vector3(-1.0f, -1.0f, 1.0f));
            renderDevice.SetLight(0, light);

            renderDevice.SetRenderState(RenderState.NormalizeNormals, true);
            renderDevice.SetRenderState(RenderState.AmbientMaterialSource, ColorSource.Material);
            renderDevice.SetRenderState(RenderState.DiffuseMaterialSource, ColorSource.Material);
            renderDevice.SetRenderState(RenderState.SpecularMaterialSource, ColorSource.Material);
            renderDevice.SetRenderState(RenderState.SpecularEnable, true);
            renderDevice.SetRenderState(RenderState.ShadeMode, ShadeMode.Gouraud);

            // Models im Manager neu laden
            if (modelManager != null) {
                modelManager.DeviceReset();
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the window-state of FullScreen-mode.
        /// </summary>
        public bool Fullscreen {
            get {
                if (renderDevice != null) {
                    return (WindowState == FormWindowState.Maximized);
                }
                else {
                    return false;
                }
            }
            set {
                if (Fullscreen != value && renderDevice != null) {
                    if (value) {
                        // Switch to fullscreen
                        FormBorderStyle = FormBorderStyle.None;
                        WindowState = FormWindowState.Maximized;
                        TopMost = true;
                    }
                    else {
                        // Switch to window-mode
                        WindowState = FormWindowState.Normal;
                        FormBorderStyle = FormBorderStyle.SizableToolWindow;
                        TopMost = false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current ProjectionMatrix
        /// </summary>
        public Matrix ProjectionMatrix {
            get { return projectionMatrix; }
        }

        /// <summary>
        /// Gets or sets the current SimulationState.
        /// </summary>
        public SimulationState SimulationState {
            get { return simulationState; }
            set { simulationState = value; }
        }

        private void doubleclick(object sender, EventArgs e) {
            Fullscreen = !Fullscreen;
        }

        private void form_closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}