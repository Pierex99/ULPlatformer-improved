using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public HeroController hero;
    private Transform mCanvasVictory;
    private Transform mCanvasLose;
    private Transform mCanvasLifebar;

    void Awake()
    {
        Instance = this;
        mCanvasVictory = GameObject.Find("BossCanvas").GetComponent<Transform>().Find("TextVictory");
        mCanvasVictory.gameObject.SetActive(false);
        mCanvasLose = GameObject.Find("BossCanvas").GetComponent<Transform>().Find("TextGameOver");
        mCanvasLose.gameObject.SetActive(false);
    }
}
