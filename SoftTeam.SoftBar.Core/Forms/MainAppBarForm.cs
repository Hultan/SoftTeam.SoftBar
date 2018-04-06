﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftTeam.SoftBar.Core.Misc;
using SoftTeam.SoftBar.Core.SoftBar;
using SoftTeam.SoftBar.Core.Xml;

namespace SoftTeam.SoftBar.Core.Forms
{
    public partial class MainAppBarForm : DevExpress.XtraEditors.XtraForm
    {
        #region Fields
        private SoftBarManager _manager = null;
        private AppBar _appBar;
        #endregion

        #region Properties
        public SoftBarManager Manager { get => _manager; set => _manager = value; }
        #endregion

        #region Clipboard
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private static int WM_HOTKEY = 0x0312;

        /// <summary>
        /// The enumeration of possible modifiers.
        /// </summary>
        [Flags]
        public new enum ModifierKeys : uint
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }
        #endregion

        #region Constructor
        public MainAppBarForm()
        {
            InitializeComponent();
            _appBar = new AppBar();
        }
        #endregion

        #region Load and closing
        private void MainAppBarForm_Load(object sender, EventArgs e)
        {
            bool exit = false;
            bool newUser = false;
            UserTypeEnum userType = UserTypeEnum.None;

            // Get the path for the xml file
            var path = HelperFunctions.GetWorkingDirectory();

            // First time user
            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(System.IO.Path.Combine(path, "menu.xml")))
            {
                using (StartupForm form = new StartupForm())
                {
                    form.ShowDialog();

                    userType = form.UserType;

                    switch (userType)
                    {
                        case UserTypeEnum.None:
                            exit = true;
                            break;
                        case UserTypeEnum.FirstTimeUser:
                            newUser = true;
                            path = ChooseWorkingDirectory();
                            break;
                        case UserTypeEnum.PHSAppBarUser:
                            newUser = true;
                            path = ChooseWorkingDirectory();
                            var success = ImportFromPHSAppBar(path);
                            if (!success)
                                exit = true;
                            break;
                        case UserTypeEnum.Wizard:
                            var fileName = ChooseMenuXmlPath();
                            if (string.IsNullOrEmpty(fileName))
                                exit = true;
                            else
                                path = System.IO.Path.GetDirectoryName(fileName);
                            break;
                    }
                }
            }

            if (string.IsNullOrEmpty(path))
                exit = true;

            // If the user has cancelled the initial dialogs, let's quit...
            if (exit)
            {
                this.Close();
                return;
            }

            // Set up the app bar at the top of the screen
            _appBar.RegisterBar(this);

            // Save the path (working folder) for the xml file
            if (HelperFunctions.GetWorkingDirectory() != path)
                HelperFunctions.SetWorkingDirectory(path);

            // Create the app bar from XML
            _manager = new SoftBarManager(this, path);

            // Register global hotkeys (Clipboard etc)
            RegisterHotKeys();

            if (newUser)
            {
                var header = $"SoftBar - New user";
                var message = $"Since you are a new SoftBar user, it is recommended" + Environment.NewLine +
                              "that you check out <b>SoftBar/Settings</b> to set up <b>SoftBar</b>," + Environment.NewLine +
                              "and <b>SoftBar/Customize</b> to create your own menus!";
                XtraMessageBox.Show(message, header, MessageBoxButtons.OK, MessageBoxIcon.Information, DevExpress.Utils.DefaultBoolean.True);
            }
        }

        private void MainAppBarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKeys();
            _appBar.RegisterBar(this);
        }
        #endregion

        #region Misc functions
        private bool ImportFromPHSAppBar(string workingDirectory)
        {
            openFileDialogSoftBar.FileName = "Config.ini";
            openFileDialogSoftBar.CheckFileExists = true;
            openFileDialogSoftBar.Title = "Open PHSAppBar config.ini";
            DialogResult result = openFileDialogSoftBar.ShowDialog();
            if (result == DialogResult.Cancel)
                return false;

            // Import from PHSAppBar config.ini
            XmlArea area = null;
            using (PHSAppBarImporter importer = new PHSAppBarImporter(openFileDialogSoftBar.FileName))
                area = importer.Import();

            // Save xml
            using (XmlSaver saver = new XmlSaver(area, System.IO.Path.Combine(workingDirectory, "menu.xml")))
                saver.Save();

            return true;
        }

        private string ChooseMenuXmlPath()
        {
            openFileDialogSoftBar.FileName = "menu.xml";
            openFileDialogSoftBar.CheckFileExists = true;
            openFileDialogSoftBar.Title = "Open SoftBarmenu.xml";
            DialogResult result = openFileDialogSoftBar.ShowDialog();

            if (result == DialogResult.Cancel)
                return "";
            else
                return openFileDialogSoftBar.FileName;
        }

        private string ChooseWorkingDirectory()
        {
            using (ChooseDirectoryForm form = new ChooseDirectoryForm())
            {
                form.ShowDialog();

                return form.Path;
            }
        }
        #endregion

        #region Register/unregister hotkeys
        private void RegisterHotKeys()
        {
            RegisterHotKey(this.Handle, this.GetType().GetHashCode(), (int)(ModifierKeys.Shift | ModifierKeys.Control), 0x43);//Set hotkey as Win + 'c'
        }

        private void UnregisterHotKeys()
        {
            UnregisterHotKey(this.Handle, this.GetType().GetHashCode());
        }
        #endregion

        #region Overrides
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            _appBar.WndProc(this, ref m);

            // check if we got a hot key pressed.
            if (m.Msg == WM_HOTKEY)
            {
                // get the keys.
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                if (modifier == (ModifierKeys.Shift | ModifierKeys.Control) && key == Keys.C)
                    _manager.ClipboardManager.HotKeyClicked(MousePosition);
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= (~0x00C00000); // WS_CAPTION
                cp.Style &= (~0x00800000); // WS_BORDER
                cp.ExStyle = 0x00000080 | 0x00000008; // WS_EX_TOOLWINDOW | WS_EX_TOPMOST
                return cp;
            }
        }
        #endregion
    }
}