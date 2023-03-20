using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rightOrLeft; //-1 is left, 1 is right and 0 is default
    public string associatedCharacterName;
    private GameObject associatedCharacter;

    public void OnClick() //when we click on a Ladybug we say it to hostage which was escorted and then destroy the Ladybug
    {
        if (associatedCharacterName != null)
        {
            associatedCharacter = GameObject.Find(associatedCharacterName + "(Clone)");
            if (associatedCharacter != null) //if the character die, he is not on the scene anymore
            {
                if (rightOrLeft == -1) associatedCharacter.GetComponent<Character>().LeftEnemyDied();
                if (rightOrLeft == 1) associatedCharacter.GetComponent<Character>().RightEnemyDied();
            }
        }
        Shoot.score +=1;
        Destroy(gameObject);
    }
}
