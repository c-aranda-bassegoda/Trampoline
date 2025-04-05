using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public int mass;
    public Vector3 position;
    bool _isFixed;
    public bool isFixed { get => _isFixed; set { _isFixed = value; } }

    Vector3 _velocity = Vector3.zero;
    public Vector3 velocity { get => _velocity; set { _velocity = value; } }

    Vector3 _accumulatedForce;
    public Vector3 accumulatedForce { get => _accumulatedForce; set { _accumulatedForce = value; } }

    List<Spring> _adjSprings;
    public List<Spring> adjSprings { get => _adjSprings; set { _adjSprings = value; } }

    public void addSpring(Spring spring) { _adjSprings.Add(spring); }

    public Vertex(int mass, Vector3 position)
    {
        this.mass = mass;
        this.position = position;
    }

    public void updatePosition()
    {
        position += velocity * Time.deltaTime;
    }    

    public void updateVelocity()
    {
        _velocity += accumulatedForce * Time.deltaTime;
    }
}
