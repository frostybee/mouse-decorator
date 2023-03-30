﻿using Frostybee.MouseDecorator.Core;
using Frostybee.MouseDecorator.Properties;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NativeTextRenderer;

namespace Frostybee.MouseDecorator
{
    public partial class MainForm : MaterialForm
    {
        private readonly MaterialSkinManager _materialSkinManager;
        private readonly DecorationController _applicationManager;

        public MainForm()
        {
            InitializeComponent();
            // TODO: find the best value to auto-scale with.
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScaleDimensions = new SizeF(96f, 96f);
            // Initialize the global managers.
            _applicationManager = DecorationController.Instance;
            _materialSkinManager = MaterialSkinManager.Instance;
            InitializeControls();            
        }

        private void InitializeControls()
        {
            // Clean up resources on application exit.
            Application.ApplicationExit += Application_ApplicationExit;
            this.Icon = Properties.Resources.Martin_Berube_Animal_Bee;

            // Handle minimizing main application's window to the system tray.
            this.Resize += MainForm_Resize;

            appNotifyIcon.DoubleClick += AppNotifyIcon_DoubleClick;
            appNotifyIcon.Icon = Properties.Resources.Martin_Berube_Animal_Bee;
            appNotifyIcon.ContextMenuStrip = trayContextMenu;
            //-- Set up the form closing/loading events. They are required for ensuring that 
            // the mouse hooks is properly installed/uninstalled.
            this.Load += MainForm_Load;

            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            _materialSkinManager.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            _materialSkinManager.AddFormToManage(this);
            _materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            _materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700,
                Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
            // Internal events.
            //pbPreview.Paint += HighlighterPreview_Paint;
            //             
        }

        /// <summary>
        /// Restores the main application's window from the system tray.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Restore();
        }

        /// <summary>
        /// Minimizes the main application's window to the system tray.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(300);
                ShowInTaskbar = false;
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                appNotifyIcon.Visible = false;
            }
        }
        /// <summary>
        /// Saves the mouse decoration settings upon shutting down the application. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            _applicationManager.SettingsManager.SaveHighlighterSettings();
            _applicationManager?.DisableHook();
            _applicationManager?.Dispose();
        }
        /// <summary>
        /// Initializes the custom user controls of the mouse highlighter and click decorator features. 
        /// If both features are enabled, the mouse hook will be installed.
        /// The user settings will be loaded at this stage of the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            _applicationManager.BootstrapApp();
            tabHighlighterSettings.InitHighlighterControls();
            Debug.WriteLine("MainForm_Load....");
        }
        private void Restore()
        {            
            Show();            
            ShowInTaskbar = true;            
            this.WindowState = FormWindowState.Normal;
            appNotifyIcon.Visible = false;            
            bool oldTopMost = TopMost;
            TopMost = true;
            TopMost = false;
            BringToFront();
        }

        protected override void WndProc(ref Message inMessage)
        {
            if (inMessage.Msg == Program.WM_SHOW_MAIN_WINDOW)
            {
                Restore();
            }
            base.WndProc(ref inMessage);
        }

        private void MenuItemShow_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            // Show the about page. Focus the about tab.
        }

        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();            
        }
    }
}
