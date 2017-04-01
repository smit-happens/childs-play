using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class WallPlace : MonoBehaviour {

    GestureRecognizer recognizer;
    private MeshRenderer currentMesh;

    // Use this for initialization
    void Start () {
        //setup the gesture recognizer
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += TapEventHandler;
        recognizer.StartCapturingGestures();

        //save the mesh renderer that's on the same object as this script
        currentMesh = this.gameObject.GetComponentInChildren<MeshRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            Debug.Log("True");
        }
        else
        {
            Debug.Log("False");
        }
        //RaycastHit Hit;
        //Ray 

        //transform.rotation = Quaternion.LookRotation(-raycastHit.normal);


    }

    void OnDestroy()
    {
        recognizer.TappedEvent -= TapEventHandler;
    }

    void TapEventHandler(InteractionSourceKind source, int tapNum, Ray headRay)
    {
        Debug.Log("Pinch received!");

        
    }

}
