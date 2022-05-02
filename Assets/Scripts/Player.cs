using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    private Rigidbody2D _rigidbody;

    private bool _thrusting;

    private float _turnDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //checking input, if player is pressing the buttons.
        //If the player is pressing W they will thrust foward, if they press A they will go left
        //and if they press D they will go right

        _thrusting = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
    }

    private void FixedUpdate()
    {
        //after that we then apply physics

        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            //rotating object via physics engine
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position,
                                    this.transform.rotation);

        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            //stop movement
            _rigidbody.velocity = Vector3.zero;
            //stop rotation
            _rigidbody.angularVelocity = 0.0f;

            //turn off game object entirely
            this.gameObject.SetActive(false);

            //need to reference game manager
            //this function isnt the best to use cause it take some time, however
            //in this case it won't be called so frequantly as it would if it was in Update for example,
            //so its not soooo bad
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }


}
