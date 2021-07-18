using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshCreator : MonoBehaviour
{
	[SerializeField]
	private DrawLines DrawLines;
	[SerializeField]
	private float thickness = 1f;
	[SerializeField]
	private Transform cube;
	GameObject sole;
	[SerializeField]
	Transform soleParent;
	[SerializeField]
	Transform zx;
	[SerializeField]
	Transform zy;
	private Mesh _mesh;
	private MeshCollider meshCollider;
	private int drawlinesCount;
	private Vector3[] x1;
	private Vector3[] x2;
	private Vector3[] x3;
	private Vector3[] x4;
	private Vector3[] x5;
	private Vector3[] x6;
	private Vector3[] x7;
	private Vector3[] x8;
	private Vector3[] x9;

	private Vector3 firstPoint;
	private Vector3 secondPoint;
	private Vector3 perpendicularToFirstPoint;
	private Vector3 perpendicularToSecondPoint;
	private void Start()
	{
		meshCollider = GetComponent<MeshCollider>();
	}

	void MeshGenerator()
	{
		x1 = new Vector3[DrawLines.linePositions.Count];
		x2 = new Vector3[DrawLines.linePositions.Count];
		x3 = new Vector3[DrawLines.linePositions.Count * 2];
		x4 = new Vector3[DrawLines.linePositions.Count * 2];
		x5 = new Vector3[DrawLines.linePositions.Count * 4];
		x6 = new Vector3[DrawLines.linePositions.Count * 2];
		x7 = new Vector3[DrawLines.linePositions.Count * 2];
		x8 = new Vector3[DrawLines.linePositions.Count * 4];
		x9 = new Vector3[DrawLines.linePositions.Count * 8];

		if (DrawLines.linePositions.Count < 2)
			return;

		_mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = _mesh;
		_mesh.name = "Sole";

		for (int i = 0; i < DrawLines.linePositions.Count - 1; i++)
		{
			firstPoint = DrawLines.linePositions[i];
			secondPoint = DrawLines.linePositions[i + 1];

			Vector3 fs = (secondPoint - firstPoint);
			Vector3 xy = Vector3.ProjectOnPlane((zx.position - zy.position), Vector3.up);
			Vector2 xz = Vector2.Perpendicular(new Vector2(xy.x, xy.z)).normalized * 0.12f;
			Vector3 newXZ = new Vector3(xz.x, 0, xz.y);

			perpendicularToFirstPoint = (Vector3.Cross(fs, newXZ).normalized) * thickness;
			perpendicularToSecondPoint = (Vector3.Cross(-fs, newXZ).normalized) * thickness;

			if (x1[0] == Vector3.zero)
			{
				x1[0] = perpendicularToSecondPoint + firstPoint;
				x2[0] = perpendicularToFirstPoint + firstPoint;
			}
			x1[i + 1] = perpendicularToSecondPoint + secondPoint;
			x2[i + 1] = perpendicularToFirstPoint + secondPoint;
		}
		#region ShitCode
		int k = 0;

		var result = x1.Concat(x2);
		var result1 = x2.Concat(x1);

		foreach (Vector3 item in result)
		{
			x3[k] = item;
			x4[k] = item;
			Vector3 xy = Vector3.ProjectOnPlane((zx.position - zy.position), Vector3.up);
			Vector2 xz = Vector2.Perpendicular(new Vector2(xy.x, xy.z)).normalized * thickness * 2;

			x4[k] = new Vector3(x3[k].x + xz.x, x3[k].y, x3[k].z + xz.y);
			k++;
		}
		k = 0;
		foreach (Vector3 item in result1)
		{
			x6[k] = item;
			x7[k] = item;
			k++;
		}
		k = 0;
		var sec = x3.Concat(x4);
		var foy = x6.Concat(x7);

		foreach (Vector3 item in foy)
		{
			x8[k] = item;
			k++;
		}
		k = 0;
		foreach (Vector3 item in sec)
		{
			x5[k] = item;
			k++;
		}
		k = 0;
		var teg = x5.Concat(x8);
		foreach (Vector3 item in teg)
		{
			x9[k] = item;
			k++;
		}


		_mesh.vertices = x9;
		int[] triangles = new int[(x2.Length - 1) * 24];
		int ti = 0, vi = 0;
		int di = triangles.Length / 4, gi = 0;
		int mi = triangles.Length / 2, ni = x5.Length / 2;
		int fi = triangles.Length * 3 / 4, ki = x5.Length / 4;
		for (int x = 0; x < x1.Length - 1; x++, ti += 6, vi++)
		{
			triangles[ti] = vi;
			triangles[ti + 1] = triangles[ti + 4] = vi + x3.Length;
			triangles[ti + 2] = triangles[ti + 3] = vi + 1;
			triangles[ti + 5] = vi + x3.Length + 1;
		}
		for (int x = 0; x < x1.Length - 1; x++, di += 6, gi++)
		{
			triangles[di] = gi;
			triangles[di + 2] = triangles[di + 5] = gi + x1.Length;
			triangles[di + 1] = triangles[di + 3] = gi + 1;
			triangles[di + 4] = gi + x1.Length + 1;
		}
		for (int x = 0; x < x1.Length - 1; x++, mi += 6, ni++)
		{
			triangles[mi] = ni;
			triangles[mi + 1] = triangles[mi + 4] = ni + x1.Length;
			triangles[mi + 2] = triangles[mi + 3] = ni + 1;
			triangles[mi + 5] = ni + x1.Length + 1;
		}
		for (int x = 0; x < x1.Length - 1; x++, fi += 6, ki++)
		{
			triangles[fi] = ki;
			triangles[fi + 2] = triangles[fi + 5] = ki + x3.Length;
			triangles[fi + 1] = triangles[fi + 3] = ki + 1;
			triangles[fi + 4] = ki + x3.Length + 1;
		}

		#endregion
		_mesh.triangles = triangles;
		_mesh.RecalculateNormals();
	}
	private void CreateCubes()
	{
		Vector3 xy = Vector3.ProjectOnPlane((zx.position - zy.position), Vector3.up);
		Vector2 xz = Vector2.Perpendicular(new Vector2(xy.x, xy.z)).normalized * thickness;
		Vector3 FirstPoint = new Vector3(DrawLines.linePositions[0].x + xz.x, DrawLines.linePositions[0].y, DrawLines.linePositions[0].z + xz.y);
		Vector3 LastPoint = new Vector3(DrawLines.linePositions.Last().x + xz.x, DrawLines.linePositions.Last().y, DrawLines.linePositions.Last().z + xz.y);

		float[] cordsOfPointsY = new float[DrawLines.linePositions.Count];
		for (int i = 0; i < DrawLines.linePositions.Count; i++)
		{
			cordsOfPointsY[i] = DrawLines.linePositions[i].y;
		}
		//	float maxValue = cordsOfPointsY.Max();
		//	int maxIndex = cordsOfPointsY.ToList().IndexOf(maxValue);
		//Vector3 HeightestPoint = new Vector3(DrawLines.linePositions[maxIndex].x + xz.x, DrawLines.linePositions[maxIndex].y, DrawLines.linePositions[maxIndex].z + xz.y);
		sole = new GameObject("Sole");
		sole.AddComponent<SoleParent>().parent = soleParent;
		sole.GetComponent<SoleParent>().drawLines = DrawLines;
		sole.transform.position = FirstPoint;
		transform.parent = sole.transform;

		Instantiate(cube, FirstPoint, DrawLines.cam.transform.rotation, sole.transform);
		Instantiate(cube, LastPoint, DrawLines.cam.transform.rotation, sole.transform);
	}
	public void ApplyMeshToMeshCollider()
	{
		if (transform.parent != null)
		{
			GameObject so = transform.parent.gameObject;
			transform.parent = null;
			transform.position = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;

			Destroy(so);
		}
		MeshGenerator();
		CreateCubes();
		meshCollider.sharedMesh = _mesh;
		meshCollider.convex = true;
	}

}
