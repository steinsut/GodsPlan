using System;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
    [SerializeField]
    private Sprite s_on, s_off;

    public bool enabled = false;

    private SpriteRenderer spriteRenderer;

    private bool playerContact = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (playerContact)
        {
            if (Input.GetButtonDown("Interract"))
            {
                enabled = !enabled;
            }
        }

        if (enabled)
        {
            if (spriteRenderer.sprite != s_on)
            {
                spriteRenderer.sprite = s_on;
            }
        } else
        {
            if (spriteRenderer.sprite != s_off) {
                spriteRenderer.sprite = s_off;
            }
        }
    }
}
