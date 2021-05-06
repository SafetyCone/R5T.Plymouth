using System;


namespace R5T.Plymouth
{
    /// <summary>
    /// A start-point class for creating application building invocations.
    /// </summary>
    /// <remarks>
    /// Instantiable (non-static) to allow the class to be a base for extensions methods.
    /// </remarks>
    public class ApplicationBuilder
    {
        /// <summary>
        /// The instance to use for extention method invocations.
        /// </summary>
        /// <remarks>
        /// Non-lazy since this will always be needed as the starting point for an application.
        /// </remarks>
        public static ApplicationBuilder Instance { get; } = new ApplicationBuilder();


        // Private constructor.
        private ApplicationBuilder()
        {
        }
    }
}
