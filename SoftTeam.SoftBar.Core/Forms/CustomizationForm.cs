﻿// Since we have using System.Windows.Forms below, we get a conflict 
// between the class MenuItem in Forms and Core.Controls so we use
// an alias here.
using CoreControls = SoftTeam.SoftBar.Core.Controls;
using SoftTeam.SoftBar.Core.Misc;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SoftTeam.SoftBar.Core.Forms
{
    public partial class CustomizationForm : DevExpress.XtraEditors.XtraForm
    {
        #region Fields
        private string _path = "";
        private int _height = Constants.TOP_MARGIN;
        private int _level = 0;
        private int _maxLevel = 0;
        private SoftBarManager _manager = null;
        private CoreControls.MenuItem _previousMenuItem = null;
        private ObservableCollection<CoreControls.MenuItem> _menuItems = new ObservableCollection<CoreControls.MenuItem>();
        #endregion

        #region Constructors
        public CustomizationForm(SoftBarManager manager, string path)
        {
            InitializeComponent();

            _path = path;
            LoadMenu(manager);
            _manager = manager;
            barStaticItemPath.Caption = _path;
        }
        #endregion

        #region Calculate max level
        private int CalculateMaxLevel(SoftBarManager manager)
        {
            int maxLevel = 0;

            foreach (var menu in manager.Menus)
                CalculateMaxLevelEx(menu, ref maxLevel);

            return maxLevel;
        }

        private void CalculateMaxLevelEx(SoftBarBaseMenu menu, ref int maxLevel)
        {
            _level += 1;
            if (_level > maxLevel)
                maxLevel = _level;

            foreach (SoftBarBaseItem menuItem in menu.MenuItems)
                if (menuItem is SoftBarSubMenu)
                    CalculateMaxLevelEx((SoftBarBaseMenu)menuItem, ref maxLevel);

            _level -= 1;
        }
        #endregion

        #region Load menu for customization
        private void LoadMenu(SoftBarManager manager)
        {
            _maxLevel = CalculateMaxLevel(manager);

            foreach (var menu in manager.Menus)
            {
                AddItemControl(MenuItemType.Menu, menu);
                LoadMenu(menu);
            }
        }

        private void LoadMenu(SoftBarBaseMenu menu)
        {
            _level += 1;

            foreach (SoftBarBaseItem menuItem in menu.MenuItems)
            {
                if (menuItem.SystemMenu)
                    continue;

                if (menuItem is SoftBarSubMenu)
                {
                    AddItemControl(MenuItemType.SubMenu, menuItem);
                    LoadMenu((SoftBarBaseMenu)menuItem);                    
                }
                else if (menuItem is SoftBarHeaderItem)
                    AddItemControl(MenuItemType.HeaderItem, menuItem);
                else if (menuItem is SoftBarMenuItem)
                    AddItemControl(MenuItemType.MenuItem, menuItem);
            }
            _level -= 1;
        }

        private void AddItemControl(MenuItemType type, SoftBarBaseItem menu)
        {
            var step = 128 / _maxLevel;
            var color = Color.FromArgb(50, _level * step, _level * step, _level * step);
            CoreControls.MenuItem item = new CoreControls.MenuItem(this,type, menu, _level, color, _menuItems, _previousMenuItem);
            var width = xtraScrollableControlMenu.ClientSize.Width - _maxLevel * Constants.LEVEL_INDENTATION - Constants.SCROLLBAR_WIDTH;

            item.Location = new Point(_level * Constants.LEVEL_INDENTATION + Constants.LEFT_MARGIN, _height);
            item.Size = new Size(width, Constants.ITEM_HEIGHT);
            xtraScrollableControlMenu.Controls.Add(item);
            _height += item.Height + Constants.SPACE;
            menu.CustomizationMenuItem = item;
            item.ClearSelectedRequested += Item_ClearSelectedRequested;

            _previousMenuItem = item;
            _menuItems.Add(item);
        }
        #endregion

        #region Clear selected menu item
        private void Item_ClearSelectedRequested(object sender, EventArgs e)
        {
            foreach (var menuItem in _manager.Menus)
            {
                menuItem.CustomizationMenuItem.Selected = MenuItemSelectedStatus.NotSelected;
                SetSelected(menuItem, MenuItemSelectedStatus.NotSelected);
            }
        }

        private void SetSelected(SoftBarBaseMenu menu, MenuItemSelectedStatus selected)
        {
            foreach (var menuItem in menu.MenuItems)
            {
                menuItem.CustomizationMenuItem.Selected = selected;
                if (menuItem is SoftBarSubMenu)
                    SetSelected((SoftBarSubMenu)menuItem, selected);
            }
        }
        #endregion

        #region Bar event handlers
        private void barStaticItemFileExitWithoutSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}