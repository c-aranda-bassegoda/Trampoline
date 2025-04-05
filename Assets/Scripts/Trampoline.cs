using UnityEngine;
using System.Collections.Generic;

//  (done) look into spring systems: shear, bending, structural springs
public class Trampoline : Mesh
{
    List<Spring> shearSprings;
    List<Spring> bendingSprings;
    List<Spring> structuralSprings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bendingSprings = new List<Spring>();
        structuralSprings = new List<Spring>();
        shearSprings = new List<Spring>();

        createSprings();
    }

    // Update is called once per frame
    void Update()
    {

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
                    spring = new Spring(1, 1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 1]);
                    structuralSprings.Add(spring); // for debuging
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + 1].addSpring(spring);
                }
                if (j < x - 1)
                {
                    spring = new Spring(1, 1, 1,vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 2]);
                    bendingSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + 2].addSpring(spring);
                }
                if (i < y)
                {
                    spring = new Spring(1, 1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + x + 1]);
                    structuralSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + x + 1].addSpring(spring);
                }
                if (i < y - 1)
                {
                    spring = new Spring(1, 1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 2 * x + 2]);
                    bendingSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + 2 * x + 2].addSpring(spring);
                }
                if (j < x)
                {
                    spring = new Spring(1, 1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + x + 2]);
                    shearSprings.Add(spring);
                    vertices[i * (y + 1) + j].addSpring(spring);
                    vertices[i * (y + 1) + j + x + 2].addSpring(spring);
                    spring = new Spring(1, 1, vertices[i * (y + 1) + j + x + 1], vertices[i * (y + 1) + j + 1]);
                    shearSprings.Add(spring);
                    vertices[i * (y + 1) + j + x + 1].addSpring(spring);
                    vertices[i * (y + 1) + j + 1].addSpring(spring);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
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
