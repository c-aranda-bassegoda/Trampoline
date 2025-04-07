using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    public float mass;
    public Vector3 position => transform.position;
    bool _isFixed = false;
    //SphereCollider collider;
    public bool isFixed { get => _isFixed; set { _isFixed = value; } }

    Vector3 _velocity = Vector3.zero;
    public Vector3 velocity { get => _velocity; set { _velocity = value; } }

    Vector3 _accumulatedForce = Vector3.zero;
    public Vector3 accumulatedForce { get => _accumulatedForce; set { _accumulatedForce = value; } }

    List<Spring> _adjSprings = new List<Spring>();
    public List<Spring> adjSprings { get => _adjSprings; set { _adjSprings = value; } }

    public void addSpring(Spring spring) { _adjSprings.Add(spring); }

    /*
    public Vertex(float mass, Vector3 position)
    {
        this.mass = mass;
        this.position = position;
        //collider = new SphereCollider();
        //collider.transform.position = position;
        
    }
    */
    public void Awake()
    {
        _adjSprings = new List<Spring>();
        _velocity = Vector3.zero;
        _isFixed = false;
        _accumulatedForce = Vector3.zero;
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 0.15f;

    }

    public void updatePosition()
    {
        if (!isFixed)
        {
            gameObject.transform.position += velocity * Time.deltaTime;
            //collider.transform.position = position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter vertex");
        //Vector3 pushDirection = (transform.position - other.transform.position).normalized;
        //transform.position += pushDirection * velocity.magnitude * Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("trigger stay vertex");
        Vector3 pushDirection = (transform.position - other.transform.position).normalized;
        transform.position += pushDirection * velocity.magnitude * Time.deltaTime;
    }

    public void updateVelocity()
    {
        float invMass = 1f / mass;
        _velocity += _accumulatedForce * Time.deltaTime * invMass;
        _accumulatedForce = Vector3.zero;
    }
}
