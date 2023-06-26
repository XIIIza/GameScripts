using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractebleObjects : MonoBehaviour
{
    [SerializeField] protected GameObject _visualCue;
    [SerializeField] protected Sprite FirstStateImage;
    [SerializeField] protected Sprite SecondStateImage;

    [SerializeField] protected bool IsInteracteble = false;
    protected bool InRange = false;
    protected SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = FirstStateImage;
        InRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            InRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            InRange = false;
        }
    }
}