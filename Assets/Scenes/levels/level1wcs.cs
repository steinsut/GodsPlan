using System;
using UnityEngine;

public class level1wcs : MonoBehaviour
{
    [SerializeField]
    Transform deadGuy;

    [SerializeField]
    GameObject levelParent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("physbox"))
        {
            // the box fell, wonderful.
            if (collision.gameObject.transform.position.x >  deadGuy.position.x)
            {
                Debug.Log("Win yay wunderbar heil hitler");
                deadGuy.gameObject.SendMessage("levelWonStopMoving");
                letGo();
            }
        }
    }

    void letGo()
    {
        GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().ReturnFromMinigame(levelParent, deadGuy.GetComponent<DeadGuyController>().isDead());
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
