﻿using DevExpress.XtraEditors;
using SoftTeam.SoftBar.Core.Forms;
using SoftTeam.SoftBar.Core.Misc;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;

namespace SoftTeam.SoftBar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [HandleProcessCorruptedStateExceptions]        
        static void Main()
        {
            try
            {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(MainAppBarForm_UIThreadException);

                // Set the unhandled exception mode to force all Windows Forms errors
                // to go through our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainAppBarForm());
            }
            catch (Exception ex)
            {
                SaveExceptionLog(ex);
                Application.Exit();
            }
        }

        // https://msdn.microsoft.com/en-us/library/system.windows.forms.application.threadexception.aspx

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void MainAppBarForm_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                SaveExceptionLog(t.Exception);
                result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                SaveExceptionLog(ex);
                string errorMsg = "An application error occurred. Please contact the administrator " +
                    "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the administrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }

        private static void SaveExceptionLog(Exception e)
        {
            try
            {
                var workingDir = HelperFunctions.GetWorkingDirectory();
                var exceptionDir = Path.Combine(workingDir, "Exception log");
                var timeStamp = HelperFunctions.GetTimeStamp();
                var fileName = $"Exception_{timeStamp}.txt";
                var writer = File.CreateText(Path.Combine(exceptionDir,fileName));

                writer.WriteLine("Date :" + DateTime.Now.ToString() + Environment.NewLine);
                writer.WriteLine("Message :" + e.Message + Environment.NewLine);
                writer.WriteLine("StackTrace :" + e.StackTrace + Environment.NewLine);
                writer.WriteLine("----------------------------------------------" + Environment.NewLine);

                writer.Close();
            }
            catch
            {
                XtraMessageBox.Show("Failed to write exception log to SoftBar working directory!");
            }
        }
    }
}
