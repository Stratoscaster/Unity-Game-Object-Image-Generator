using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSnapshotGenerator : MonoBehaviour
{
    [HideInInspector]
    public GameObject objectToSnapshot;
    [HideInInspector]
    public GameObject[] objectsToSnapshot;
    [HideInInspector]
    public Color backgroundColor = Color.clear;
    [SerializeField]
    public Vector3 position = new Vector3(0, -0.25f, 1);
    [SerializeField]
    public Vector3 rotation = new Vector3(15f, 130f, -10f);
    [SerializeField]
    public float scalingCeiling = 0.9f;

    private SnapshotCamera snapshotCamera;
    private Texture2D texture;

    private bool snapshotTriggered = false;

    Dictionary<GameObject, float> gameObjectToScale = new Dictionary<GameObject, float>();

    void Start()
    {
        snapshotCamera = SnapshotCamera.MakeSnapshotCamera("SnapshotLayer");
        LoadResources();
    }

    void LoadResources()
    {
        objectsToSnapshot = ResourceManager.getInstance().LoadAllPrefabsInRoot();

        foreach (GameObject obj in objectsToSnapshot) {
            Renderer renderer = obj.GetComponent<Renderer>();
            // TODO: Handle Child Renderer components
            // TODO: Calculate minimum bounds
            if (renderer == null) {
                continue;
            }
            Vector3 size = renderer.bounds.size;
            // Find largest dimension
            float largestDim = Mathf.Max(size.x, size.y, size.z);
            // Scale the entire GameObject down based on the largest dimension
            float scaleFactor = scalingCeiling / largestDim;
            gameObjectToScale[obj] = scaleFactor;
            Debug.Log(obj.name + " size: " + size.ToString());
        }

    }

    void OnGUI()
    {
        GUI.TextField(new Rect(10, 5, 275, 21), "Press \"Spacebar\" to save the snapshot");

        if (texture != null) {
            GUI.backgroundColor = Color.clear;
            GUI.Box(new Rect(10, 32, texture.width, texture.height), texture);
        }
    }

    public void UpdatePreview(float scaleMagnitude)
    {
        if (objectToSnapshot != null) {
            // Destroy the texture to prevent a memory leak
            // For a bit of fun you can try removing this and watching the memory profiler while for example continuously changing the rotation to trigger UpdatePreview()
            Object.Destroy(texture);

            // Take a new snapshot of the objectToSnapshot
            texture = snapshotCamera.TakePrefabSnapshot(objectToSnapshot, backgroundColor, position, Quaternion.Euler(rotation),
                new Vector3(scaleMagnitude, scaleMagnitude, scaleMagnitude), width: 512, height: 512);
        }
    }

    void Update()
    {
        // Save a PNG of the snapshot when pressing space
        if (Input.GetKeyUp(KeyCode.Space) && !snapshotTriggered) {
            foreach (GameObject obj in gameObjectToScale.Keys) {
                objectToSnapshot = obj;
                UpdatePreview(gameObjectToScale[obj]);
                if (texture != null) {
                    System.IO.FileInfo fi = SnapshotCamera.SavePNG(texture, objectToSnapshot.name);
                    Debug.Log(string.Format("Snapshot {0} saved to {1}", fi.Name, fi.DirectoryName));
                }
            }
            snapshotTriggered = true;
        }
    }
}
