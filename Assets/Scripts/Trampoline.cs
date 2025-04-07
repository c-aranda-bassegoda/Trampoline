using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

//  (done) look into spring systems: shear, bending, structural springs
public class Trampoline : Mesh
{
    List<Spring> shearSprings;
    List<Spring> bendingSprings;
    List<Spring> structuralSprings;
    public float stiffness = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    protected override void Start()
    {
        base.Start();
        Debug.Log("Verticees Length " + vertices.Length);
        bendingSprings = new List<Spring>();
        structuralSprings = new List<Spring>();
        shearSprings = new List<Spring>();

        createSprings();
        Collider collider = GetComponent<Collider>();


    }


    public override void updateVertices()
    {
        Debug.Log("updateVertices trampoline");
        base.updateVertices();
    }
    void createSprings()
    {
        Vector3 dims = mesh.bounds.size;
        int x = (int)dims.x;
        int y = (int)dims.z;

        Spring spring;
        for (int i = 0; i <= y; i++)
        {
            for (int j = 0; j <= x; j++)
            {
                if (j < x)
                {
                    spring = new Spring(1, stiffness, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 1]);
                    structuralSprings.Add(spring); // for debuging
                    if (vertices[i * (y + 1) + j] == null)
                    {
                        Debug.Log("vertex is null");
                    }
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + 1].addSpring(new Spring(1, stiffness, vertices[i * (y + 1) + j + 1], vertices[i * (y + 1) + j]));
                }
                if (j<x - 1)
                {
                    spring = new Spring(2, stiffness, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 2]);
                    bendingSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + 2].addSpring(new Spring(2, stiffness, vertices[i * (y + 1) + j + 2], vertices[i * (y + 1) + j]));
                }
                if (i < y)
                {
                    spring = new Spring(1, stiffness, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + x + 1]);
                    structuralSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + x + 1].addSpring(new Spring(1, stiffness, vertices[i * (y + 1) + j + x + 1], vertices[i * (y + 1) + j]));
                }
                if (i < y - 1)
                {
                    spring = new Spring(2, stiffness, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 2 * x + 2]);
                    bendingSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + 2 * x + 2].addSpring(new Spring(2, stiffness, vertices[i * (y + 1) + j + 2 * x + 2], vertices[i * (y + 1) + j]));
                }
                if (j < x && i < y)
                {
                    spring = new Spring(1.41f, 100, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + x + 2]);
                    shearSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + x + 2].addSpring(new Spring(1.41f, 100, vertices[i * (y + 1) + j + x + 2], vertices[i * (y + 1) + j]));
                    spring = new Spring(1.41f, 100, vertices[i * (y + 1) + j + x + 1], vertices[i * (y + 1) + j + 1]);
                    shearSprings.Add(spring);
                    vertices[i * (y + 1) + j + x + 1].addSpring(spring);
                    vertices[i * (y + 1) + j + 1].addSpring(new Spring(1.41f, 100, vertices[i * (y + 1) + j + 1], vertices[i * (y + 1) + j + x + 1]));
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < structuralSprings.Count; i++)
        {
            Gizmos.DrawLine(structuralSprings[i].v1.position, structuralSprings[i].v2.position);
        }
        Gizmos.color = Color.red;
        for (int i = 0; i < bendingSprings.Count; i++)
        {
            Gizmos.DrawLine(bendingSprings[i].v1.position, bendingSprings[i].v2.position);
        }
        Gizmos.color = Color.cyan;
        for (int i = 0; i < shearSprings.Count; i++)
        {
            Gizmos.DrawLine(shearSprings[i].v1.position, shearSprings[i].v2.position);
        }

    }
}
