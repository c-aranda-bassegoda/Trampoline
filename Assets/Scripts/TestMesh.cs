using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestMesh : Mesh
{
    GameObject obj;
    protected override void Start()
    {
        vertices = new Vertex[1];
        obj = new GameObject();
        Vertex vertexScript = obj.AddComponent<Vertex>();
        vertexScript.mass = 5f;
        vertexScript.transform.position = transform.position;
        vertices[0] = vertexScript;
        vertexScript.enabled = true;
        GetComponent<SphereCollider>().enabled = true;
        vertexScript.GetComponent<SphereCollider>().enabled = false;        
    }

    public override void updateVertices()
    {
        Rigidbody tmp = GetComponent<Rigidbody>();
        tmp.position = vertices[0].position;
        transform.position = vertices[0].position;
    }
    private void Update()
    {
        updateVertices();
        /*
        Rigidbody tmp = GetComponent<Rigidbody>();
        tmp.position = vertices[0].position;
        obj.transform.position = new Vector3(tmp.position.x, tmp.position.y - 2, tmp.position.z);
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter vertex");
        Vector3 pushDirection = (transform.position - other.transform.position).normalized;
        transform.position += pushDirection * vertices[0].velocity.magnitude * Time.deltaTime;
        vertices[0].transform.position = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collisionö");
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("trigger stay vertex");
        Vector3 pushDirection = (transform.position - other.transform.position).normalized;
        transform.position += pushDirection * vertices[0].velocity.magnitude * Time.deltaTime;
        vertices[0].transform.position = transform.position;
    }
}
