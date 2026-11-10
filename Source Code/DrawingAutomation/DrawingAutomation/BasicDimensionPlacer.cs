using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;
using View = SolidWorks.Interop.sldworks.View;

namespace DrawingAutomation
{
    public static class BasicDimensionPlacer
    {
        public static void PlaceBasicDimensions(DrawingDoc drawingDoc, View frontView)
        {
            try
            {
                if (drawingDoc == null || frontView == null)
                {
                    MessageBox.Show("Invalid drawing document or view.");
                    return;
                }

                ModelDoc2 modelDoc = (ModelDoc2)drawingDoc;

                drawingDoc.ActivateView(frontView.Name);

                object[] annotations = (object[])drawingDoc.InsertModelAnnotations3(
                    (int)swImportModelItemsSource_e.swImportModelItemsFromEntireModel,
                    (int)swInsertAnnotation_e.swInsertDimensionsMarkedForDrawing,
                    true,  // Insert into all views
                    true,  // Eliminate duplicate dimensions
                    false, // Do not insert dimensions from hidden features
                    false  // Do not use sketch placement
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
