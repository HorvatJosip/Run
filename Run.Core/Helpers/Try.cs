using System;

namespace Run.Core
{
    /// <summary>
    /// Quick try-catch wrapper with common actions.
    /// </summary>
    public static class Try
    {
        /// <summary>
        /// Tries to execute the given action.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <returns>Whether or not the execution succeeded.</returns>
        public static bool To(Action action) => Get(() =>
        {
            action();
            return true;
        });

        /// <summary>
        /// Tries to get the value from the given method.
        /// </summary>
        /// <typeparam name="T">Type to get.</typeparam>
        /// <param name="getter">Method to execute.</param>
        /// <returns>Value fetched by the getter or default if the fetch fails.</returns>
        public static T Get<T>(Func<T> getter)
        {
            try
            {
                return getter();
            }
            catch (Exception ex)
            {
                Core.LogError(ex.Message, ex);
                return default(T);
            }
        }
    }
}
