using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolSupply : MonoBehaviour
{
    public int points = 100; //???
    public AudioClip schoolSupply;
    protected virtual void Collect()
    {
        FindObjectOfType<GameManager>().SchoolSupplyGet(this);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SirQuack")
        {
            Collect();
            AudioSource.PlayClipAtPoint(schoolSupply, transform.position);
        }
    }
}
