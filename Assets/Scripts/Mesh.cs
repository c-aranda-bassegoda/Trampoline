using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class Mesh : MonoBehaviour
{
    UnityEngine.Mesh mesh;
    Vertex[] vertices;
    List<int> corners = new List<int>();
    List<int> boundry = new List<int>();
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

        defineBoundry();
        defineCorners();

        fixBoundry();
    }

    public Vertex[] getVertices()
    {
        return vertices;
    }
    public List<int> getCornerIdx()
    {
        return corners;
    }
    public List<int> getBoundryIdx()
    {
        return boundry;
    }
    public void defineCorners()
    {
        Vector3 dims = mesh.bounds.size;
        int x = (int)dims.x;
        int y = (int)dims.z;

        corners.Add(0);
        corners.Add(x);
        corners.Add(x * y + x);
        corners.Add(x * y + x + y);
    }
    public void defineBoundry()
    {
        Vector3 dims = mesh.bounds.size;
        int x = (int)dims.x;
        int y = (int)dims.z;

        for (int i = 0; i <= y; i++)
        {
            for (int j = 0; j <= x; j++)
            {
                if (i == 0)
                {
                    boundry.Add(j);
                }
                else if (i == y)
                {
                    boundry.Add(x * y + x + j);
                }
                else if (j == 0 || j == x)
                {
                    boundry.Add(i * (x + 1) + j);
                }
            }
        }
    }

    public void fixBoundry()
    {
        for (int i = 0; i < boundry.Count; i++)
        {
            vertices[boundry[i]].setFixed(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < boundry.Count; i++)
        {
            Gizmos.DrawSphere(vertices[boundry[i]].position, 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
