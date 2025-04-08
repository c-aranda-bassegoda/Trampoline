using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Sphere : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 _accumulatedForce;
    public float mass;
    public Rigidbody body;
    private Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        velocity = new Vector3(0, -5, 0);
        _accumulatedForce = Vector3.zero;
        mass = 5f;
        body = gameObject.AddComponent<Rigidbody>();
        body.useGravity = false; 
    }

    // Update is called once per frame
    void Update()
    {
        applyGravity();
        updateVelocity();
        updatePosition();
    }

    public void applyGravity()
    {
        _accumulatedForce += gravity;
    }
    public void updatePosition()
    {
         Vector3 tmp = gameObject.transform.position + velocity * Time.deltaTime;
        body.MovePosition(tmp);
        
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter sphere");
        Vector3 pushDirection = (transform.position - other.transform.position).normalized;
        Vector3 tmp = transform.position + pushDirection * velocity.magnitude * Time.deltaTime;
        body.MovePosition(tmp);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collisionö");
    }
    private void OnTriggerStay(Collider other)
    {
        Vertex comp = other.transform.gameObject.GetComponent<Vertex>();
        float vel = velocity.magnitude;
        if (comp != null)
        {
            vel = (((mass - comp.mass) * velocity + 2 * comp.mass * comp.velocity) / (mass + comp.mass)).magnitude;
        }
        Debug.Log("trigger stay sphere");
        Vector3 pushDirection = (transform.position - other.transform.position).normalized;
        Vector3 tmp = transform.position + pushDirection * vel * Time.deltaTime;
        body.MovePosition(tmp);
    }
    */

    public void updateVelocity()
    {
        float invMass = 1f / mass;
        velocity += _accumulatedForce * Time.deltaTime * invMass;
        _accumulatedForce = Vector3.zero;
    }
}
