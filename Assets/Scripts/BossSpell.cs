using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Vector2 boxDimentions;
    [SerializeField] private Transform boxPosition;
    [SerializeField] private float lifeTime;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Strike()
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(boxPosition.position, boxDimentions, 0f);

        foreach (Collider2D collisions in objects)
        {
            if (collisions.CompareTag("Hero"))
            {
                collisions.GetComponent<HeroBattle>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxPosition.position, boxDimentions);
    }
}
