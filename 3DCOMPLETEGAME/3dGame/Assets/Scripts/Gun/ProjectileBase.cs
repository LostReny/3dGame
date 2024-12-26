using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float timeToDestroy = 2f;

    public int damageAmount = 1;
    public float speed = 50f;

    [Header("Tags")]
    public List<string> tagsToHit;

    private void Awake() {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) 
    {

        foreach(var t in tagsToHit)
        {
            if(collision.transform.tag == t)
            {
                var damagable = collision.transform.GetComponent<IDamagable>();

                if (damagable != null) 
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damagable.Damage(damageAmount, dir);

                }

                break;
            }

        }

        Destroy(gameObject);
    }
}
