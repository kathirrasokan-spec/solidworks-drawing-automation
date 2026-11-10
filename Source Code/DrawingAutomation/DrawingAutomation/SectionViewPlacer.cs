using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;
using View = SolidWorks.Interop.sldworks.View;

namespace DrawingAutomation
{
    public static class SectionViewPlacer
    {
        public static void CreateVerticalSectionView(DrawingDoc drawingDoc, View frontView)
        {
            try
            {
                if (drawingDoc == null)
                {
                    MessageBox.Show("The drawing document is null.");
                    return;
                }

                if (frontView == null)
                {
                    MessageBox.Show("Front view not found on the sheet.");
                    return;
                }

                // Step 1: Get the outline of the Front View
                double[] outline = (double[])frontView.GetOutline();
                double viewHeight = outline[3] - outline[1]; // Height of the front view
                double[] viewPosition = (double[])frontView.Position; // [X, Y, Z]
                double viewWidth = outline[2] - outline[0];
                double centerX = 0; // Center of the view

                // Step 2: Get the sheet size
                double sheetWidth = 0.0, sheetHeight = 0.0;
                Sheet activeSheet = (Sheet)drawingDoc.GetCurrentSheet();
                activeSheet.GetSize(ref sheetWidth, ref sheetHeight);

                // Step 3: Calculate the startY and endY of the cutting line
                double startY = outline[1] - (3.5 * viewHeight); // Extend further down
                double endY = outline[3] + (0.5 * viewHeight);   // Slightly reduce the top margin

                // Step 4: Adjust for small views
                if (viewHeight < 0.2 * sheetHeight)
                {
                    startY = 0.05 * sheetHeight; // Start near the bottom
                    endY = 0.95 * sheetHeight;   // End near the top
                }

                // Step 5: Activate the front view
                drawingDoc.ActivateView(frontView.Name);

                // Step 6: Access SketchManager from DrawingDoc
                ModelDoc2 modelDoc = (ModelDoc2)drawingDoc;
                SketchManager sketchManager = modelDoc.SketchManager;
                sketchManager.InsertSketch(true); // Start Sketch

                // Step 7: Draw the cutting line
                SketchSegment sectionLine = sketchManager.CreateLine(
                    centerX, startY, 0,
                    centerX, endY, 0
                );

                if (sectionLine == null)
                {
                    MessageBox.Show("Failed to create the section line.");
                    sketchManager.InsertSketch(false);
                    return;
                }

                sketchManager.InsertSketch(false); // Exit Sketch

                // Step 8: Create the Section View
                double sectionViewX = outline[2] + 0.1;
                double sectionViewY = (outline[1] + outline[3]) / 2;
                object excludedComponents = null;

                View sectionView = drawingDoc.CreateSectionViewAt5(
                    sectionViewX,
                    sectionViewY,
                    0,
                    "A-A",
                    32,
                    excludedComponents,
                    0.01
                );

                if (sectionView == null)
                {
                    MessageBox.Show("Failed to create the Section View.");
                    return;
                }

                DrSection swSectionView = (DrSection)sectionView.GetSection();
                swSectionView.SetAutoHatch(false);
                swSectionView.SetReversedCutDirection(false);
                swSectionView.SetPartialSection(false);
                swSectionView.SetDisplayOnlySurfaceCut(false);
                swSectionView.SectionDepth = 0.2;
                swSectionView.DisplaySurfaceBodies = false;
                swSectionView.CutSurfaceBodies = false;
                sectionView.Position = new double[] { sectionViewX, sectionViewY - 0.1, 0 };

                HideViewLabel(sectionView);
                modelDoc.EditRebuild3();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while placing the section view: {ex.Message}");
            }
        }

        private static void HideViewLabel(View view)
        {
            try
            {
                Annotation annotation = (Annotation)view.GetFirstAnnotation();

                while (annotation != null)
                {
                    if (annotation.GetType() == (int)swAnnotationType_e.swNote)
                    {
                        annotation.Visible = 0;
                    }
                    annotation = (Annotation)annotation.GetNext3();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while hiding view label: {ex.Message}");
            }
        }
    }
}
