﻿using System;
using System.Drawing;

using DevExpress.XtraBars;

using SoftTeam.SoftBar.Core.Forms;
using SoftTeam.SoftBar.Core.Misc;
using SoftTeam.SoftBar.Core.Settings;
using SoftTeam.SoftBar.Core.Xml;

namespace SoftTeam.SoftBar.Core.SoftBar
{
    public class SoftBarMenuItem : SoftBarBaseItem
    {
        #region Fields
        private int _width;
        private int _left;
        private BarButtonItem _item = null;
        private CommandLineHelper _commandLine = null;
        #endregion

        #region Constructor
        // Used for tools
        public SoftBarMenuItem(MainAppBarForm form, Tool tool, bool systemMenu = false) : base(form, tool.Name, systemMenu)
        {
            _commandLine = new CommandLineHelper();
            IconPath = tool.IconPath;
            BeginGroup = tool.BeginGroup;
            ApplicationPath = tool.Path;
            Parameters = tool.Parameters;
        }

        // Used for system items
        public SoftBarMenuItem(MainAppBarForm form, string name, bool systemMenu = true) : base(form, name, systemMenu)
        {
            _commandLine = new CommandLineHelper();
        }

        // Used for user menu items
        public SoftBarMenuItem(MainAppBarForm form, XmlMenuItem menuItem, bool systemMenu = false) : base(form, menuItem.Name, systemMenu)
        {
            _commandLine = new CommandLineHelper();
            IconPath = menuItem.IconPath;
            ApplicationPath = menuItem.ApplicationPath;
            DocumentPath = menuItem.DocumentPath;
            Parameters = menuItem.Parameters;
            BeginGroup = menuItem.BeginGroup;
            RunAsAdministrator = menuItem.RunAsAdministrator;
        }
        #endregion

        #region Properties
        public string DocumentPath { get => _commandLine.Document; set => _commandLine.Document = value; }
        public string ApplicationPath
        {
            get => _commandLine.Application;
            set { _commandLine.Application = value; if (string.IsNullOrEmpty(IconPath)) IconPath = value; }
        }
        public string Parameters { get => _commandLine.Parameters; set => _commandLine.Parameters = value; }
        public bool RunAsAdministrator { get => _commandLine.RunAsAdministrator; set => _commandLine.RunAsAdministrator = value; }
        public int Width { get => _width; set => _width = value; }
        public int Left { get => _left; set => _left = value; }

        public BarButtonItem Item { get => _item; set => _item = value; }
        #endregion

        #region Setup
        public BarButtonItem Setup()
        {
            // Create the BarButtonIem
            Item = new BarButtonItem();
            Item.Manager = Form.barManagerSoftBar;
            Item.Caption = Name;

            // Associate the BarButtonItem with the MenuItem, used when clicked
            Item.Tag = this;

            if (SystemMenu) return Item;

            Item.ItemClick += Item_ItemClick;

            try
            {
                // Set the image 
                Item.ImageOptions.Image = Image;
            }
            catch (Exception e)
            {
                // No icon on error
                WarningText = $"Unknown icon exception! : \n\n{e.Message}";
                Warning = true;
            }

            // If we have a warning...
            if (Warning)
            {
                // Create a tool tip and set the warning image
                Item.SuperTip = HelperFunctions.CreateWarningToolTip(WarningText);
                Item.ImageOptions.Image = new Bitmap(Properties.Resources.Warning_small);
            }

            return Item;
        }
        #endregion

        #region Events
        private void Item_ItemClick(object sender, ItemClickEventArgs e)
        {
            _commandLine.Execute();
        }
        #endregion
    }
}
