using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vertex
{
    public float mass;
    public Vector3 position;
    bool _isFixed = false;
    public bool isFixed { get => _isFixed; set { _isFixed = value; } }

    Vector3 _velocity = Vector3.zero;
    public Vector3 velocity { get => _velocity; set { _velocity = value; } }

    Vector3 _accumulatedForce = Vector3.zero;
    public Vector3 accumulatedForce { get => _accumulatedForce; set { _accumulatedForce = value; } }

    List<Spring> _adjSprings = new List<Spring>();
    public List<Spring> adjSprings { get => _adjSprings; set { _adjSprings = value; } }

    public void addSpring(Spring spring) { _adjSprings.Add(spring); }

    public Vertex(float mass, Vector3 position)
    {
        this.mass = mass;
        this.position = position;
    }

    public void updatePosition()
    {
        if (!isFixed)
        {
            position += velocity * Time.deltaTime;
        }
    }    

    public void updateVelocity()
    {
        float invMass = 1f / mass;
        _velocity += accumulatedForce * Time.deltaTime * invMass;
        accumulatedForce = Vector3.zero;
    }
}
