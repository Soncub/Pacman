using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SirQuack sirQuack;
    public Citros citros;
    public Transform duckies;
    public int score {get;private set;}
    public int lives {get;private set;}

    private void Start()
    {
        CreateGame();
    }
    private void CreateGame()
    {
        ScorePrepare(0);
        LivesPrepare(3);
        MakeRound();
    }
  
    private void ScorePrepare(int score)
    {
        this.score = score;
    }
    private void LivesPrepare(int lives)
    {
        this.lives = lives;
    }
    private void Update()
    {
        
    }
}
