using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnvilX
{
    /// <summary>
    /// Provides a centralised mechanism for getting hold of the DontDestroyOnLoad scene.
    /// </summary>
    public static class GlobalSceneTracker
    {
        private static GameObject globalSceneTracker;

        /// <summary>
        /// Get the Scene that represents DontDestroyOnLoad.
        /// </summary>
        /// <remarks>
        /// This method will result in DontDestroyOnLoad being created if nothing was previously marked.
        /// </remarks>
        /// <returns></returns>
        public static Scene GetGlobalScene()
        {
            if (globalSceneTracker)
            {
                return globalSceneTracker.scene;
            }
            
            globalSceneTracker = new GameObject("Global Scene Tracker")
            {
                // NOTE: If we use HideAndDontSave here, it breaks sometimes!
                hideFlags = HideFlags.HideInHierarchy
            };
            
            Object.DontDestroyOnLoad(globalSceneTracker);

            return globalSceneTracker.scene;
        }
    }
}