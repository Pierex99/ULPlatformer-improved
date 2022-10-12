using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBattle : MonoBehaviour
{
    [SerializeField] private float life;

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
