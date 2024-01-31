using UnityEngine;

public class CitrosRunAway : CitrosBehavior 
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public bool eaten {get; private set;}

    public override void Enable(float duration)
    {
        base.Enable(duration);
        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }
    public override void Disable()
    {
        base.Disable();
        this.body.enabled = true;
        this.eyes.enabled = true;
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

    private void Eaten()
    {
        this.eaten = true;
        Vector3 position = this.citros.idle.inside.position;
        position.z = this.citros.transform.position.z;
        this.citros.transform.position = position;
        this.citros.idle.Enable(this.duration);
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void OnEnable()
    {
        this.citros.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }
    private void OnDisable()
    {
        this.citros.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("SirQuack"))     
        {   
            if (this.enabled)
            {
            Eaten();
            }
        }
    }
        private void OnTriggerEnter2d(Collider2D other)
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
