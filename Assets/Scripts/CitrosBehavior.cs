using UnityEngine;
//behavior used her is inherited to other Citro Behaviors
public abstract class CitrosBehavior : MonoBehaviour
{
    public Citros citros {get;private set;}
    public float duration;
    private void Awake()
    {
        this.citros = GetComponent<Citros>();
        this.enabled = false;
    }
    public void Enable()
    {
        Enable(this.duration);
    }
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }
    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}
