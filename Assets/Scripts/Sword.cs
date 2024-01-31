using UnityEngine;

public class Sword : Duckie
{
    public float duration = 8.0f;
    protected override void CitroSlay()
    {
        FindObjectOfType<GameManager>().SwordGet(this);
    }
}