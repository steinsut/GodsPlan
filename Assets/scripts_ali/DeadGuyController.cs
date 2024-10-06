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

    [SerializeField]
    SpriteRenderer head, hair;

    private float speed = 1f;

    [SerializeField]
    private HumanData humandata;

    int lastPosition;
    float currentTime = 0;

    bool dead = false;
    [SerializeField]
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
        if (canMove)
        {
            transform.position = positions[0];

        }
        lastPosition = 0;
        loadDetails();
    }

    void loadDetails()
    {
        humandata = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().GetLevelHuman();
        hair.color = humandata.hairColor;
        head.color = humandata.skinColor;
        head.sprite = Globals.Instance.MaleSprites.child.GhostHeads[0];

        switch (humandata.sex)
        {
            case Sex.MALE:
                if (humandata.age == AgeGroup.CHILD)
                {
                    hair.sprite = Globals.Instance.MaleSprites.child.GhostHairs[humandata.headId];
                } else if (humandata.age == AgeGroup.ADULT)
                {
                    hair.sprite = Globals.Instance.MaleSprites.adult.GhostHairs[humandata.headId];

                } else
                {
                    hair.sprite = Globals.Instance.MaleSprites.geezer.GhostHairs[humandata.headId];
                }
                break;
            case Sex.FEMALE:
                if (humandata.age == AgeGroup.CHILD)
                {
                    hair.sprite = Globals.Instance.FemaleSprites.child.GhostHairs[humandata.headId];
                }
                else if (humandata.age == AgeGroup.ADULT)
                {
                    hair.sprite = Globals.Instance.FemaleSprites.adult.GhostHairs[humandata.headId];

                }
                else
                {
                    hair.sprite = Globals.Instance.FemaleSprites.geezer.GhostHairs[humandata.headId];
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && canMove)
        {
            currentTime += Time.deltaTime;
            if (currentTime  > timeStamps[lastPosition] )
            {
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
