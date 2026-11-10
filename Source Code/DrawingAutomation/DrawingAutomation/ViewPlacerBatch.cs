using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;
using View = SolidWorks.Interop.sldworks.View;

namespace DrawingAutomation
{
    /// <summary>
    /// Handles the placement of 3rd angle projection views in a batch processing workflow.
    /// </summary>
    public static class ViewPlacerBatch
    {
        /// <summary>
        /// Places 3rd angle projection views (Front, Top, Isometric) on the drawing sheet.
        /// </summary>
        /// <param name="drawingDoc">The active drawing document.</param>
        /// <param name="partDoc">The active part document.</param>
        public static void Place3rdAngleViews(DrawingDoc drawingDoc, ModelDoc2 partDoc)
        {
            try
            {
                if (drawingDoc == null)
                {
                    MessageBox.Show("The active document is not a drawing.");
                    return;
                }

                if (partDoc == null)
                {
                    MessageBox.Show("The part or assembly document is invalid.");
                    return;
                }

                string partPath = partDoc.GetPathName();
                if (string.IsNullOrEmpty(partPath))
                {
                    MessageBox.Show("The part file path is invalid.");
                    return;
                }

                // Step 1: Get the sheet size
                Sheet activeSheet = (Sheet)drawingDoc.GetCurrentSheet();
                double sheetWidth = 0.0, sheetHeight = 0.0;
                activeSheet.GetSize(ref sheetWidth, ref sheetHeight);

                // Step 2: Calculate dynamic margins
                double marginX = 0.1 * sheetWidth;
                double marginY = 0.15 * sheetHeight;

                // Place the base views
                View frontView = PlaceBaseViews(drawingDoc, partPath, sheetWidth, sheetHeight, marginX, marginY);

                // Create a section view
                SectionViewPlacer.CreateVerticalSectionView(drawingDoc, frontView);

                // Place all dimensions
                AllDimensionPlacer.PlaceAllDimensions(drawingDoc, frontView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while placing views: {ex.Message}");
            }
        }

        /// <summary>
        /// Places the base views (Front, Top, Isometric) on the drawing sheet.
        /// </summary>
        /// <param name="drawingDoc">The drawing document.</param>
        /// <param name="partPath">The file path of the part document.</param>
        /// <param name="sheetWidth">The width of the drawing sheet.</param>
        /// <param name="sheetHeight">The height of the drawing sheet.</param>
        /// <param name="marginX">Horizontal margin.</param>
        /// <param name="marginY">Vertical margin.</param>
        /// <returns>The front view created.</returns>
        public static View PlaceBaseViews(DrawingDoc drawingDoc, string partPath, double sheetWidth, double sheetHeight, double marginX, double marginY)
        {
            // Place Front View
            View frontView = (View)drawingDoc.CreateDrawViewFromModelView3(partPath, "*Top", 0, 0, 0);
            if (frontView == null) throw new Exception("Failed to create the Front view.");

            double frontViewWidth, frontViewHeight;
            GetViewGeometrySize(frontView, out frontViewWidth, out frontViewHeight);

            double frontViewX = marginX + (frontViewWidth / 1.5);
            double frontViewY = marginY + frontViewHeight;
            frontView.Position = new double[] { frontViewX, frontViewY, 0 };

            // Place Top View
            View topView = (View)drawingDoc.CreateDrawViewFromModelView3(partPath, "*Front", 0, 0, 0);
            if (topView == null) throw new Exception("Failed to create the Top view.");

            double topViewWidth, topViewHeight;
            GetViewGeometrySize(topView, out topViewWidth, out topViewHeight);

            double topViewX = frontViewX;
            double topViewY = frontViewY + (frontViewHeight / 2) + (topViewHeight / 2) + (0.15 * sheetHeight);
            topView.Position = new double[] { topViewX, topViewY, 0 };

            // Place Isometric View
            View isoView = (View)drawingDoc.CreateDrawViewFromModelView3(partPath, "*Isometric", 0, 0, 0);
            if (isoView == null) throw new Exception("Failed to create the Isometric view.");

            double isoViewWidth, isoViewHeight;
            GetViewGeometrySize(isoView, out isoViewWidth, out isoViewHeight);

            double isoViewX = sheetWidth - marginX - (isoViewWidth / 3);
            double isoViewY = sheetHeight - marginY - (isoViewHeight / 3);
            isoView.Position = new double[] { isoViewX, isoViewY, 0 };

            return frontView;
        }

        /// <summary>
        /// Retrieves the geometry size of a view based on its bounding box.
        /// </summary>
        /// <param name="view">The view to measure.</param>
        /// <param name="width">The width of the view.</param>
        /// <param name="height">The height of the view.</param>
        public static void GetViewGeometrySize(View view, out double width, out double height)
        {
            double[] outline = (double[])view.GetOutline();
            width = outline[2] - outline[0];
            height = outline[3] - outline[1];
        }
    }
}
