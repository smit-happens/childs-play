using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawALine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject myLine = new GameObject();
		Vector3 start = new Vector3((float)1.0, (float)1.0, (float)1.0);
		Vector3 end = new Vector3((float)2.0,(float)2.0,(float)2.0);
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(Color.red, Color.blue);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition (0,start);
		lr.SetPosition(1, end);
		//GameObject.Destroy(myLine, duration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
