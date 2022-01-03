using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]private Text  texScore;
    [SerializeField] private Text textLife;
    // public static int Score;
    private int scoreInstanced;
    private int lifeInstanced =3 ;

    private AnimationAndoMovementController playerScript;
    public enum typeHeart { heart, largeHeart }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            scoreInstanced = 0;
            lifeInstanced = 3;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<AnimationAndoMovementController>();
        playerScript.onDeath += GameOver;
       
    }
    private void GameOver()
    {
     
        Debug.Log("EL JUEGO TERMINO");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void AddScore()
    {
        instance.scoreInstanced += 1;
        texScore.text = "SCORE:" + " " + scoreInstanced;
        Debug.Log(scoreInstanced);
    }
    public static int getScore()
    {
        return instance.scoreInstanced;
    }
    public void AddLife()
    {
        instance.lifeInstanced += 1;
        textLife.text = "LIFE:" + " " + lifeInstanced;
    }
    public static int getLife()
    {

        return instance.lifeInstanced;
    }
    public void OnDeathHandler (int lifesLeft, bool isGameover)
    {
       
    }
}
