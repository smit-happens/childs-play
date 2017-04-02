using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.Windows.Speech;

public class WallPlace : MonoBehaviour {

    [Tooltip("Material to use when rendering Spatial Mapping meshes while the observer is running.")]
    public Material defaultMaterial;

    [Tooltip("Optional Material to use when rendering Spatial Mapping meshes after the observer has been stopped.")]
    public Material secondaryMaterial;

    //used for speech command input]
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    GestureRecognizer recognizer;

    //storage of points that make up the line
    ArrayList currentPoints;

    //Mode variables
    bool isDrawing = false;
    bool isMapping = true;          //default mode is mapping view

    //Current drawing color
    string currentColor = "blue";  //default color is blue

    // Use this for initialization
    void Start () {
        //setup the gesture recognizer
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += TapEventHandler;
        recognizer.StartCapturingGestures();

        // Update surfaceObserver and storedMeshes to use the same material during scanning.
        SpatialMappingManager.Instance.SetSurfaceMaterial(defaultMaterial);

        currentPoints = new ArrayList();

        #region keywordDeclaration

        keywords.Add("Map", () =>
        {
            // Call the OnMapMode method on every descendant object.
            this.BroadcastMessage("OnMapMode");
        });

        keywords.Add("Draw", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("OnDrawMode");
        });

        keywords.Add("Clear", () =>
        {
            // Call the OnMapMode method on every descendant object.
            this.BroadcastMessage("OnClear");
        });

        keywords.Add("Blue", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("SelectBlue");
        });

        keywords.Add("Red", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("SelectRed");
        });

        keywords.Add("Yellow", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("SelectYellow");
        });

        keywords.Add("Pink", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("SelectPink");
        });

        keywords.Add("Green", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("SelectGreen");
        });

        keywords.Add("White", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("SelectWhite");
        });

        keywords.Add("Orange", () =>
        {
            // Call the OnDrawMode method on every descendant object.
            this.BroadcastMessage("SelectOrange");
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

        #endregion
    }
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        if (!SpatialMappingManager.Instance.IsObserverRunning() && !isDrawing)
        {
            SpatialMappingManager.Instance.StartObserver();
            Debug.Log("Observer Is starting");
        }

        if(isDrawing)
        {
            //sphere.transform.position = currentPoint;    //TODO IMPORTANT LATER

            Vector3 point = GameObject.Find("CursorVisual").transform.position;

            currentPoints.Add(point);

            if(currentPoints.Count >= 60)
            {
                this.GetComponent<DrawALine>().SetOfLines(currentPoints, currentColor, (float)0.01);
                currentPoints.Clear();
                currentPoints.Add(point);
            }

            //transform.LookAt(Camera.main.transform);
        }

    }

    /// <summary>
    /// Handles which keyword was actually heard 
    /// </summary>
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    void OnClear()
    {
        isDrawing = false;
        this.GetComponent<DrawALine>().Clear();
    }

    void OnMapMode()
    {
        if (!isMapping)
        {
            Debug.Log("switching to map mode");

            SpatialMappingManager.Instance.SetSurfaceMaterial(defaultMaterial);
            isMapping = true;
            isDrawing = false;
        }
    }

    /// <summary>
    /// Takes you out of Mapping mode, into a "ready to draw" mode
    /// </summary>
    void OnDrawMode()
    {
        if (!isDrawing)
        {
            Debug.Log("switching to Drawing mode");

            SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);
            SpatialMappingManager.Instance.StopObserver();
            isMapping = false;
        }
    }

#region colorSelection

    void SelectBlue()
    {
        currentColor = "blue";
    }

    void SelectRed()
    {
        currentColor = "red";
    }

    void SelectYellow()
    {
        currentColor = "yellow";
    }

    void SelectOrange()
    {
        currentColor = "orange";
    }

    void SelectPink()
    {
        currentColor = "pink";
    }

    void SelectGreen()
    {
        currentColor = "green";
    }

    void SelectWhite()
    {
        currentColor = "white";
    }

    #endregion

    ///// <summary>
    ///// Creates planes from the spatial mapping surfaces.
    ///// </summary>
    //private void CreatePlanes()
    //{
    //    // Generate planes based on the spatial map.
    //    SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
    //    if (surfaceToPlanes != null && surfaceToPlanes.enabled)
    //    {
    //        surfaceToPlanes.MakePlanes();
    //    }
    //}

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

        if (!isDrawing && isMapping)
        {
            SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);
            SpatialMappingManager.Instance.StopObserver();
            //CreatePlanes();
            isDrawing = true;
            isMapping = false;

        }
        else if(isDrawing && !isMapping)
        {
            Debug.Log("Drawing = false");

            isDrawing = false;
            currentPoints.Clear();
        }
        else if (!isDrawing && !isMapping)
        {
            isDrawing = true;
        }
    }

}
