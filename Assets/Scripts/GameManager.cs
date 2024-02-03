using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    public SirQuack sirquack;
    public Citros[] citros;
    public Transform duckies;
    public SchoolSupply supply;
    public int citroMultiplier { get; private set; } = 1;
    public int score {get;private set; }
    private float lives = 3;
    int amount;
    public static GameManager Instance {get; private set;}
    public TextMeshProUGUI scoreText;
   
    [SerializeField] private Sprite [] livesSprite;
    [SerializeField] private Image livesImage;
    int lives2;

    private void Start()
    {
        CreateGame();
        supply.gameObject.SetActive(false);
        
    }

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
        scoreText.text = "Score: " + score.ToString();

         if (score == 500 || score == 1500) {
            
            Invoke(nameof(SchoolSupplyAppear), 0f);

        }
        if (score == 10000){
            LivesPrepare(lives+1);
        }
    }
    private void LivesPrepare(float lives)
    {
        // set lives in game
        this.lives = lives;
        lives2 = (int)lives;
        //livesText.text = "Lives: " + lives.ToString();
        livesImage.sprite = livesSprite[lives2];
        
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
        LivesPrepare(lives - 0.5f);

        // deactivates citros when sir quack dies
        for (int i = 0; i < this.citros.Length; i++)
        {
            this.citros[i].gameObject.SetActive(false);
        }
        

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
            // ------------------ Invoke(nameof(MakeRound),3.0f);
        } 
    }
    public void SwordGet(Sword duckie)
    {
        for (int i = 0; i < this.citros.Length; i++)
        {
            this.citros[i].runaway.Enable(duckie.duration);
        }
        DuckieGet(duckie);
        CancelInvoke(nameof(ResetCitroMultiplier));
        Invoke(nameof(ResetCitroMultiplier), duckie.duration);
    }

    public void SchoolSupplyGet(SchoolSupply supply){
        supply.gameObject.SetActive(false);
        ScorePrepare(this.score + supply.points);
    }

    private bool HasRemainingDuckies()
    {
        foreach (Transform duckie in this.duckies)
        {
            if (duckie.gameObject.activeInHierarchy)
            {
                //Debug.Log("TRUE");
                return true;
                
            }
        }
        //Debug.Log("I AM FALSE");
        return false;
        
    }

    private void ResetCitroMultiplier()
    {
        this.citroMultiplier = 1;
    }

    void SchoolSupplyAppear() {
        supply.gameObject.SetActive(true);
    }
}
