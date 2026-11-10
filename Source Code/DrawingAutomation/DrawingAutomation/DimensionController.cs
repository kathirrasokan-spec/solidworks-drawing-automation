using SolidWorks.Interop.sldworks;
using System;
using System.Windows.Forms;
using View = SolidWorks.Interop.sldworks.View;

namespace DrawingAutomation
{
    /// <summary>
    /// Controls the placement of dimensions on the drawing document.
    /// </summary>
    public static class DimensionController
    {
        /// <summary>
        /// Places dimensions on the specified drawing view based on user-selected dimensioning options.
        /// </summary>
        /// <param name="drawingDoc">The drawing document where dimensions are placed.</param>
        /// <param name="view">The view where dimensions are placed (e.g., front view).</param>
        /// <param name="options">The user's dimensioning preferences from the form checkboxes.</param>
        public static void PlaceDimensions(DrawingDoc drawingDoc, View view, DimensionOptions options)
        {
            try
            {
                // Validate input parameters
                if (drawingDoc == null)
                    throw new ArgumentNullException(nameof(drawingDoc), "Drawing document cannot be null.");

                if (view == null)
                    throw new ArgumentNullException(nameof(view), "View cannot be null.");

                if (options == null)
                    throw new ArgumentNullException(nameof(options), "Dimension options cannot be null.");

                // Place basic dimensions if the user has selected this option
                if (options.PlaceBasic)
                {
                    BasicDimensionPlacer.PlaceBasicDimensions(drawingDoc, view);
                }

                // Place hole dimensions if the user has selected this option
                if (options.PlaceHole)
                {
                    HoleDimensionPlacer.PlaceHoleDimensions(drawingDoc, view);
                }

                // Place chamfer dimensions if the user has selected this option (currently disabled)
                //if (options.PlaceChamfer)
                //{
                //    ChamferDimensionPlacer.PlaceChamferDimensions(drawingDoc, view);
                //}

                // Place fillet dimensions if the user has selected this option
                if (options.PlaceFillet)
                {
                    FilletDimensionPlacer.PlaceFilletDimensions(drawingDoc, view);
                }

                // Place pattern dimensions if the user has selected this option
                if (options.PlacePattern)
                {
                    PatternDimensionPlacer.PlacePatternDimensions(drawingDoc, view);
                }
            }
            catch (Exception ex)
            {
                // Display error message if an exception occurs
                MessageBox.Show($"An error occurred while placing dimensions: {ex.Message}");
            }
        }
    }
}
