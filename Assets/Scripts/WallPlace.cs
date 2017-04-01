using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class WallPlace : MonoBehaviour {

    GestureRecognizer recognizer = new GestureRecognizer();


    // Use this for initialization
    void Start () {
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        //recognizer.TappedEvent += TapEventHandler;

    }
	
	// Update is called once per frame
	void Update () {
        //if ()
        //{

        //}
        //RaycastHit Hit;
        //Ray 

        //transform.rotation = Quaternion.LookRotation(-raycastHit.normal);

        
    }

    void OnDestroy()
    {
        //recognizer.TappedEvent -= TapEventHandler;
    }

    public void TapEventHandler()
    {

    }

}
