using System;
using System.IO;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;

namespace DrawingAutomation
{
    public partial class SHEETSELECTION : Form
    {
        private ISldWorks _swApp;

        public SHEETSELECTION(ISldWorks swApp)
        {
            InitializeComponent();
            _swApp = swApp;
        }

        public string SelectedTemplateSize
        {
            get { return TemplateSelection.SelectedItem?.ToString(); }
        }

        public DimensionOptions UserDimensionOptions
        {
            get
            {
                return new DimensionOptions(
                    BasicDimension.Checked,
                    Hole.Checked,
                    Chamfer.Checked,
                    Fillet.Checked,
                    Pattern.Checked
                );
            }
        }

        private readonly string A0TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "A0 Drawing.DRWDOT");
        private readonly string A1TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "A1 Drawing.DRWDOT");
        private readonly string A2TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "A2 Drawing.DRWDOT");
        private readonly string A3TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "A3 Drawing.DRWDOT");
        private readonly string A4LTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "A4 Landscape.DRWDOT");
        private readonly string A4PTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "A4 Portrait.DRWDOT");

        private void TemplateSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional logic for template selection
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string selectedTemplate = SelectedTemplateSize;
            string templatePath = "";

            // Determine the template path based on the selection
            switch (selectedTemplate)
            {
                case "SIZE A0":
                    templatePath = A0TemplatePath;
                    break;
                case "SIZE A1":
                    templatePath = A1TemplatePath;
                    break;
                case "SIZE A2":
                    templatePath = A2TemplatePath;
                    break;
                case "SIZE A3":
                    templatePath = A3TemplatePath;
                    break;
                case "SIZE A4L":
                    templatePath = A4LTemplatePath;
                    break;
                case "SIZE A4P":
                    templatePath = A4PTemplatePath;
                    break;
                default:
                    MessageBox.Show("Please select a valid template.");
                    return;
            }

            if (!string.IsNullOrEmpty(templatePath))
            {
                this.Close();
                DrawingPreparation.PrepareDrawing(_swApp, templatePath, UserDimensionOptions);
            }
        }

        private void CreateDrawingWithTemplate(string templatePath)
        {
            try
            {
                ModelDoc2 drawingDoc = (ModelDoc2)_swApp.NewDocument(templatePath, 0, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating drawing: {ex.Message}");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { }
        private void Hole_CheckedChanged(object sender, EventArgs e) { }
        private void Pattern_CheckedChanged(object sender, EventArgs e) { }
        private void Chamfer_CheckedChanged(object sender, EventArgs e) { }
        private void Fillet_CheckedChanged(object sender, EventArgs e) { }
    }
}
