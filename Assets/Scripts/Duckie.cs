using UnityEngine;

public class Duckie : MonoBehaviour
{
    public int points = 5;
    protected virtual void Collect()
    {
        FindObjectOfType<GameManager>().DuckieGet(this);
        //GameManager.Instance.DuckieGet(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SirQuack")
        {
            Collect();
        }
    }
}