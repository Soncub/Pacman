using UnityEngine;
public class CitrosScatter : CitrosBehavior 
{
    public void OnDisable()
    {
        this.citros.chase.Enable();
    }
    private void OnTriggerEnter2d(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && this.enabled && !this.citros.runaway.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index] == -this.citros.movement.direction && node.availableDirections.Count > 1)
            {
                index++;
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            this.citros.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
