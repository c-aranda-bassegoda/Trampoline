using UnityEngine;
using System.Collections.Generic;


public class Mesh : MonoBehaviour
{
    List<Vertex> vertices;
    Vector3 v = Vector3.zero;
    int size = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // TODO: calculate positions of vertices, add spheres and attach script
                Vertex v = new Vertex(5);
                vertices.Add(v);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
