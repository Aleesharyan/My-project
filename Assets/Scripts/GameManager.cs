using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;
    public float respawnTime = 3.0f;

    public float respawnInvulnerabilityTime = 3.0f;

    public int lives = 3;

    public void PlayerDied()
    {
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
        //TODO
    }

}
