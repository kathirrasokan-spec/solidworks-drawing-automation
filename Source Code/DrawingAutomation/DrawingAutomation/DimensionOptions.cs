namespace DrawingAutomation
{
    /// <summary>
    /// Represents the user's dimensioning preferences for placing dimensions in a drawing.
    /// </summary>
    public class DimensionOptions
    {
        /// <summary>Indicates if Basic dimensions (outer edges) should be placed.</summary>
        public bool PlaceBasic { get; set; }

        /// <summary>Indicates if Hole dimensions should be placed.</summary>
        public bool PlaceHole { get; set; }

        /// <summary>Indicates if Chamfer dimensions should be placed.</summary>
        public bool PlaceChamfer { get; set; }

        /// <summary>Indicates if Fillet dimensions should be placed.</summary>
        public bool PlaceFillet { get; set; }

        /// <summary>Indicates if Pattern dimensions should be placed.</summary>
        public bool PlacePattern { get; set; }

        /// <summary>
        /// Default constructor for DimensionOptions. 
        /// Initializes all dimension options to false.
        /// </summary>
        public DimensionOptions()
        {
            PlaceBasic = false;
            PlaceHole = false;
            PlaceChamfer = false;
            PlaceFillet = false;
            PlacePattern = false;
        }

        /// <summary>
        /// Parameterized constructor to initialize dimension options based on user input.
        /// </summary>
        /// <param name="placeBasic">Whether to place basic dimensions.</param>
        /// <param name="placeHole">Whether to place hole dimensions.</param>
        /// <param name="placeChamfer">Whether to place chamfer dimensions.</param>
        /// <param name="placeFillet">Whether to place fillet dimensions.</param>
        /// <param name="placePattern">Whether to place pattern dimensions.</param>
        public DimensionOptions(bool placeBasic, bool placeHole, bool placeChamfer, bool placeFillet, bool placePattern)
        {
            PlaceBasic = placeBasic;
            PlaceHole = placeHole;
            PlaceChamfer = placeChamfer;
            PlaceFillet = placeFillet;
            PlacePattern = placePattern;
        }
    }
}
