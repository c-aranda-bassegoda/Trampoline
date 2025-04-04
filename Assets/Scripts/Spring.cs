using UnityEngine;

public class Spring
{
    float k = 1; // spring constant
    float restLength = 1;
    public Vertex v1;
    public Vertex v2;

    public Spring(float k, float restLength, Vertex v1, Vertex v2)
    {
        this.k = k;
        this.restLength = restLength;
        this.v1 = v1;
        this.v2 = v2;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
