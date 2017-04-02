using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawALine : MonoBehaviour {
	GameObject myLine;// = new GameObject();
	LineRenderer lr;// = myLine.GetComponent<LineRenderer>();
	Renderer renderer;
	int totalPieces;
	Vector3[] allPoints;
	Color colorUsed;
	// Use this for initialization
	void Start () {
		
		/*myLine = new GameObject();
		myLine.AddComponent<LineRenderer>();
		lr = myLine.GetComponent<LineRenderer>();
		//renderer.material.color = Color.cyan;
		//lr.material = renderer.material;
        //lr.material.color = Color.white;
		lr.material = new Material (Shader.Find("Particles/Additive"));
		lr.startColor = Color.black;
		lr.endColor = Color.white;
        lr.startWidth = (float)0.01;
        lr.endWidth = (float)1.0;
        //myLine.transform.position = new Vector3( (float)0.0,  (float)0.0,  (float)10.0);
        int count = 0;
		lr.positionCount = 11;*/
		Vector3[] points = new Vector3[11];

		for (int i = -5; i <= 5; i++) {
			points[i+5] = new Vector3((float)i, (float)i, (float)2.0);

		}
		setOfLines (points, "black", (float)0.01);
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

	void setOfLines(Vector3[] points, string color, float width)
	{
		myLine = new GameObject();
		myLine.AddComponent<LineRenderer>();
		lr = myLine.GetComponent<LineRenderer>();

		if (color.Equals ("black")) {
			lr.startColor = Color.black;
			lr.endColor = Color.black;
		}
		else if(color.Equals("blue")) {
			lr.startColor = Color.blue;
			lr.endColor = Color.blue;
		}
		else if(color.Equals("red")) {
			lr.startColor = Color.red;
			lr.endColor = Color.red;
		}
		else if(color.Equals("yellow")) {
			lr.startColor = Color.yellow;
			lr.endColor = Color.yellow;
		}
		else if(color.Equals("purple")) {
			lr.startColor = Color.magenta;
			lr.endColor = Color.magenta;
		}
		else if(color.Equals("green")) {
			lr.startColor = Color.green;
			lr.endColor = Color.green;
		}
		else if(color.Equals("white")) {
			lr.startColor = Color.white;
			lr.endColor = Color.white;
		}
			
		lr.startWidth = width;
		lr.endWidth = width;
		totalPieces = points.Length;
		allPoints = points;
		lr.positionCount = totalPieces;
		lr.GetPositions (points);
	}
	
	// Update is called once per frame
	void Update () {
		int count = 0;
        lr.positionCount = totalPieces;
        //lr.startWidth = (float)0.01;
        //lr.endWidth = (float)0.01;
        for (int i = -5; i <= 5; i++) {
			Vector3 start = new Vector3((float)i, (float)i, (float)2.0);
			Vector3 end = new Vector3((float)i+(float)1.0,(float)i+(float)1.0,(float)2.0);

			lr.SetPosition (count,start);
			lr.SetPosition(count++, end);
		}
	}
}
