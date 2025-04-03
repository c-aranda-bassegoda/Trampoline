using UnityEngine;

public class Vertex : MonoBehaviour
{
    public int mass;
    public Vector3 position;
    public bool _isFixed;
    public bool isFixed { get => _isFixed; set { _isFixed = value; } }

    Vector3 _velocity = Vector3.zero;
    public Vector3 velocity { get => _velocity; set { _velocity = value; } }

    public Vertex(int mass, Vector3 position)
    {
        this.mass = mass;
        this.position = position;
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
