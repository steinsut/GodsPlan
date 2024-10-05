using UnityEngine;

public class KillerController : MonoBehaviour
{
    public bool selfPropelled = false;
    public float speed = 0;
    Vector2 direction = Vector2.left;

    Rigidbody2D rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selfPropelled)
        {
            rigidbody.linearVelocity = direction * speed;
        }
    }
}
