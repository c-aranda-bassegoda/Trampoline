using UnityEngine;
using System.Collections.Generic;

// look into spring systems: shear, bending, structural springs
public class Trampoline : Mesh
{
    float k = 1; // spring constant
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
        for (int i = 0; i <= y; i++)
        {
            for (int j = 0; j <= x; j++)
            {
                if (j < x)
                    structuralSprings.Add(new Spring(1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 1]));
                if (j < x - 1)
                    bendingSprings.Add(new Spring(1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 2]));
                if (i < y)
                    structuralSprings.Add(new Spring(1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + x + 1]));
                if (i < y - 1)
                    bendingSprings.Add(new Spring(1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + 2 * x + 2]));
                if (j < x)
                {
                    shearSprings.Add(new Spring(1, 1, vertices[i * (y + 1) + j], vertices[i * (y + 1) + j + x + 2]));
                    shearSprings.Add(new Spring(1, 1, vertices[i * (y + 1) + j + x + 1], vertices[i * (y + 1) + j + 1]));
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
