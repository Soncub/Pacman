using System.Collections;
using UnityEngine;

public class CitrosIdle : CitrosBehavior 
{
    public Transform inside;
    public Transform outside;

    public void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
        StartCoroutine(ExitTransition());
        }
    }   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
           this.citros.movement.SetDirection(-this.citros.movement.direction); 
        }
    }

    private IEnumerator ExitTransition()
    {
        this.citros.movement.SetDirection(Vector2.up, true);
        this.citros.movement.rigidbody.isKinematic = true;
        this.citros.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            //Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            //newPosition.z = position.z;
            //this.citros.transform.position = newPosition;
            this.citros.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            //Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            //newPosition.z = position.z;
            //this.citros.transform.position = newPosition;
            this.citros.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.citros.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.citros.movement.rigidbody.isKinematic = false;
        this.citros.movement.enabled = true;
    }
}
