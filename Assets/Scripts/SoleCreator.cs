using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoleCreator : MonoBehaviour
{
    [SerializeField]
    private DrawLines DrawLines;
    Vector3 HeightestPoint;
    [SerializeField]
    private float thickness = 0.02f;
    GameObject sole;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        float[] cordsOfPointsY = new float[DrawLines.linePositions.Count];
        for (int i = 0; i < DrawLines.linePositions.Count; i++)
        {
            cordsOfPointsY[i] = DrawLines.linePositions[i].y;
        }
        float maxValue = cordsOfPointsY.Max();
        int maxIndex = cordsOfPointsY.ToList().IndexOf(maxValue);
        HeightestPoint = new Vector3(DrawLines.linePositions[maxIndex].x, DrawLines.linePositions[maxIndex].y, DrawLines.linePositions[maxIndex].z + thickness);
        sole = new GameObject(transform.name);
        sole.transform.parent = transform.parent;
        sole.transform.position = HeightestPoint;
        sole.AddComponent<MeshCreator>();
       
    }
}
