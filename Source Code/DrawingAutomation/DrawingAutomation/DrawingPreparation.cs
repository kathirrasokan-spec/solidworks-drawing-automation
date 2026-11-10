using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;

namespace DrawingAutomation
{
    public static class DrawingPreparation
    {
        public static void PrepareDrawing(ISldWorks swApp, string templatePath, DimensionOptions dimensionOptions)
        {
            try
            {
                // Get the active document (part/assembly)
                ModelDoc2 activeDoc = (ModelDoc2)swApp.ActiveDoc;

                // Save the reference to the active part/assembly
                ModelDoc2 previousDoc = activeDoc;

                // Create a new drawing document
                ModelDoc2 drawingDoc = (ModelDoc2)swApp.NewDocument(templatePath, 0, 0, 0);
                if (drawingDoc == null)
                {
                    MessageBox.Show("Failed to create a drawing document.");
                    return;
                }

                // Pass the part document and drawing document to the view placer
                ViewPlacer.Place3rdAngleViews((DrawingDoc)drawingDoc, previousDoc, dimensionOptions);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}