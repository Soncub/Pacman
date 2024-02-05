using UnityEngine;

public class CitrosRunAway : CitrosBehavior 
{

    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public SpriteRenderer justEyes;
    public AudioClip consumed;
    public bool eaten; //{get; private set;}

    public override void Enable(float duration)
    {
        base.Enable(duration);

        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;
        justEyes.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        eyes.enabled = true;
        justEyes.enabled = false;
        blue.enabled = false;
        white.enabled = false;
    }
    private void Flash()
    {
        // ADD ANIMATED SPRITE
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        eaten = true;
        Vector3 position = citros.idle.inside.position;
        //position.z = this.citros.transform.position.z;
        citros.transform.position = position;
        citros.idle.Enable(this.duration);

        //this.eyes.enabled = true;
        justEyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    // makes ghosts slow 
    private void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        citros.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }  
    private void OnDisable()
    {
        citros.movement.speedMultiplier = 1.0f;
        eaten = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SirQuack")     
        {   
            if (enabled)
            {
            AudioSource.PlayClipAtPoint(consumed, transform.position);
            Eaten();
            }
        }
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && enabled)
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
        citros.movement.SetDirection(direction);
        }
    }
}
