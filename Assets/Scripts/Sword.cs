using UnityEngine;

public class Sword : Duckie
{
    public float duration = 8.0f;
    protected override void Collect()
    {
        FindObjectOfType<GameManager>().SwordGet(this);
    }
}