using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 0.5f;
    public float minSize = 0.1f;
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

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check for collision with a bullet
        if (collision.gameObject.tag == "Bullet")
        {
            //asteroid splits into 2 if its big enough to
            if ((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        //setting new trajectory
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }

    
}
