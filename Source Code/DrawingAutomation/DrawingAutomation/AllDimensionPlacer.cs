using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;
using View = SolidWorks.Interop.sldworks.View;

namespace DrawingAutomation
{
    public static class AllDimensionPlacer
    {
        public static void PlaceAllDimensions(DrawingDoc drawingDoc, View frontView)
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
                    (int)swInsertAnnotation_e.swInsertDimensionsMarkedForDrawing |
                    (int)swInsertAnnotation_e.swInsertDimensionsNotMarkedForDrawing |
                    (int)swInsertAnnotation_e.swInsertholeCallout |
                    (int)swInsertAnnotation_e.swInsertHoleWizardLocationDimensions,
                    true,
                    true,
                    false,
                    false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
