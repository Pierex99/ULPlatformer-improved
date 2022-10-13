using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBattle : MonoBehaviour
{
    [SerializeField] public float life;
    
    private Transform mCanvasLose;
    

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
            mCanvasLose = GameObject.Find("BossCanvas").GetComponent<Transform>().Find("TextGameOver");
            mCanvasLose.gameObject.SetActive(true);
        }
    }
}
