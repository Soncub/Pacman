using UnityEngine;

public class Duckie : MonoBehaviour
{
    public int points = 10;
    protected virtual void CitroSlay()
    {
        FindObjectOfType<GameManager>().DuckieGet(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SirQuack"))
        {
            CitroSlay();
        }
    }
}