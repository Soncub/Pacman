using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SirQuack sirquack;
    public Citros[] citros;
    public Transform duckies;
    public int citroMultiplier { get; private set; } = 1;
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
        foreach (Transform duckie in this.duckies)
        {
            duckie.gameObject.SetActive(true);
        }

        // calls citros and sir quack
        ResetState();
    }

    // function that resets the citros and sir Quack's position without changing the pellets
    private void ResetState() 
    {
        // put sir quack back onto the map / active
        this.sirquack.ResetState();

        ResetCitroMultiplier();
        // put the citros back onto the map / set active
        for (int i = 0; i < this.citros.Length; i++)
        {
            this.citros[i].ResetState();
        }
    }

    private void GameOver()
    {
        // deactivates sir Quack
        this.sirquack.gameObject.SetActive(false);

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
        ScorePrepare(this.score + (citros.points * this.citroMultiplier));
        this.citroMultiplier++;
    }

    // when Citro kills Sir Quack, deactivate Sir Quack and take away a life
    public void SirQuackDies()
    {
        this.sirquack.gameObject.SetActive(false);
        LivesPrepare(this.lives -= 1);

        // check lives, if lives, reset round AFTER 3 seconds, otherwise end game
        if (this.lives > 0) 
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else 
        {
            GameOver();
        }
    }
    public void DuckieGet(Duckie duckie)
    {
        duckie.gameObject.SetActive(false);
        ScorePrepare(this.score + duckie.points);
        if(!HasRemainingDuckies())
        {
            this.sirquack.gameObject.SetActive(false);
            Invoke(nameof(MakeRound),3.0f);
        }
    }
    public void SwordGet(Sword duckie)
    {
        for (int i = 0; i < this.citros.Length; i++)
        {
            this.citros[i].runaway.Enable(duckie.duration);
        }
        DuckieGet(duckie);
        CancelInvoke();
        Invoke(nameof(ResetCitroMultiplier), duckie.duration);
    }
    private bool HasRemainingDuckies()
    {
        foreach (Transform duckie in this.duckies)
        {
            if (duckies.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
     private void ResetCitroMultiplier()
    {
        this.citroMultiplier = 1;
    }
}
