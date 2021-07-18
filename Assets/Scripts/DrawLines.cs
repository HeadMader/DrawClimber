using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class DrawLines : MonoBehaviour
{

	[SerializeField]
	private Fuvk Image;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private MeshCreator meshCreatorRightLeg;
	[SerializeField]
	public MeshCreator meshCreatorLeftLeg;
	[SerializeField]
	private MeshCreator meshCreatorRightLegInvisable;
	[SerializeField]
	public MeshCreator meshCreatorLeftLegInvisable;
	public Camera cam;
	internal LineRenderer lineRenderer;
	private Vector3 mousePos;
	private Vector3 Pos;
	private Vector3 previousPos;
	public List<Vector3> linePositions;
	public float minimumDistance = 0.05f;
	private float distance = 0.00001f;
	public float dis = 2;
	Vector3 currentPosition;
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		currentPosition = transform.position;
		if (Image.isOnPlane)
		{
			DrawLine();
		}

	}
	private void DrawLine()
	{
		if (Input.GetMouseButtonDown(0))
		{
			linePositions.Clear();
			mousePos = Input.mousePosition;
			mousePos.z = dis;
			Pos = cam.ScreenToWorldPoint(mousePos);
			previousPos = Pos;
			linePositions.Add(Pos - currentPosition);
		}
		else if (Input.GetMouseButton(0))
		{
			mousePos = Input.mousePosition;
			mousePos.z = dis;
			Pos = cam.ScreenToWorldPoint(mousePos);
			distance = Vector3.Distance(Pos, previousPos);
			if (distance >= minimumDistance)
			{
				previousPos = Pos;
				linePositions.Add(Pos - currentPosition);
				lineRenderer.positionCount = linePositions.Count;
				lineRenderer.SetPositions(linePositions.ToArray());
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			lineRenderer.positionCount = 0;
			meshCreatorLeftLeg.ApplyMeshToMeshCollider();
			meshCreatorRightLeg.ApplyMeshToMeshCollider();
			meshCreatorLeftLegInvisable.ApplyMeshToMeshCollider();
			meshCreatorRightLegInvisable.ApplyMeshToMeshCollider();
		//	player.transform.position += Vector3.up*0.8f;  
		}
	}
}

