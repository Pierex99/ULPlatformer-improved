using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float raycastDistance;
    [SerializeField]
    private GameObject prefabBullet;
    [SerializeField]
    private float maxEnergy;
    

    private float mMovement = 0f;    
    private bool mIsJumpPressed = false;
    private bool mIsJumping = false;
    private Rigidbody2D mRb;
    private Transform mRaycastPoint;
    private CapsuleCollider2D mCollider;
    private Vector3 mRaycastPointCalculated;
    private Animator mAnimator;
    private Transform mBulletSpawnPoint;
    private bool doubleJump;
    private float mEnergy = 0f;
    private Slider mEnergySlider;



    private Slider mSlider;
    private Transform mCanvas;
    private float mHealth;
    private float maxHealth;

    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mRaycastPoint = transform.Find("RaycastPoint");
        mCollider = GetComponent<CapsuleCollider2D>();
        mAnimator = GetComponent<Animator>();
        mBulletSpawnPoint = transform.Find("BulletSpawnPoint");

        maxHealth = GetComponent<HeroBattle>().life;

        //barra de vida
        mSlider = transform.Find(
            "Canvas"
        ).Find(
            "HealthBar"
        ).Find(
            "Border"
        ).GetComponent<Slider>();
        
        mEnergySlider = transform.Find(
            "Canvas"
        ).Find(
            "EnergyBar"
        ).Find(
            "Border"
        ).GetComponent<Slider>();

        mCanvas = transform.Find("Canvas");
        mHealth = maxHealth;
        mSlider.maxValue = maxHealth;
        mEnergySlider.maxValue = 100f;



    }
    

    void FixedUpdate()
    {
        
        //transform.position += mMovement * speed * Time.fixedDeltaTime * Vector3.right;
        mRb.velocity = new Vector2(
            mMovement * speed,
            mRb.velocity.y
        );

        if (mRb.velocity.x != 0f)
        {
            transform.localScale = new Vector3(
                mRb.velocity.x < 0f ? -1f : 1f,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        IsJumping();

        if (mIsJumpPressed)
        {
            // Comenzar salto
            Jump();
        }

        // Informativo
        Debug.DrawRay(
            mRaycastPointCalculated,
            Vector2.down * raycastDistance,
            mIsJumping == true ? Color.green : Color.white
        );

        //actualizar vida
        mHealth = GetComponent<HeroBattle>().life;
        mSlider.value = mHealth;
    }

    void Update()
    {
        mMovement = Input.GetAxis("Horizontal");

        if (mMovement > 0f || mMovement < 0f )
        {
            mAnimator.SetBool("isMoving", true);
        }else
        {
            mAnimator.SetBool("isMoving", false);
        }

        //mIsJumpPressed = Input.GetKeyDown(KeyCode.Space);
        
        if (!doubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            mIsJumpPressed = true;
        }
        if(mIsJumping && Input.GetKeyDown(KeyCode.Space)){
            doubleJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Animacion de disparo
            mAnimator.SetTrigger("shoot");
            Fire();
        }

        mAnimator.SetBool("isJumping", mIsJumping);
        mAnimator.SetBool("isFalling", mRb.velocity.y < 0f);
    }

    private void Jump()
    {
        mRb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
        mIsJumping = true;
        mIsJumpPressed = false;
    }

    private void IsJumping()
    {
        mRaycastPointCalculated = new Vector3(
            mCollider.bounds.center.x,
            mCollider.bounds.center.y - mCollider.bounds.extents.y,
            transform.position.z
        );

        RaycastHit2D hit = Physics2D.Raycast(
            mRaycastPointCalculated,// Posicion origen
            Vector2.down,// Direccion
            raycastDistance// Distancia
        );
        if (hit)
        {
            // Hay una colision, esta en el suelo
            mIsJumping = false;
            doubleJump = false;
        }
    }

    private void Fire()
    {
        Instantiate(
            prefabBullet, 
            mBulletSpawnPoint.position, 
            Quaternion.identity
        );
    }

    public int GetPointDirection()
    {
        return (int)transform.localScale.x;
    }

    public void AddEnergy(){
        mEnergy += 25f;
        mEnergySlider.value = mEnergy;
        if(mEnergy >= 100){
            Debug.Log("Se activo el poder!");
            mEnergy = 0f;
            mEnergySlider.value = mEnergy;
        }
    }

}
