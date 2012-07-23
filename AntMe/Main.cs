using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using AntMe.Gui.net.antme.update;
using AntMe.Gui.Properties;
using AntMe.SharedComponents.Plugin;
using System.Reflection;

namespace AntMe.Gui {
    internal sealed partial class Main : Form {
        #region Variablen

        private readonly PluginManager manager;

        private PluginItem activeProducer;
        private readonly List<PluginItem> activeConsumers = new List<PluginItem>();
        private bool ignoreTimerEvents = false;
        private readonly bool initPhase = false;
        private bool restart = false;
        private readonly bool directstart = false;
        private bool updateChecked = false;
        private Version updateVersion;
        private string updateUrl = string.Empty;

        #endregion

        #region Konstruktor und Initialisierung

        public Main(string[] parameter) {
            initPhase = true;

            InitializeComponent();
            CreateHandle();

            // check Language-buttons
            switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName) {
                case "de":
                    germanMenuItem.Checked = true;
                    break;
                default:
                    englishMenuItem.Checked = true;
                    break;
            }

            // Load welcomepage
            try {
                infoWebBrowser.Navigate("file://" + Application.StartupPath + Resource.MainWelcomePagePath);
            }
            catch {}

            manager = new PluginManager();

            try {
                manager.LoadSettings();
            }
            catch (Exception ex) {
                ExceptionViewer problems = new ExceptionViewer(ex);
                problems.ShowDialog(this);
            }

            // Set Window-Position
            WindowState = Settings.Default.windowState;
            Location = Settings.Default.windowPosition;
            Size = Settings.Default.windowSize;

            manager.SearchForPlugins();
            timer.Enabled = true;

            // Forward startparameter
            foreach (PluginItem plugin in manager.ProducerPlugins) {
                plugin.Producer.StartupParameter(parameter);
            }
            foreach (PluginItem plugin in manager.ConsumerPlugins) {
                plugin.Consumer.StartupParameter(parameter);
            }

            foreach (string p in parameter) {
                if (p.ToUpper() == "/START") {
                    directstart = true;
                }
            }

            initPhase = false;
        }

        #endregion

        #region Frontend- und Interaktionshandling

