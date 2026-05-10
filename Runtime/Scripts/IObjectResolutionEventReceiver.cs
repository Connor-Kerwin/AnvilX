namespace AnvilX
{
    /// <summary>
    /// Represents an object that can receive messages upon being resolved via dependency injection.
    /// </summary>
    public interface IObjectResolutionEventReceiver
    {
        /// <summary>
        /// Called when the object has been resolved via dependency injection.
        /// </summary>
        void NotifyResolution();
    }
}