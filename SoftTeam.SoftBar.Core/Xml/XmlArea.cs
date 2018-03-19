﻿using System.Collections.Generic;
using System.Xml;

namespace SoftTeam.SoftBar.Core.Xml
{
    /// <summary>
    /// Class for a user button area (Xml)
    /// </summary>
    public class XmlArea
    {
        private List<XmlMenu> _menus = null;

        public XmlArea()
        {
            _menus = new List<XmlMenu>();
        }

        public List<XmlMenu> Menus { get => _menus; set => _menus = value; }

        // Parse an area node
        public void ParseXml(XmlNode areaNode)
        {
            // and loop through them
            foreach (XmlNode menuNode in areaNode)
            {
                // Create the new menu
                XmlMenu menu = new XmlMenu();
                // Parse the xml for this menu
                menu.ParseXml(menuNode);
                // Add the menu to the user menu collection
                _menus.Add(menu);
            }

        }
    }
}