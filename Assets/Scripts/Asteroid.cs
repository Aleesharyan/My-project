using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 0.7f;
    public float minSize = 0.5f;
    public float maxSize = 0.9f;

    public float speed = 50.0f;

    public float maxLifetime = 30.0f;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //this is going to pick a random sprite
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        //this will give it a random rotation
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        //and this will randomise its scale
        //Vector3.one is a simplier way of writing 'new Vector3(this.size, this.size, this.size)
        this.transform.localScale = Vector3.one * this.size;

        //now to set the mass
        _rigidbody.mass = this.size;

    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    
}
