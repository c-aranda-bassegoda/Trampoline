using Unity.VisualScripting;
using UnityEngine;

public class Spring
{
    public float restLength = 1;
    public float stiffness = 1;
    public Vertex v1;
    public Vertex v2;

    public Spring(float restLength, float stiffness, Vertex v1, Vertex v2)
    {
        this.restLength = restLength;
        this.stiffness = stiffness;
        this.v1 = v1;
        this.v2 = v2;
    }
}
