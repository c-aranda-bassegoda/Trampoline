using UnityEngine;
using System.Collections.Generic;

public class Trampoline : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    List<Vector3> corners = new List<Vector3> ();
    List<Vector3> boundry = new List<Vector3>();
    List<int> corners2 = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh ();
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
