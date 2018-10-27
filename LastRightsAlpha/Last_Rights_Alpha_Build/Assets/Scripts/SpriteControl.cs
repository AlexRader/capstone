using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class SpriteControl : MonoBehaviour
{
    private const int IsometricYOffset = 100;
    SpriteRenderer mRenderer;
    public SpriteRenderer reticle, player;
    public Canvas canvasRenderer;
    int order;

    Rigidbody2D myRB;

    // Use this for initialization
    void Start ()
    {
        myRB = GetComponent<Rigidbody2D>();
        mRenderer = GetComponentInChildren<SpriteRenderer>();
        //canvasRenderer = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update ()
    {
        order = -(int)(transform.position.y * IsometricYOffset);
        mRenderer.sortingOrder = player.sortingOrder = order;
        reticle.sortingOrder = order + 1;
        canvasRenderer.sortingOrder = order;
        SpriteDirection();
    }

    void SpriteDirection()
    {
        if (myRB.velocity.x > 0)
            player.flipX = true;
        else if (myRB.velocity.x < 0)
            player.flipX = false;
    }
}
