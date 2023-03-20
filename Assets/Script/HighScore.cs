using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public static int highScore = 0;
    // Start is called before the first frame update
    
    public void NewHighScore () //print it if there is a new highScore
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "New high score !!!" + highScore.ToString();
    }

    public void DisplayHighScore ()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "High Score : " + highScore.ToString();
    }
}
