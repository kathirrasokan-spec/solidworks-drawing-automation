using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;

namespace DrawingAutomation
{
    /// <summary>
    /// Provides methods to validate the active document in SolidWorks.
    /// </summary>
    public static class DocumentValidator
    {
        /// <summary>
        /// Validates if the active document is valid and of the correct type (part or assembly).
        /// </summary>
        /// <param name="swApp">The SolidWorks application instance (ISldWorks).</param>
        /// <returns>True if the active document is valid, otherwise false.</returns>
        public static bool IsActiveDocumentValid(ISldWorks swApp)
        {
            // Get the active document from SolidWorks
            ModelDoc2 activeDoc = (ModelDoc2)swApp.ActiveDoc;

            if (activeDoc == null)
            {
                // No active document found
                MessageBox.Show("No active document found. Please open a part or assembly file.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            // Check if the active document is a part or an assembly
            int docType = activeDoc.GetType();
            if (docType != (int)swDocumentTypes_e.swDocPART && docType != (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                // Active document is not a part or assembly
                MessageBox.Show("The active document is not a part or assembly. Please open a valid part or assembly file.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            return true; // Document is valid
        }
    }
}
