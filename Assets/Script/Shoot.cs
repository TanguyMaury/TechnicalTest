using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    public GameObject ammoDisplay;
    public static int ammo = 20; //count of ammo
    public static int characterDisable = 0; //count the number of free and saved characters 
    public GameObject scoreDisplay;
    public static int score = 0; //count score
    int scoreDiplayed = 0; 
    bool gameEnded = false;
    AudioSource audioSource;
    public AudioClip shotSound;

    void Start() //Initialization of variables
    {
        ammoDisplay.GetComponent<TextMeshProUGUI>().text = "Ammo : " + ammo.ToString();
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score.ToString();
        characterDisable = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (score != scoreDiplayed) 
        {
            scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score.ToString();
            scoreDiplayed = score;
        }
        if (Input.GetMouseButtonDown(0) && !gameEnded) //when we click on screen it lauch a sound and it use an ammo
        {
            ammo-=1;
            audioSource.PlayOneShot(shotSound);
            ammoDisplay.GetComponent<TextMeshProUGUI>().text = "Ammo : " + ammo.ToString();
            scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score.ToString();
            if (ammo == 0) //if we use all of our ammo, it's the end of the game
            {
                //Debug.Log("End game");
                EndGame("Out of Ammo");
                
            }
        }
        if (characterDisable == 5 && !gameEnded){ //if the 5 characters have been saved or killed it's the end of the game
            //Debug.Log("End Game");
            EndGame("All characters has been killed or saved");
        }
    }

    //End Game Management
    public GameObject endGame;
    public GameObject canvasEndGame;
    public GameObject canvasGame;
    public GameObject highScoreDisplay;


    void EndGame(string causeOfEnd) //we display high score, cause of end of the game, and Final Score
    {
        if (score > HighScore.highScore) 
        {
            HighScore.highScore = score;
            highScoreDisplay.GetComponent<HighScore>().NewHighScore();
        }
        else highScoreDisplay.GetComponent<HighScore>().DisplayHighScore();
        gameEnded =true;
        endGame.GetComponent<TextMeshProUGUI>().text = causeOfEnd +  "\nFinal Score : " + score.ToString();
        canvasEndGame.SetActive(true);
        canvasGame.SetActive(false);
    }

    public void Restart() //Restart a new game, and reset static variables
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canvasEndGame.SetActive(false);
        canvasGame.SetActive(true);
        ammo = 20;
        score = 0;
        Character.posXMiniCharacter = 8f;
        gameEnded = false;
        highScoreDisplay.GetComponent<HighScore>().DisplayHighScore();
    }
}