        /// <summary>
        /// Make updates based on manager-settings
        /// </summary>
        private void updatePanel() {

            if (ignoreTimerEvents) {
                return;
            }

            ignoreTimerEvents = true;

            // Controlling-Buttons
            startMenuItem.Enabled = manager.CanStart;
            startToolItem.Enabled = manager.CanStart;
            pauseToolItem.Enabled = manager.CanPause;
            pauseMenuItem.Enabled = manager.CanPause;
            stopToolItem.Enabled = manager.CanStop;
            stopMenuItem.Enabled = manager.CanStop;

            newspanelMenuItem.Checked = Settings.Default.showNews;
            autoupdateMenuItem.Checked = Settings.Default.checkForUpdates;

            if (manager.FrameLimiterEnabled) {
                if ((int) Math.Round(manager.FrameLimit) <= 100) {
                    fasterToolItem.Enabled = true;
                }
                else {
                    fasterToolItem.Enabled = false;
                }
                if ((int) Math.Round(manager.FrameLimit) > 1) {
                    slowerToolItem.Enabled = true;
                }
                else {
                    slowerToolItem.Enabled = false;
                }

                speedDropDownToolItem.Text = string.Format(Resource.MainFramesPerSecond, manager.FrameLimit);
            }
            else {
                slowerToolItem.Enabled = false;
                fasterToolItem.Enabled = false;
                speedDropDownToolItem.Text = Resource.MainSpeedMaximal;
            }

            // producer-list
            List<PluginItem> producerList = new List<PluginItem>(manager.ProducerPlugins);
            for (int i = 0; i < producerComboBoxToolItem.Items.Count; i++) {
                PluginItem item = (PluginItem) producerComboBoxToolItem.Items[i];
                if (!producerList.Contains(item)) {
                    producerComboBoxToolItem.Items.Remove(item);
                    i--;
                }
            }
            foreach (PluginItem item in producerList) {
                if (!producerComboBoxToolItem.Items.Contains(item)) {
                    producerComboBoxToolItem.Items.Add(item);
                }
            }

            // manage tabs
            if (activeProducer != manager.ActiveProducerPlugin) {
                // Update Combobox
                producerComboBoxToolItem.SelectedItem = manager.ActiveProducerPlugin;

                // remove old tab
                if (activeProducer != null) {
                    if (activeProducer.Producer.Control != null) {
                        tabControl.TabPages.RemoveAt(1);
                    }
                    activeProducer = null;
                }

                // add new tab
                if (manager.ActiveProducerPlugin != null) {
                    if (manager.ActiveProducerPlugin.Producer.Control != null) {
                        TabPage page = new TabPage(manager.ActiveProducerPlugin.Name);
                        page.Controls.Add(manager.ActiveProducerPlugin.Producer.Control);
                        tabControl.TabPages.Insert(1, page);
                        manager.ActiveProducerPlugin.Producer.Control.Dock = DockStyle.Fill;
                    }
                    activeProducer = manager.ActiveProducerPlugin;
                }
            }

            // synchronize Consumer
            List<PluginItem> newActiveConsumers = new List<PluginItem>(manager.ActiveConsumerPlugins);
            for (int i = activeConsumers.Count - 1; i >= 0; i--) {
                // Kick the old tab
                if (!newActiveConsumers.Contains(activeConsumers[i])) {
                    if (tabControl.TabPages.ContainsKey(activeConsumers[i].Guid.ToString())) {
                        tabControl.TabPages.RemoveByKey(activeConsumers[i].Guid.ToString());
                    }
                    activeConsumers.Remove(activeConsumers[i]);
                }
            }
            foreach (PluginItem plugin in newActiveConsumers) {
                //Create new, if needed
                if (!activeConsumers.Contains(plugin)) {
                    // Create Tab and place control
                    if (plugin.Consumer.Control != null) {
                        tabControl.TabPages.Add(plugin.Guid.ToString(), plugin.Name);
                        tabControl.TabPages[plugin.Guid.ToString()].Controls.Add(plugin.Consumer.Control);
                        plugin.Consumer.Control.Dock = DockStyle.Fill;
                    }
                    activeConsumers.Add(plugin);
                }
            }

            // popup exceptions
            if (manager.Exceptions.Count > 0) {
                ExceptionViewer problems = new ExceptionViewer(manager.Exceptions);
                problems.ShowDialog(this);
                manager.Exceptions.Clear();
            }

            // StatusBar-information
            stateLabelBarItem.Text = string.Empty;
            switch (manager.State) {
                case PluginState.NotReady:
                    stateLabelBarItem.Text = Resource.MainStateNotReady;
                    break;
                case PluginState.Paused:
                    stateLabelBarItem.Text = Resource.MainStatePaused;
                    break;
                case PluginState.Ready:
                    stateLabelBarItem.Text = Resource.MainStateReady;
                    break;
                case PluginState.Running:
                    stateLabelBarItem.Text = Resource.MainStateRunning;
                    break;
            }

            if (manager.State == PluginState.Running || manager.State == PluginState.Paused) {
                progressBarItem.Maximum = manager.TotalRounds;
                progressBarItem.Value = manager.CurrentRound;
                stepCounterBarItem.Text = string.Format(Resource.MainStateRoundIndicator, manager.CurrentRound, manager.TotalRounds);
                progressBarItem.Visible = true;
                stepCounterBarItem.Visible = true;
            }
            else {
                progressBarItem.Visible = false;
                stepCounterBarItem.Visible = false;                
            }

            if (manager.State == PluginState.Running) {
                fpsBarItem.Text = manager.FrameRate.ToString(Resource.MainStateFramesPerSecond);
                fpsBarItem.Visible = true;
            }
            else {
                fpsBarItem.Visible = false;
            }

            // Autoupdater
            if (updateUrl != string.Empty) {
                if (MessageBox.Show(this, 
                    string.Format(Resource.MainUpdateMessage, Assembly.GetCallingAssembly().GetName().Version, updateVersion), 
                    Resource.MainUpdateTitle, MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Information, 
                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes) {

                    Help.ShowHelp(this, updateUrl);
                    // TODO: Exceptionhandling
                }
                updateUrl = string.Empty;
            }

            ignoreTimerEvents = false;
        }

        #endregion

        #region Formularfunktionen

        #region form

        private void form_shown(object sender, EventArgs e) {
            updatePanel();

            if (manager.Exceptions.Count > 0) {
                ExceptionViewer problems = new ExceptionViewer(manager.Exceptions);
                problems.ShowDialog(this);
                manager.Exceptions.Clear();
            }

            if (Settings.Default.firstStart)
            {
                UpdateRequest form = new UpdateRequest();
                form.ShowDialog(this);
            }

            if (Settings.Default.checkForUpdates && 
                Settings.Default.lastUpdateCheck + Settings.Default.updateCheckInterval < DateTime.Now)
            {
                Thread thread = new Thread(CheckUpdates);
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.Lowest;
                thread.Start();
            }

            autoupdateMenuItem.Checked = Settings.Default.checkForUpdates;
            newspanelMenuItem.Checked = Settings.Default.showNews;

            // force a direkt start, if manager is ready
            if (manager.CanStart && directstart) {
                start(sender, e);
            }
        }

        private void form_close(object sender, FormClosingEventArgs e) {
            if (manager.CanStop) {
                manager.Stop();
            }

            // Alle Plugin-Einstellungen absichern
            Settings.Default.Save();
            manager.SaveSettings();

            // show possible problems
            if (manager.Exceptions != null && manager.Exceptions.Count > 0) {
                ExceptionViewer form = new ExceptionViewer(manager.Exceptions);
                manager.Exceptions.Clear();
                form.ShowDialog(this);
            }
        }

        private void form_resize(object sender, EventArgs e)
        {
            if (initPhase)
            {
                return;
            }

            if (WindowState == FormWindowState.Normal)
            {
                Settings.Default.windowPosition = Location;
                Settings.Default.windowSize = Size;
            }

            if (WindowState != FormWindowState.Minimized)
            {
                Settings.Default.windowState = WindowState;
            }
        }

        #endregion

        #region tab

        private void tab_select(object sender, TabControlCancelEventArgs e) {
            if (e.TabPage.Tag != null) {
                manager.SetVisiblePlugin(((PluginItem) e.TabPage.Tag).Guid);
            }
            else {
                manager.SetVisiblePlugin(new Guid());
            }
        }

        #endregion

        #region buttons

        private void button_close(object sender, EventArgs e) {
            Close();
        }

        private void button_website(object sender, EventArgs e) {
            Help.ShowHelp(this, Resource.MainWebsiteLink);
        }

        private void button_plugins(object sender, EventArgs e) {
            ignoreTimerEvents = true;
            Plugins pluginForm = new Plugins(manager);
            pluginForm.ShowDialog(this);
            ignoreTimerEvents = false;
            updatePanel();
        }

        private void button_tutorials(object sender, EventArgs e) {
            // Es wurde Hilfe angefordert. Hier wird geprüft ob eine Hilfe verfügbar ist
            if (File.Exists(Resource.MainTutorialPath)) {
                Help.ShowHelp(this, Resource.MainTutorialPath);
            }
            else {
                MessageBox.Show(
                    this,
                    Resource.MainMessageBoxNoTutorialMessage,
                    Resource.MainMessageBoxNoTutorialTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void button_classDescription(object sender, EventArgs e) {
            // Klassen-Browser soll angezeigt werden
            if (File.Exists(Resource.MainClassDescriptionPath)) {
                Help.ShowHelp(this, Resource.MainClassDescriptionPath);
            }
            else {
                MessageBox.Show(
                    this,
                    Resource.MainMessageBoxNoClassdescriptionMessage,
                    Resource.MainMessageBoxNoClassdescriptionTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void button_info(object sender, EventArgs e) {
            InfoBox infoBox = new InfoBox();
            infoBox.ShowDialog(this);
        }

        private void button_forum(object sender, EventArgs e) {
            Help.ShowHelp(this, Resource.MainForumLink);
        }

        private void button_limitSetTo2(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 2.0f);
        }

        private void button_limitSetTo8(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 8.0f);
        }

        private void button_limitSetTo15(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 15.0f);
        }

        private void button_limitSetTo22(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 22.5f);
        }

        private void button_limitSetTo30(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 30.0f);
        }

        private void button_limitSetTo50(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 50.0f);
        }

        private void button_limitSetTo80(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 80.0f);
        }

