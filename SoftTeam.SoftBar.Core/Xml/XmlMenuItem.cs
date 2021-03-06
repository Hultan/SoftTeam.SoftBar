﻿using System.Xml;

namespace SoftTeam.SoftBar.Core.Xml
{
    // Class for a menu item (Xml)
    public class XmlMenuItem : XmlMenuItemBase
    {
        #region Fields
        private string _applicationPath = string.Empty;
        private string _documentPath = string.Empty;
        private string _parameters = string.Empty;
        private bool _runAsAdministrator = false;
        #endregion

        #region Constructor
        public XmlMenuItem()
        {
        }
        #endregion

        #region Properties
        public string ApplicationPath { get => _applicationPath; set => _applicationPath = value; }
        public string DocumentPath { get => _documentPath; set => _documentPath = value; }
        public string Parameters { get => _parameters; set => _parameters = value; }
        public bool RunAsAdministrator { get => _runAsAdministrator; set => _runAsAdministrator = value; }
        #endregion

        #region ParseXml
        // Parse a menu item node
        public void ParseXml (XmlNode menuItemNode)
        {
            // Get the name of the menu
            _name = menuItemNode.Attributes["name"].Value;

            // Check if the menu has an iconPath attribute
            var iconPathAttribute = menuItemNode.Attributes["iconPath"];
            if (iconPathAttribute != null)
                _iconPath = iconPathAttribute.Value;

            // Check if the menu has an beginGroup attribute
            var beginGroupAttribute = menuItemNode.Attributes["beginGroup"];
            if (beginGroupAttribute != null)
                _beginGroup = beginGroupAttribute.Value.ToLower() == "true";

            // Check if the menu has an runAsAdministrator attribute
            var runAsAdministratorAttribute = menuItemNode.Attributes["runAsAdministrator"];
            if (runAsAdministratorAttribute != null)
                _runAsAdministrator = runAsAdministratorAttribute.Value.ToLower() == "true";

            // Get and store the application and document path
            var applicationNodeElement = menuItemNode.SelectSingleNode("applicationPath");
            _applicationPath = applicationNodeElement == null ? "" : applicationNodeElement.InnerText;
            var documentNodeElement = menuItemNode.SelectSingleNode("documentPath");
            _documentPath = documentNodeElement == null ? "" : documentNodeElement.InnerText;
            var parameterElement = menuItemNode.SelectSingleNode("parameters");
            _parameters = parameterElement == null ? "" : parameterElement.InnerText;

        }
        #endregion

        #region Overrides
        public override int CountItems()
        {
            return 1;
        }

        public override bool ContainsItem(XmlMenuItemBase item)
        {
            return item.Equals(this);
        }
        #endregion
    }
}
