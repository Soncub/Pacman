using UnityEngine;

public class Citros : MonoBehaviour
{
    public Movement movement {get;private set;}
    public CitrosIdle idle {get;private set;}
    public CitrosScatter scatter {get;private set;}
    public CitrosChase chase {get;private set;}
    public CitrosRunAway runaway {get;private set;}
    public CitrosBehavior initialBehavior;
    public Transform target;

    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.idle = GetComponent<CitrosIdle>();
        this.scatter = GetComponent<CitrosScatter>();
        this.chase = GetComponent<CitrosChase>();
        this.runaway = GetComponent<CitrosRunAway>();
        //gameObject.AddComponent<CircleCollider2D>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.runaway.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        this.idle.Disable();
        if(this.idle != this.initialBehavior)
        {
            this.idle.Disable();
        }
        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SirQuack")     
        {   
            //Debug.Log("Touched Quack");
            if (this.runaway.enabled)
            {
            GameManager.Instance.CitroDies(this);
            }
            else
            {
                GameManager.Instance.SirQuackDies();
            }
        }
        else {
           // Debug.Log("Nothing is there");
        }
    }
}