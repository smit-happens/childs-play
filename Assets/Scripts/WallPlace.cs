using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity.SpatialMapping;

public class WallPlace : MonoBehaviour {

    GestureRecognizer recognizer;
    bool isDraw = false;

    [Tooltip("How much time (in seconds) that the SurfaceObserver will run after being started; used when 'Limit Scanning By Time' is checked.")]
    public float scanTime = 30.0f;

    [Tooltip("Material to use when rendering Spatial Mapping meshes while the observer is running.")]
    public Material defaultMaterial;

    [Tooltip("Optional Material to use when rendering Spatial Mapping meshes after the observer has been stopped.")]
    public Material secondaryMaterial;

    // Use this for initialization
    void Start () {
        //setup the gesture recognizer
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += TapEventHandler;
        recognizer.StartCapturingGestures();

        // Update surfaceObserver and storedMeshes to use the same material during scanning.
        SpatialMappingManager.Instance.SetSurfaceMaterial(defaultMaterial);

    }
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        if (!SpatialMappingManager.Instance.IsObserverRunning() && !isDraw)
        {
            SpatialMappingManager.Instance.StartObserver();
            Debug.Log("Observer Is starting");
        }
    }

    /// <summary>
    /// Creates planes from the spatial mapping surfaces.
    /// </summary>
    private void CreatePlanes()
    {
        // Generate planes based on the spatial map.
        SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
        if (surfaceToPlanes != null && surfaceToPlanes.enabled)
        {
            surfaceToPlanes.MakePlanes();
        }
    }

    /// <summary>
    /// Removes triangles from the spatial mapping surfaces.
    /// </summary>
    /// <param name="boundingObjects"></param>
    private void RemoveVertices(IEnumerable<GameObject> boundingObjects)
    {
        RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
        if (removeVerts != null && removeVerts.enabled)
        {
            removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
        }
    }

    void OnDestroy()
    {
        recognizer.TappedEvent -= TapEventHandler;
    }

    void TapEventHandler(InteractionSourceKind source, int tapNum, Ray headRay)
    {
        Debug.Log("Pinch received!");


        if (!isDraw)
        {
            SpatialMappingManager.Instance.StopObserver();
            CreatePlanes();
            isDraw = true;
        }
        SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);
    }

}
