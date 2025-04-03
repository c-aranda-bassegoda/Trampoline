using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class Mesh : MonoBehaviour
{
    UnityEngine.Mesh mesh;
    Vertex[] vertices;
    List<Vertex> corners = new List<Vertex>();
    List<Vertex> boundry = new List<Vertex>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        List<Vector3> VertexList = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices);
        vertices = new Vertex[VertexList.Count];
        for (int i = 0; i < VertexList.Count; i++)
        {
            vertices[i] = new Vertex(5, transform.TransformPoint(VertexList[i]));
        }

        Vector3 dims = mesh.bounds.size;
        int x = (int)dims.x;
        int y = (int)dims.z;

        corners.Add(vertices[0]);
        corners.Add(vertices[x]);
        corners.Add(vertices[x*y + x]);
        corners.Add(vertices[x*y + x + y]);

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (i == 0)
                {
                    boundry.Add(vertices[j]);
                }
                else if (i == y - 1)
                {
                    boundry.Add(vertices[x * y + x + j]);
                }
                else if (j == 0 || j == x - 1)
                {
                    boundry.Add(vertices[i * x + j]);
                }
            }
        }
    }

    public Vertex[] getVertices()
    {
        return vertices;
    }

    public List<Vertex> getCorners()
    {
        return corners;
    }
    public List<Vertex> getBoundry()
    {
        return boundry;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i].position, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
