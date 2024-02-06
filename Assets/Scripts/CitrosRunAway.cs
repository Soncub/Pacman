using UnityEngine;

public class CitrosRunAway : CitrosBehavior 
{

    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public SpriteRenderer justEyes;
    public AudioClip consumed;
    
    public bool eaten {get; private set;}

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;
        this.justEyes.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        this.eyes.enabled = true;
        this.justEyes.enabled = false;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Eaten()
    {
        this.eaten = true;
        Vector3 position = this.citros.idle.inside.position;
        position.z = this.citros.transform.position.z;
        this.citros.transform.position = position;
        this.citros.idle.Enable(this.duration);

        this.eyes.enabled = false;
        this.justEyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Flash()
    {
        // ADD ANIMATED SPRITE
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    // makes ghosts slow 
    private void OnEnable()
    {
        this.blue.GetComponent<AnimatedSprite>().Restart(); // problem here
        this.citros.movement.speedMultiplier = 0.5f; //problem here
        this.eaten = false;
    }  
    private void OnDisable()
    {
        this.citros.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SirQuack")     
        {   
            if (this.enabled)
            {
            AudioSource.PlayClipAtPoint(consumed, transform.position);
            Eaten();
            }
        }
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && this.enabled)
        {
           Vector2 direction = Vector2.zero;
           float maxDistance = float.MinValue;

           foreach(Vector2 availableDirection in node.availableDirections)
           {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.citros.target.position - newPosition).sqrMagnitude;
                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
           }
        this.citros.movement.SetDirection(direction);
        }
    }
}
