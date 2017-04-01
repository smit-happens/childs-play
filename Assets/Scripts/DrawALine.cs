using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawALine : MonoBehaviour {
	GameObject myLine;// = new GameObject();
	LineRenderer lr;// = myLine.GetComponent<LineRenderer>();
	Renderer renderer;
	// Use this for initialization
	void Start () {
		myLine = new GameObject();
		myLine.AddComponent<LineRenderer>();
		lr = myLine.GetComponent<LineRenderer>();
		//renderer.material.color = Color.cyan;
		//lr.material = renderer.material;
        //lr.material.color = Color.white;
		/*lr.material = new Material (Shader.Find("Particles/Additive"));
		lr.startColor = Color.black;
		lr.endColor = Color.white;*/
        lr.startWidth = (float)0.01;
        lr.endWidth = (float)1.0;
        //myLine.transform.position = new Vector3( (float)0.0,  (float)0.0,  (float)10.0);
        int count = 0;
		lr.positionCount = 11;
		for (int i = -5; i <= 5; i++) {
			Vector3 start = new Vector3((float)i, (float)i, (float)(i+5)/(float)10.0 +  (float)10.0);
			Vector3 end = new Vector3((float)i+(float)1.0,(float)i+(float)1.0,(float)(i+5)/(float)10.0 +  (float)10.0);

			lr.SetPosition (count,start);
			lr.SetPosition(count++, end);
		}
		/*Vector3 start = new Vector3((float)1.0, (float)1.0, (float)1.0);
		Vector3 end = new Vector3((float)2.0,(float)2.0,(float)2.0);
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(Color.red, Color.blue);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition (0,start);
		lr.SetPosition(1, end);*/
		//GameObject.Destroy(myLine, duration);
	}
	
	// Update is called once per frame
	void Update () {
		int count = 0;
        lr.positionCount = 11;
        lr.startWidth = (float)0.01;
        lr.endWidth = (float)0.01;
        for (int i = -5; i <= 5; i++) {
			Vector3 start = new Vector3((float)i, (float)i, (float)2.0);
			Vector3 end = new Vector3((float)i+(float)1.0,(float)i+(float)1.0,(float)2.0);

			lr.SetPosition (count,start);
			lr.SetPosition(count++, end);
		}
	}
}
