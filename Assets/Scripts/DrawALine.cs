using System.Collections;
using UnityEngine;

public struct Line
{
    public int totalPieces;
    public Vector3[] allPoints;
    public Color colorUsed;
    public GameObject myLine;// = new GameObject();
    public LineRenderer lr;// = myLine.GetComponent<LineRenderer>();
}

public class DrawALine : MonoBehaviour
{

    ArrayList lineList;
    // Use this for initialization
    void Start()
    {

        lineList = new ArrayList();
        /*Vector3[] points = new Vector3[11];
        Vector3[] points2 = new Vector3[11];
        for (int i = -5; i <= 5; i++)
        {
            points[i + 5] = new Vector3((float)i, (float)0.0, (float)2.0);
            points2[i + 5] = new Vector3((float)i, (float)i, (float)2.0);
        }
        setOfLines(points, "orange", (float)0.01);
        setOfLines(points2, "blue", (float)0.01);*/

    }

    public void SetOfLines(ArrayList points, string color, float width)
    {   
        Line currentLine = new Line();
        currentLine.myLine = new GameObject();
        currentLine.myLine.AddComponent<LineRenderer>();
        currentLine.lr = currentLine.myLine.GetComponent<LineRenderer>();
        currentLine.lr.material = new Material(Shader.Find("Particles/Additive"));

        if (color.Equals("black"))
        {
            currentLine.lr.startColor = Color.black;
            currentLine.lr.endColor = Color.black;
        }
        else if (color.Equals("blue"))
        {
            currentLine.lr.startColor = Color.blue;
            currentLine.lr.endColor = Color.blue;
        }
        else if (color.Equals("red"))
        {
            currentLine.lr.startColor = Color.red;
            currentLine.lr.endColor = Color.red;
        }
        else if (color.Equals("yellow"))
        {
            currentLine.lr.startColor = Color.yellow;
            currentLine.lr.endColor = Color.yellow;
        }
        else if (color.Equals("orange"))
        {
            currentLine.lr.startColor = new Color((float)0.9, (float)0.4, (float)0, (float)1.0);
            currentLine.lr.startColor = new Color((float)0.9, (float)0.4, (float)0, (float)1.0);
        }
        else if (color.Equals("purple"))
        {
            currentLine.lr.startColor = Color.magenta;
            currentLine.lr.endColor = Color.magenta;
        }
        else if (color.Equals("green"))
        {
            currentLine.lr.startColor = Color.green;
            currentLine.lr.endColor = Color.green;
        }
        else if (color.Equals("white"))
        {
            currentLine.lr.startColor = Color.white;
            currentLine.lr.endColor = Color.white;
        }

        currentLine.colorUsed = currentLine.lr.startColor;
        currentLine.lr.startWidth = width;
        currentLine.lr.endWidth = width;
        currentLine.totalPieces = points.Count;
        currentLine.allPoints = new Vector3[currentLine.totalPieces];
        currentLine.lr.positionCount = currentLine.totalPieces;
        Vector3[] pointsArray = new Vector3[points.Count];
        for(int i = 0; i < points.Count; i++)
        {
            pointsArray[i] = (Vector3)points[i];
        }
        currentLine.lr.SetPositions(pointsArray);
        currentLine.allPoints = pointsArray;
        lineList.Add(currentLine);
    }

    // Update is called once per frame
    void Update()
    {
        Line currentLine;
        for (int i = 0; i < lineList.Count; i++)
        {
            currentLine = (Line)lineList[i];
            currentLine.lr.positionCount = currentLine.totalPieces;

            currentLine.lr.material = new Material(Shader.Find("Particles/Additive"));
            currentLine.lr.startColor = currentLine.colorUsed;
            currentLine.lr.endColor = currentLine.colorUsed;

            currentLine.lr.SetPositions(currentLine.allPoints);
        }

    }

    public void Clear()
    {
        lineList.Clear();
    }
}