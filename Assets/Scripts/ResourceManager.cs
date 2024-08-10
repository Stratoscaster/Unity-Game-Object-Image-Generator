using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{

    private static ResourceManager manager;
    private Dictionary<string, Object> prefabs = new Dictionary<string, Object>();


    public static ResourceManager getInstance()
    {
        if (manager == null) {
            manager = new ResourceManager();
        }

        return manager;
    }

    public GameObject[] LoadAllPrefabsInRoot()
    {
        return Resources.LoadAll<GameObject>("");
    }

    public Object GetPrefab(string name)
    {
        if (!prefabs.ContainsKey(name)) {
            Object prefab = LoadPrefab(name);
            if (prefab == null) { return null; }
            else {
                Debug.Log("Loaded Prefab: "  + name);
            }
            prefabs.Add(name, prefab);
        }

        return prefabs[name];
    }

    Object LoadPrefab(string name)
    {

        Object prefab = Resources.Load(name) as Object;

        if (prefab == null) {
            Debug.LogError("Prefab path does not exist: " + name);
        }

        return prefab;
    }

    private ResourceManager() { }

}
