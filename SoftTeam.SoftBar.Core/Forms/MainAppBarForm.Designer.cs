﻿namespace SoftTeam.SoftBar.Core.Forms
{
    partial class MainAppBarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainAppBarForm));
            this.barManagerSoftBar = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.openFileDialogSoftBar = new System.Windows.Forms.OpenFileDialog();
            this.toolTipControllerSoftBar = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerSoftBar)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerSoftBar
            // 
            this.barManagerSoftBar.DockControls.Add(this.barDockControlTop);
            this.barManagerSoftBar.DockControls.Add(this.barDockControlBottom);
            this.barManagerSoftBar.DockControls.Add(this.barDockControlLeft);
            this.barManagerSoftBar.DockControls.Add(this.barDockControlRight);
            this.barManagerSoftBar.Form = this;
            this.barManagerSoftBar.MaxItemId = 5;
            this.barManagerSoftBar.ShowScreenTipsInMenus = true;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagerSoftBar;
            this.barDockControlTop.Size = new System.Drawing.Size(467, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 32);
            this.barDockControlBottom.Manager = this.barManagerSoftBar;
            this.barDockControlBottom.Size = new System.Drawing.Size(467, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManagerSoftBar;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 32);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(467, 0);
            this.barDockControlRight.Manager = this.barManagerSoftBar;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 32);
            // 
            // openFileDialogSoftBar
            // 
            this.openFileDialogSoftBar.FileName = "openFileDialog1";
            // 
            // toolTipControllerSoftBar
            // 
            this.toolTipControllerSoftBar.AllowHtmlText = true;
            this.toolTipControllerSoftBar.AutoPopDelay = 10000;
            this.toolTipControllerSoftBar.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // MainAppBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 32);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainAppBarForm";
            this.ShowInTaskbar = false;
            this.Text = "SoftTeam SoftBar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainAppBarForm_FormClosing);
            this.Load += new System.EventHandler(this.MainAppBarForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainAppBarForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerSoftBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarManager barManagerSoftBar;
        private System.Windows.Forms.OpenFileDialog openFileDialogSoftBar;
        private DevExpress.Utils.ToolTipController toolTipControllerSoftBar;
    }
}