using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class SpriteControlENV : MonoBehaviour
{
    private const int IsometricYOffset = 100;
    Renderer renderer;
    public int offset;
	// Use this for initialization
	void Start ()
    {
        renderer = GetComponent<Renderer>();
        renderer.sortingOrder = -(int)(transform.position.y * IsometricYOffset) + offset;
	}
}
