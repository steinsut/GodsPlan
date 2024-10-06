using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeadGuyController : MonoBehaviour
{
    [SerializeField]
    List<Vector3> positions = new List<Vector3>();
    [SerializeField]
    List<float> timeStamps = new List<float>();

    private float speed = 1f;

    private HumanData humandata;

    int lastPosition;
    float currentTime = 0;

    bool dead = false;
    bool canMove = true;

    public bool isDead() {  return dead; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("deathReason"))
        {
            die();
        }
    }

    private void die()
    {
        dead = true;
        GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().ReturnFromMinigame(levelParent, false);
    }


    [SerializeField]
    GameObject levelParent;

    public void levelWonStopMoving()
    {
        canMove = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = 0;
        transform.position = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && canMove)
        {
            currentTime += Time.deltaTime;
            if (currentTime  > timeStamps[lastPosition] )
            {
                Console.Write("moveing");
                Vector3 goal = positions[lastPosition + 1];
                transform.Translate((goal - transform.position).normalized * speed * Time.deltaTime * Mathf.Clamp(Vector3.Distance(transform.position, goal), 0.3f, float.PositiveInfinity));
            }

            if (Vector3.Distance(positions[lastPosition + 1], transform.position) < 0.1f)
            {
                lastPosition += 1;
            }
        }
    }
}
