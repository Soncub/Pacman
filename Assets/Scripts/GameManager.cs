using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SirQuack sirQuack;
    public Citros[] citros;
    public Transform duckies;
    public int score {get;private set; }
    public int lives {get;private set; }

    private void Start()
    {
        CreateGame();
    }

    private void Update()
    {
        // if no lives + player presses any key, create new game !
        if (this.lives <= 0)
        {
            if (Input.anyKeyDown)
            {
                CreateGame();
            }
        }
    }
    private void CreateGame()
    {
        ScorePrepare(0);
        LivesPrepare(3);
        MakeRound();
    }

    private void MakeRound()
    {

        // put the duckies back onto the map in the new round
        foreach (Transform duckies in this.duckies)
        {
            duckies.gameObject.SetActive(true);
        }

        // calls citros and sir quack
        DeathRestart();
    }

    // function that resets the citros and sir Quack's position without changing the pellets
    private void DeathRestart() 
    {
        // put sir quack back onto the map / active
        this.sirQuack.gameObject.SetActive(true);

        // put the citros back onto the map / set active
        for (int i = 0; i < this.citros.Length; i++)
        {
            this.citros[i].gameObject.SetActive(true);
        }
    }

    private void GameOver()
    {
        // deactivates sir Quack
        this.sirQuack.gameObject.SetActive(false);

        // deactivates citros
        for (int i = 0; i < this.citros.Length; i++)
        {
            this.citros[i].gameObject.SetActive(false);
        }
    }
  
    private void ScorePrepare(int score)
    {
        // set score
        this.score = score;
    }
    private void LivesPrepare(int lives)
    {
        // set lives in game
        this.lives = lives;
    }

    // when Sir Quack kills Citro with sword, increment score
    public void CitroDies(Citros citros)
    {
        ScorePrepare(this.score + citros.points);
    }

    // when Citro kills Sir Quack, deactivate Sir Quack and take away a life
    public void SirQuackDies()
    {
        this.sirQuack.gameObject.SetActive(false);
        LivesPrepare(this.lives -= 1);

        // check lives, if lives, reset round AFTER 3 seconds, otherwise end game
        if (this.lives > 0) 
        {
            Invoke(nameof(DeathRestart), 3.0f);
        }
        else 
        {
            GameOver();
        }
    }
}
