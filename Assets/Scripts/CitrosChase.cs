using UnityEngine;

public class CitrosChase : CitrosBehavior 
{
    public void OnDisable()
    {
        this.citros.scatter.Enable();
    }
    private void OnTriggerEnter2d(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && this.enabled && !this.citros.runaway.enabled)
        {
           Vector2 direction = Vector2.zero;
           float minDistance = float.MaxValue;

           foreach(Vector2 availableDirection in node.availableDirections)
           {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.citros.target.position - newPosition).sqrMagnitude;
                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
           }
        this.citros.movement.SetDirection(direction);
        }
    }
}
