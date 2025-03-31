using UnityEngine;
using System.Collections.Generic;

public class Trampoline : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    List<Vector3> corners = new List<Vector3> ();
    List<int> corners2 = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        List<Vector3> VertexList = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices);
        corners.Add(transform.TransformPoint(VertexList[0]));
        corners.Add(transform.TransformPoint(VertexList[10]));
        corners.Add(transform.TransformPoint(VertexList[110]));
        corners.Add(transform.TransformPoint(VertexList[120]));
        corners2.Add(0); corners2.Add(10); corners2.Add(110); corners2.Add(120);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (corners2.Contains(i))
            {
                continue;
            }
            vertices[i] += Vector3.up * Time.deltaTime;
        }

        // assign the local vertices array into the vertices array of the Mesh.
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
