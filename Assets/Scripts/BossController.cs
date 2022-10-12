using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator mAnimator;
    public Rigidbody2D mRb;
    private Transform hero;

    private bool lookingRight = true;

    //Barra de vida
    [Header("Life")]
    [SerializeField] private float life;
    //[SerializeField] private Lifebar lifebar;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRb = GetComponent<Rigidbody2D>();
        //lifebar.InitializeLifebar(life);
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(25f);
        }
        
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        //lifebar.ChangeActualLife(life);

        if (life <= 0)
        {
            mAnimator.SetTrigger("Death");
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public void LookHero()
    {
        if((hero.position.x > transform.position.x && !lookingRight) || (hero.position.x < transform.position.x && lookingRight))
        {
            lookingRight = !lookingRight;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

}
