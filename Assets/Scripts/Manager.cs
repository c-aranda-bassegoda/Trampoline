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
    private float damping_coef = 5f;

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
        createSphere();
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
                vert.accumulatedForce = Vector3.zero;
            }
            mesh.updateVertices();           
        }
        
    }
    public void applyForces(Vertex vert)
    {
        applySpring(vert);
        applyGravity(vert);
        applyDamping(vert);
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
        if (vert == null)
        {
            Debug.Log("vert is null");
        }
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
        plane.transform.GetComponent<MeshCollider>().enabled = false;
        Trampoline trampoline = plane.AddComponent<Trampoline>();
        Meshes.Add(trampoline);
    }
    public void createSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(spawn_trampoline.position.x, 10, spawn_trampoline.position.z);
        sphere.transform.localScale = new Vector3(4, 4, 4);
        Sphere test = sphere.AddComponent<Sphere>();

    }
    /*
    public void createSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(spawn_trampoline.position.x, 5, spawn_trampoline.position.z);
        sphere.transform.localScale = new Vector3(4, 4, 4);
        TestMesh test = sphere.AddComponent<TestMesh>();
        sphere.AddComponent<Rigidbody>();
        Meshes.Add(test);

    }
    */
}
