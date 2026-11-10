using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;
using System.Windows.Forms;

namespace DrawingAutomation
{
    /// <summary>
    /// Handles the preparation of drawing files for batch processing.
    /// </summary>
    public static class DrawingPreparationBatch
    {
        /// <summary>
        /// Prepares a drawing document for a given part or assembly and saves it to a specified location.
        /// </summary>
        /// <param name="swApp">The SolidWorks application instance.</param>
        /// <param name="templatePath">The file path of the drawing template to use.</param>
        /// <param name="saveLocation">The directory where the generated drawing will be saved.</param>
        public static void PrepareDrawing(ISldWorks swApp, string templatePath, string saveLocation)
        {
            try
            {
                // Step 1: Get the active part or assembly document
                ModelDoc2 activeDoc = (ModelDoc2)swApp.ActiveDoc;

                if (activeDoc == null)
                {
                    MessageBox.Show("No active document found. Please ensure a part or assembly is open before running the batch process.");
                    return;
                }

                // Step 2: Create a new drawing document using the provided template
                ModelDoc2 drawingDoc = (ModelDoc2)swApp.NewDocument(templatePath, 0, 0, 0);
                if (drawingDoc == null)
                {
                    MessageBox.Show("Failed to create a new drawing document.");
                    return;
                }

                // Step 3: Place the 3rd angle views in the drawing
                ViewPlacerBatch.Place3rdAngleViews((DrawingDoc)drawingDoc, activeDoc);

                // Step 4: Generate the file path for saving the drawing
                string drawingFileName = Path.Combine(saveLocation, $"{Path.GetFileNameWithoutExtension(activeDoc.GetPathName())}.slddrw");

                // Step 5: Save the drawing document
                bool saveResult = drawingDoc.SaveAs(drawingFileName);
                if (!saveResult)
                {
                    MessageBox.Show($"Failed to save the drawing: {drawingFileName}");
                }

                // Step 6: Close the drawing document
                swApp.CloseDoc(drawingDoc.GetTitle());
            }
            catch (Exception ex)
            {
                // Log and display any errors encountered during the process
                MessageBox.Show($"An error occurred while preparing the drawing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
