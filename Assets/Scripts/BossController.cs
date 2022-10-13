using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private Animator mAnimator;
    public Rigidbody2D mRb;
    public Transform hero;

    private bool lookingRight = false;

    //Barra de vida
    [Header("Life")]
    [SerializeField] private float life;
    //[SerializeField] private Lifebar lifebar;
    [Header("Attack")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadio;
    [SerializeField] private float attackDamage;

    private float mBossHealth;
    private Slider mBossSlider;


    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRb = GetComponent<Rigidbody2D>();
        //lifebar.InitializeLifebar(life);
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>();

        mBossSlider = GameObject.Find(
            "BossCanvas"
        ).GetComponent<Transform>().Find(
            "HealthBar"
        ).Find(
            "Border"
        ).GetComponent<Slider>();        

        mBossSlider.maxValue = life;
        mBossHealth = life;
    }

    private void Update() {
        float distanceHero = Vector2.Distance(transform.position, hero.position);
        mAnimator.SetFloat("distanceHero", distanceHero); 
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
        mBossHealth -= damage;
        //lifebar.ChangeActualLife(life);
        mBossSlider.value = mBossHealth;
        if (mBossHealth <= 0)
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

    public void Attack(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackController.position, attackRadio);

        foreach (Collider2D collision in objects)
        {
            if(collision.CompareTag("Hero")){
                collision.GetComponent<HeroBattle>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, attackRadio);
    }

}
