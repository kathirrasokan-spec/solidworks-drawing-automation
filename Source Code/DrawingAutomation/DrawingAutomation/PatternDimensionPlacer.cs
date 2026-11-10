using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;
using View = SolidWorks.Interop.sldworks.View;

namespace DrawingAutomation
{
    /// <summary>
    /// Responsible for placing pattern dimensions in the specified drawing view.
    /// </summary>
    public static class PatternDimensionPlacer
    {
        /// <summary>
        /// Places pattern dimensions in the specified drawing view.
        /// </summary>
        /// <param name="drawingDoc">The drawing document where dimensions are to be placed.</param>
        /// <param name="frontView">The drawing view where pattern dimensions are to be applied.</param>
        public static void PlacePatternDimensions(DrawingDoc drawingDoc, View frontView)
        {
            try
            {
                // Validate the drawing document and view
                if (drawingDoc == null)
                {
                    MessageBox.Show("Invalid drawing document. Please ensure a drawing document is open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (frontView == null)
                {
                    MessageBox.Show("Invalid drawing view. Please ensure the specified view exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Activate the specified drawing view
                drawingDoc.ActivateView(frontView.Name);

                // Insert annotations for pattern dimensions
                object[] annotations = (object[])drawingDoc.InsertModelAnnotations3(
                    (int)swImportModelItemsSource_e.swImportModelItemsFromEntireModel,
                    (int)swInsertAnnotation_e.swInsertDimensionsNotMarkedForDrawing |
                    (int)swInsertAnnotation_e.swInsertholeCallout |
                    (int)swInsertAnnotation_e.swInsertInstanceCounts,
                    true,  // Apply dimensions only to the current view
                    true,  // Eliminate duplicate dimensions
                    false, // Exclude dimensions of hidden features
                    false  // Do not use sketch placement
                );

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while placing pattern dimensions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
