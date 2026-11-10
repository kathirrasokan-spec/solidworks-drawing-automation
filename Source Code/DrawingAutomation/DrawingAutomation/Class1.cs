using System;
using System.Runtime.InteropServices;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace DrawingAutomation
{
    [Guid("C073BA1F-2757-47EC-AA17-E0FB3255C6F6")]
    [ComVisible(true)]
    [DisplayName("Drawing Automation")]
    [Description("Drawing SOLIDWORKS add-in")]
    public class Addin1 : ISwAddin
    {
        #region Registration

        private const string ADDIN_KEY_TEMPLATE = @"SOFTWARE\SolidWorks\Addins\{{{0}}}";
        private const string ADDIN_STARTUP_KEY_TEMPLATE = @"Software\SolidWorks\AddInsStartup\{{{0}}}";
        private const string ADD_IN_TITLE_REG_KEY_NAME = "Title";
        private const string ADD_IN_DESCRIPTION_REG_KEY_NAME = "Description";

        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            try
            {
                string addInTitle = t.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? t.ToString();
                string addInDesc = t.GetCustomAttributes(false).OfType<DescriptionAttribute>().FirstOrDefault()?.Description ?? t.ToString();
                bool loadAtStartup = true;

                var addInKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(string.Format(ADDIN_KEY_TEMPLATE, t.GUID));
                addInKey.SetValue(null, 0);
                addInKey.SetValue(ADD_IN_TITLE_REG_KEY_NAME, addInTitle);
                addInKey.SetValue(ADD_IN_DESCRIPTION_REG_KEY_NAME, addInDesc);

                var addInStartupKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(string.Format(ADDIN_STARTUP_KEY_TEMPLATE, t.GUID));
                addInStartupKey.SetValue(null, Convert.ToInt32(loadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while registering the add-in: {ex.Message}");
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            try
            {
                Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(string.Format(ADDIN_KEY_TEMPLATE, t.GUID));
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(string.Format(ADDIN_STARTUP_KEY_TEMPLATE, t.GUID));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while unregistering the add-in: {ex.Message}");
            }
        }

        #endregion

        private ISldWorks _swApp;
        private ICommandManager _cmdMgr;
        private int _addinCookie;

        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            _swApp = (SldWorks)ThisSW;
            _addinCookie = Cookie;

            // Register add-in with SolidWorks
            _swApp.SetAddinCallbackInfo2(0, this, _addinCookie);

            _cmdMgr = _swApp.GetCommandManager(Cookie);

            AddCommandTabWithButtons();

            return true;
        }

        public bool DisconnectFromSW()
        {
            RemoveCommandTab();
            return true;
        }

        private void AddCommandTabWithButtons()
        {
            try
            {
                string tabTitle = "Drawing Automation";

                // Create or get the command group
                int commandGroupID = 1;
                ICommandGroup cmdGroup = _cmdMgr.CreateCommandGroup2(commandGroupID, tabTitle, tabTitle, "", -1, true, ref commandGroupID);
                cmdGroup.HasToolbar = true;
                cmdGroup.HasMenu = true;

                // Define icon paths
                string icon1Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image1.bmp");
                string icon2Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image2.bmp");
                cmdGroup.IconList = new[] { icon1Path, icon2Path };

                // Add buttons to the command group
                int cmdIndex0 = cmdGroup.AddCommandItem2(
                    "Create Drawing",
                    -1,
                    "Generate a 2D drawing from the open part file",
                    "Create Drawing",
                    0,
                    "CreateDrawingCallback",
                    "",
                    0,
                    (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow);

                int cmdIndex1 = cmdGroup.AddCommandItem2(
                    "Batch Process",
                    -1,
                    "Generate 2D drawings for multiple part files",
                    "Batch Process",
                    1,
                    "BatchProcessCallback",
                    "",
                    1,
                    (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow);

                if (cmdIndex0 < 0 || cmdIndex1 < 0)
                {
                    MessageBox.Show("Failed to add one or more buttons to the command group.");
                    return;
                }

                cmdGroup.Activate();

                // Associate the command group with a tab
                ICommandTab cmdTab = _cmdMgr.GetCommandTab((int)swDocumentTypes_e.swDocPART, tabTitle);
                if (cmdTab == null)
                {
                    cmdTab = _cmdMgr.AddCommandTab((int)swDocumentTypes_e.swDocPART, tabTitle);

                    CommandTabBox cmdBox = cmdTab.AddCommandTabBox();

                    int[] cmdIDs = { cmdGroup.get_CommandID(cmdIndex0), cmdGroup.get_CommandID(cmdIndex1) };
                    int[] textTypes = { (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow, (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow };

                    cmdBox.AddCommands(cmdIDs, textTypes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating tab: {ex.Message}");
            }
        }

        private void RemoveCommandTab()
        {
            CommandTab cmdTab = _cmdMgr.GetCommandTab((int)swDocumentTypes_e.swDocPART, "Drawing Automation");
            if (cmdTab != null)
            {
                _cmdMgr.RemoveCommandTab(cmdTab);
            }
        }

        public void CreateDrawingCallback()
        {
            if (!DocumentValidator.IsActiveDocumentValid(_swApp)) return;

            SHEETSELECTION templateSelectionForm = new SHEETSELECTION(_swApp);
            templateSelectionForm.ShowDialog();

            string selectedTemplate = templateSelectionForm.SelectedTemplateSize;

            if (string.IsNullOrEmpty(selectedTemplate))
            {
                MessageBox.Show("No template selected!");
                return;
            }

            // DrawingPreparation.PrepareDrawing(_swApp, selectedTemplate);
        }

        public void BatchProcessCallback()
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Multiselect = true;
                    dialog.Filter = "SolidWorks Part Files (*.sldprt)|*.sldprt";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string[] partFilePaths = dialog.FileNames;
                        List<string> validPartFiles = partFilePaths
                            .Where(filePath => Path.GetExtension(filePath).ToLower() == ".sldprt")
                            .ToList();

                        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "A2 Drawing.DRWDOT");
                        string saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Automation_Parts");

                        foreach (string partFilePath in validPartFiles)
                        {
                            PartDoc partDoc = (PartDoc)_swApp.OpenDoc6(partFilePath, (int)swDocumentTypes_e.swDocPART, 0, "", 0, 0);

                            if (partDoc != null)
                            {
                                DrawingPreparationBatch.PrepareDrawing(_swApp, templatePath, saveLocation);
                                _swApp.CloseDoc(Path.GetFileName(partFilePath));
                                _swApp.CloseAllDocuments(true);
                            }
                            else
                            {
                                MessageBox.Show($"Failed to open part file: {partFilePath}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during batch processing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
