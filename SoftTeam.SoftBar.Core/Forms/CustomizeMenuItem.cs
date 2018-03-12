﻿using System;

using SoftTeam.SoftBar.Core.Misc;

namespace SoftTeam.SoftBar.Core.Forms
{
    public partial class CustomizeMenuItem : DevExpress.XtraEditors.XtraForm
    {
        private MenuItemType _type = MenuItemType.None;

        private SoftBarMenu _menu = null;
        private SoftBarHeaderItem _headerItem = null;
        private SoftBarSubMenu _subMenu = null;
        private SoftBarMenuItem _menuItem = null;

        public CustomizeMenuItem(SoftBarMenu menu)
        {
            InitializeComponent();

            _type = MenuItemType.Menu;
            editMenu.BringToFront();

            editMenu.Name = menu.Name;
            editMenu.IconPath = menu.IconPath;
            editMenu.BeginGroup = menu.BeginGroup;
            _menu = menu;

            editMenu.LoadValues();
        }

        public CustomizeMenuItem(SoftBarHeaderItem headerItem)
        {
            InitializeComponent();

            _type = MenuItemType.HeaderItem;
            editHeaderItem.BringToFront();

            editHeaderItem.Name = headerItem.Name;
            editHeaderItem.BeginGroup = headerItem.BeginGroup;
            _headerItem = headerItem;

            editHeaderItem.LoadValues();
        }

        public CustomizeMenuItem(SoftBarSubMenu subMenu)
        {
            InitializeComponent();

            _type = MenuItemType.SubMenu;
            editSubMenu.BringToFront();

            editSubMenu.Name = subMenu.Name;
            editSubMenu.IconPath = subMenu.IconPath;
            editSubMenu.BeginGroup = subMenu.BeginGroup;
            _subMenu = subMenu;

            editSubMenu.LoadValues();
        }

        public CustomizeMenuItem(SoftBarMenuItem menuItem)
        {
            InitializeComponent();

            _type = MenuItemType.MenuItem;
            editMenuItem.BringToFront();

            editMenuItem.Name = menuItem.Name;
            editMenuItem.IconPath = menuItem.IconPath;
            editMenuItem.BeginGroup = menuItem.BeginGroup;

            editMenuItem.ApplicationPath = menuItem.ApplicationPath;
            editMenuItem.DocumentPath = menuItem.DocumentPath;
            editMenuItem.Parameters = menuItem.Parameters;
            _menuItem = menuItem;

            editMenuItem.LoadValues();
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            switch (_type)
            {
                case MenuItemType.Menu:
                    editMenu.SaveValues();

                    _menu.Name = editMenu.Name;
                    _menu.IconPath = editMenu.IconPath;
                    _menu.BeginGroup = editMenu.BeginGroup;
                    break;
                case MenuItemType.HeaderItem:
                    editHeaderItem.SaveValues();

                    _headerItem.Name = editHeaderItem.Name;
                    _headerItem.BeginGroup = editHeaderItem.BeginGroup;
                    break;
                case MenuItemType.SubMenu:
                    editSubMenu.SaveValues();

                    _subMenu.Name = editSubMenu.Name;
                    _subMenu.IconPath = editSubMenu.IconPath;
                    _subMenu.BeginGroup = editSubMenu.BeginGroup;
                    break;
                case MenuItemType.MenuItem:
                    editMenuItem.SaveValues();

                    _menuItem.Name = editMenuItem.Name;
                    _menuItem.IconPath = editMenuItem.IconPath;
                    _menuItem.BeginGroup = editMenuItem.BeginGroup;
                    break;
            }

            this.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}