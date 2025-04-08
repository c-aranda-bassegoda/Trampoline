using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Security;



public class Mesh : MonoBehaviour
{
    protected UnityEngine.Mesh mesh;
    protected Vertex[] vertices;
    protected List<int> corners = new List<int>();
    protected List<int> boundry = new List<int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    protected virtual void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        //mesh.Clear();
        List<Vector3> VertexList = new List<Vector3>(GetComponent<MeshFilter>().mesh.vertices);
        vertices = new Vertex[VertexList.Count];
        for (int i = 0; i < VertexList.Count; i++)
        {
            GameObject obj = new GameObject();
            Vertex vertexScript = obj.AddComponent<Vertex>();
            vertexScript.mass = 1f;
            vertexScript.transform.position = VertexList[i];
            vertices[i] = vertexScript;
            vertexScript.enabled = true;
        }

        defineBoundry();
        defineCorners();

        fixBoundry();
    }
    public virtual void updateVertices()
    {
        Vector3[] new_vertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            new_vertices[i] = vertices[i].position;
        }
        mesh.vertices = new_vertices;

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
            vertices[boundry[i]].isFixed = true;
        }
    }

    public void fixCorners()
    {
        for (int i = 0; i < corners.Count; i++)
        {
            vertices[corners[i]].isFixed = true;
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

}
