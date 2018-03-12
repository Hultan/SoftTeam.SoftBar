﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

using DevExpress.XtraBars;

using SoftTeam.SoftBar.Core.Extensions;
using SoftTeam.SoftBar.Core.Helpers;
using SoftTeam.SoftBar.Core.Forms;

namespace SoftTeam.SoftBar.Core
{
    public class SoftBarMenuItem : SoftBarBaseItem
    {
        #region Fields
        private int _width;
        private int _left;
        private BarStaticItem _item = null;
        private CommandLineHelper _commandLine = null;
        #endregion

        #region Constructor
        public SoftBarMenuItem(MainAppBarForm form, string name, bool systemMenu = false) : base(form, name, systemMenu)
        {
            _commandLine = new CommandLineHelper();
        }
        #endregion

        #region Properties
        public string DocumentPath { get => _commandLine.Document; set => _commandLine.Document = value; }
        public string ApplicationPath {
            get => _commandLine.Application;
            set { _commandLine.Application = value; if (string.IsNullOrEmpty(IconPath)) IconPath = value; }
        }
        public string Parameters { get => _commandLine.Parameters; set=>_commandLine.Parameters = value; }
        public int Width { get => _width; set => _width = value; }
        public int Left { get => _left; set => _left = value; }

        public BarStaticItem Item { get => _item; set => _item = value; }
        #endregion

        #region Setup
        public BarStaticItem Setup()
        {
            // Create the BarButtonIem
            Item = new BarStaticItem();
            Item.Manager = Form.barManagerSoftBar;
            Item.Caption = Name;

            // Associate the BarButtonItem with the MenuItem, used when clicked
            Item.Tag = this;
            Item.ItemClick += Item_ItemClick;

            if (SystemMenu) return Item;

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
                Item.SuperTip = ToolTipHelper.CreateWarningToolTip(WarningText);
                Item.ImageOptions.Image = new Bitmap(Properties.Resources.Warning_small);
            }

            return Item;
        }
        #endregion

        #region Events
        private void Item_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get the menu item that was clicked
            var menuItem = (SoftBarMenuItem)e.Item.Tag;

            if (_commandLine.CanExecute())
                _commandLine.Execute();
        }
        #endregion
    }
}
