using System.Collections.Generic;
using UnityEngine;

public class movingPlatformScript : MonoBehaviour
{

    public bool move = true;

    [SerializeField]
    Vector3 pointA, pointB;

    float pointDistance;

    [SerializeField]
    private float speed = 1f;
    private int direction = 1;

    private float eRange = 0.1f;

    void switchDirections() {
        direction = -1 * direction;
    }

    float minimumDistance()
    {
        return Mathf.Min(Vector3.Distance(transform.position, pointA), Vector3.Distance(transform.position, pointB));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointDistance = Vector3.Distance(pointA, pointB);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Vector3 goal;
            if (direction == 1) {
                goal = pointA;
            } else
            {
                goal = pointB;
            }
            if (Vector3.Distance(transform.position, goal) < eRange)
            {
                switchDirections();
            }

            transform.Translate((goal - transform.position).normalized * speed * Time.deltaTime * Mathf.Clamp(minimumDistance(), 0.3f, float.PositiveInfinity));
        }
        //transform.Translate(transform.position - Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime * Mathf.Clamp(Vector3.Distance(transform.position, goal), 0.3f, float.PositiveInfinity)));
        //transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime * Mathf.Clamp(Vector3.Distance(transform.position, goal), 0.3f, float.PositiveInfinity));
    }
}
