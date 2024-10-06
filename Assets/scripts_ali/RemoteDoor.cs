using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RemoteDoor : MonoBehaviour
{
    [SerializeField]
    ToggleController toggleController;

    public Sprite closed, open;

    private SpriteRenderer spriteRenderer;
    private ShadowCaster2D shadowCaster;

    private BoxCollider2D collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (toggleController == null)
        {
            throw new NullReferenceException("Toggle controller not assined for a remote door!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        shadowCaster = GetComponent<ShadowCaster2D>();
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
                shadowCaster.enabled = false;
            }
        } else
        {
            if (spriteRenderer.sprite != closed)
            {
                spriteRenderer.sprite = closed;
                collider.enabled = true;
                shadowCaster.enabled = true;
            }
        }
    }
}
