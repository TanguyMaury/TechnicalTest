using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    public GameObject deadCharacter;
    bool hasLeftEnemy = true;
    bool hasRightEnemy = true;
    public static float posXMiniCharacter = 8f; //it is the X position of the next miniCharacter on the scene
    bool isFree = false;

    private void Update() 
    {
        if ((!(hasLeftEnemy || hasRightEnemy)) && !(isFree)) freeHostage(); //if both enemy which escorted the characters died, character is free
    }

    public void OnClick() //if we shot the character, he dies and a miniDeadCharacter spawn on the top right corner
    {
        Destroy(gameObject);
        Shoot.characterDisable +=1;
        PlaceMiniCharacter(true);
        Shoot.score -=1;//we lose 1 point if we kill a character
    }

    void PlaceMiniCharacter (bool isDead) //place mini character on the top right corner, if isDead = true, we put a mini dead character else a mini character alive
    {
        if (isDead) 
        {
            GameObject myObject = Instantiate(deadCharacter, new Vector3(posXMiniCharacter -0.1f, 3.6f, 0f),  Quaternion.Euler(0f, 0f, 90f));
            posXMiniCharacter -= 0.4f;
        }
        else // mini character alive are just a movement of character on the top right corner and then we reduce scale
        {
            transform.position = new Vector3(posXMiniCharacter, 3.7f, 0f);
            transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
            posXMiniCharacter -= 0.4f;
        }
    }

    public void RightEnemyDied()
    {
        hasRightEnemy = false;
    }

    public void LeftEnemyDied()
    {
        hasLeftEnemy = false;
    }

    void freeHostage ()
    {
        isFree = true;
        //Debug.Log("FREE");
        //Destroy(gameObject);
        Shoot.characterDisable +=1;
        Shoot.score +=5; //we got 5 point if we liberate a character
        gameObject.GetComponent<Movement>().enabled=false; //we stop movement of character when he becomes free (paradox ??)
        PlaceMiniCharacter(false); //We place it on the top right corner
    }
}
