using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


// Take vertices of plane mesh
// for every timestep we calculate how vertices should behave based on connections forces
// we tell the mesh to take in new vertices 

// ToDo:
// finish createTrampoline function in manager
// test functions

public class Manager : MonoBehaviour
{
    private Vector3 gravity =  new Vector3(0f, -9.81f, 0f);
    public float damping_coef = 0.05f;

    // Debug
    float time = 0f;
    // SpawnPoint Trampoline
    public Transform spawn_trampoline;

    // List of all Meshes
    public List<Mesh> Meshes = new List<Mesh>();
    //Mesh trampoline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        createTrampoline();
        //createSphere();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Mesh mesh in Meshes)
        {
            foreach (Vertex vert in mesh.getVertices())
            {
                applyForces(vert);
                vert.updateVelocity();
                vert.updatePosition();
            }
            mesh.updateVertices();
        }
        
    }
    public void applyForces(Vertex vert)
    {
        applyGravity(vert);
        applyDamping(vert);
        applySpring(vert);
    }

    // Force Functions
    public void applyGravity(Vertex vert)
    {
        vert.accumulatedForce += gravity;
    }
    
    public void applyDamping(Vertex vert)
    {
        vert.accumulatedForce -= vert.velocity * damping_coef;
    }
    public void applySpring(Vertex vert)
    {
        foreach (Spring spring in vert.adjSprings)
        {
            Vector3 value = spring.v1.position - spring.v2.position;
            float magnitude = value.magnitude;
            float mult = spring.stiffness * (spring.restLength - magnitude) / magnitude;
            vert.accumulatedForce += value * mult;
        }   
    }   
    public void createTrampoline()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = spawn_trampoline.position;
        Trampoline trampoline = plane.AddComponent<Trampoline>();
        Meshes.Add(trampoline);
    }
    public void createSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = spawn_trampoline.position;
        Trampoline trampoline = sphere.AddComponent<Trampoline>();
        Meshes.Add(trampoline);
    }
}
