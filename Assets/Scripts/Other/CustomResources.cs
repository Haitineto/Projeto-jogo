using System.Collections.Generic;

using UnityEngine;
using Object = UnityEngine.Object;

public class CustomResources
{
    private static Dictionary<string, Object> _recursos = new Dictionary<string, Object>();

    public static T Load<T>(string path) where T : Object
    {
        if (!_recursos.ContainsKey(path))
            _recursos.Add(path, Resources.Load<T>(path));
        return (T)_recursos[path];
    }
}

