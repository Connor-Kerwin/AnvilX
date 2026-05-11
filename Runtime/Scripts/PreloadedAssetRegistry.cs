using System.Collections;
using System.Collections.Generic;

public class PreloadedAssetRegistry<T> : IReadOnlyList<T>
    where T : UnityEngine.Object
{
    private readonly List<T> _index;

    public int Count => _index.Count;

    public T this[int index] => _index[index];
    
    private PreloadedAssetRegistry()
    {
        _index = new List<T>();
        CollectAssets();
    }

    private void CollectAssets()
    {
#if UNITY_EDITOR
        // In editor, rely on the preloaded assets
        var preloaded = UnityEditor.PlayerSettings.GetPreloadedAssets();
        foreach (var asset in preloaded)
        {
            if (asset is T expected)
            {
                _index.Add(expected);
            }
        }
#else
        // At runtime, rely on Resources
        _index.AddRange(UnityEngine.Resources.FindObjectsOfTypeAll<T>());
#endif
    }

    private static PreloadedAssetRegistry<T> instance;

    public static PreloadedAssetRegistry<T> GetRegistry()
    {
        if (instance == null)
        {
            instance = new PreloadedAssetRegistry<T>();
        }

        return instance;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _index.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}