        private void button_limitSetTo100(object sender, EventArgs e) {
            manager.SetSpeedLimit(true, 100.0f);
        }

        private void button_limitSetToMax(object sender, EventArgs e) {
            manager.SetSpeedLimit(false, 0.0f);
        }

        private void button_limitFaster(object sender, EventArgs e) {
            if (manager.FrameLimiterEnabled) {
                if ((int) Math.Round(manager.FrameLimit) < 100) {
                    manager.SetSpeedLimit(true, (int) Math.Round(manager.FrameLimit) + 1);
                }
                else {
                    manager.SetSpeedLimit(false, 0.0f);
                }
            }
        }

        private void button_limitSlower(object sender, EventArgs e) {
            if (manager.FrameLimiterEnabled && (int) Math.Round(manager.FrameLimit) > 1) {
                manager.SetSpeedLimit(true, (int) Math.Round(manager.FrameLimit) - 1);
            }
        }

        private void button_german(object sender, EventArgs e)
        {
            Settings.Default.culture = "de";
            Settings.Default.Save();
            restart = true;
            Close();
        }

        private void button_english(object sender, EventArgs e)
        {
            Settings.Default.culture = "en";
            Settings.Default.Save();
            restart = true;
            Close();
        }

        private void button_update(object sender, EventArgs e)
        {
            updateChecked = false;
            CheckUpdates();

            if (!updateChecked) {
                // TODO: Localize
                MessageBox.Show(
                    this,
                    "Der Updateservice ist zur Zeit leider nicht verfügbar. Versuchen Sie es zu einem späteren Zeitpunkt noch einmal oder informieren Sie sich direkt auf der AntMe!-Webseite.",
                    Resource.MainUpdateTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button_switchAutoupdate(object sender, EventArgs e)
        {
            Settings.Default.checkForUpdates = !Settings.Default.checkForUpdates;
        }

        private void button_switchNewspanel(object sender, EventArgs e)
        {
            Settings.Default.showNews = !Settings.Default.showNews;
        }

        #endregion

        #region combos

        private void combo_producer(object sender, EventArgs e) {
            if (ignoreTimerEvents) {
                return;
            }

            ignoreTimerEvents = true;
            if (producerComboBoxToolItem.SelectedItem != null) {
                PluginItem plugin = (PluginItem) producerComboBoxToolItem.SelectedItem;
                manager.ActivateProducer(plugin.Guid);
            }
            else {
                manager.ActivateProducer(new Guid());
            }
            updatePanel();
            ignoreTimerEvents = false;
        }

        #endregion

        #region timer

        private void timer_tick(object sender, EventArgs e) {
            if (!ignoreTimerEvents) {
                updatePanel();
            }
        }

        #endregion

        #endregion

        #region Managersteuerung

        private void start(object sender, EventArgs e) {
            if (manager.CanStart) {
                manager.Start();

                // Aktives Eingangsplugin anzeigen
                if (activeProducer.Producer.Control != null) {
                    tabControl.SelectedIndex = 1;
                }
            }
        }

        private void stop(object sender, EventArgs e) {
            if (manager.CanStop) {
                manager.Stop();
            }
        }

        private void pause(object sender, EventArgs e) {
            if (manager.CanPause) {
                manager.Pause();
            }
        }

        #endregion

        public bool Restart {
            get { return restart; }
        }

        public void CheckUpdates() {
            updateChecked = false;
            try {
                Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                UpdateService updateService = new UpdateService();
                UpdateInformation updateInformation = updateService.CheckForUpdate(currentVersion.ToString());
                updateVersion = new Version(updateInformation.Version);

                Settings.Default.lastUpdateCheck = DateTime.Now;
                if (updateVersion > currentVersion) {
                    updateUrl = updateInformation.InfoLink;
                }
                updateChecked = true;
            }
            catch (Exception) {
                updateUrl = string.Empty;
            }
        }
    }
}