using System.Collections.Generic;
using UnityEngine;

public class TestMesh : Mesh
{
    public float stiffness = 50f;
    List<Spring> structuralSprings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Start()
    {

        structuralSprings = new List<Spring>();
        vertices = new Vertex[2];
        vertices[0] = new Vertex(5f, transform.TransformPoint(new Vector3(0f,0f,0f)));
        vertices[1] = new Vertex(5f, transform.TransformPoint(new Vector3(0f, -2f, 0f)));
        vertices[0].isFixed = true;

    }
}
