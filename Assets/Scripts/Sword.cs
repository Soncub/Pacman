using UnityEngine;

public class Sword : Duckie
{
    public float duration = 8.0f;
    public AudioClip sword;
    protected override void Collect()
    {
        FindObjectOfType<GameManager>().SwordGet(this);
        AudioSource.PlayClipAtPoint(sword, transform.position);
    }
}