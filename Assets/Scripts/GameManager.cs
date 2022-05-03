using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;

    public ParticleSystem explosion;

    public float respawnTime = 3.0f;

    public float respawnInvulnerabilityTime = 3.0f;

    public int lives = 3;

    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        // TODO increase score
        //smaller asteroids give more points
        if (asteroid.size < 0.5f)
        {
            this.score += 100;
        }
        else if (asteroid.size > 0.5f)
        {
            this.score += 50;
        }


    }

    public void PlayerDied()
    {

        //play particle effect where the player is
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        //decrease lives
        this.lives--;

        //check how many lives left
        //if no lives left, gameover, if not game continues
        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }

        
    }

    private void Respawn()
    {
        //reset position to middle of screen
        this.player.transform.position = Vector3.zero;

        //changing layer of player so they dont instantly die if they respawn on an asteroid
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");

        //put player back on screen
        this.player.gameObject.SetActive(true);

        //have to turn collisions back on
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    //setting layer back to what it should be
    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        //reset lives
        this.lives = 3;

        //reset score
        this.score = 0;

        //respawn player
        Invoke(nameof(Respawn), this.respawnTime);
    }

}
