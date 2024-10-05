using System;
using UnityEngine;

public class RemoteDoor : MonoBehaviour
{
    [SerializeField]
    ToggleController toggleController;

    public Sprite closed, open;

    private SpriteRenderer spriteRenderer;

    private BoxCollider2D collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (toggleController == null)
        {
            throw new NullReferenceException("Toggle controller not assined for a remote door!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleController.toggled)
        {
            if (spriteRenderer.sprite != open)
            {
                spriteRenderer.sprite = open;
                collider.enabled = false;
            }
        } else
        {
            if (spriteRenderer.sprite != closed)
            {
                spriteRenderer.sprite = closed;
                collider.enabled = true;
            }
        }
    }
}
