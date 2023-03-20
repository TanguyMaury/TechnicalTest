using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveGeneration : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject enemy;
    public Transform parentTransform; //Parent gameobject of entities generated
    int nbPath = 5;
    public static bool[] characterState = new bool[5]; //register if a character is on screen, saved or dead
    System.Random rnd;
    public float interval = 1.0f; // Interval in seconds between 2 waves
    int[] usedPath = new int[5]; //Say if a path is used and for how many time
    
    void Start()
    {
        
        rnd = new System.Random();
        for (int i = 0; i<5; i++)
        {
            characterState[i] = false;
            usedPath[i] = 0;
        }
        InvokeRepeating("GenerateWave", 0.0f, interval); //Invoke the function GenerateWave each second
    }

    

    void GenerateWave()
    {
        int nbGroup = rnd.Next(1,nbPath+1);
        GenerateGroup(true);  //generate the group with hostage
        for (int i = 1; i<nbGroup; i++) GenerateGroup(false);  //generate other group
        for (int i = 0; i<5; i++)
        {
            if (usedPath[i]>0) usedPath[i]-=1;
        }
    }

    void GenerateGroup(bool hasHostage)
    {
        int characterRandom = getCharacter();
        int positionRandom = rnd.Next(0,nbPath); 
        //Debug.Log(characterRandom);
        //Debug.Log(positionRandom);
        if (usedPath[positionRandom] > 0) return;  // Avoid superposition of entities
         
        if (hasHostage && (characterRandom != -1)) //Apparition wave with an Hostage and 2 ladybugs, if characterRandom = -1 there in no character available so we put a wave full of Ladybug instead
        {
            EnemyApparition(characterRandom, positionRandom, -1, 1);
            CharacterApparition(characterRandom, positionRandom, -2);
            EnemyApparition(characterRandom, positionRandom, -3, -1);
            usedPath[positionRandom] = 3;
        }
        else //Apparition of a ladybug without Character
        {
            EnemyApparition(-1, positionRandom, -1, 0);
            usedPath[positionRandom] = 1;
        }
    }

    int getCharacter()  //return a random available character
    {
        int numberCharacterAvailable = 0;
        for (int i = 0; i < 5; i++)
        {
            if (characterState[i] == false) numberCharacterAvailable +=1;
        }
        if (numberCharacterAvailable==0) return -1;
        int random = rnd.Next(1, numberCharacterAvailable +1);
        int count =  0;
        for (int i = 0; i < 5; i++)
        {
            if (characterState[i] == false)
            {
                count+=1;
                if (count == random )
                {
                    return i;
                }
            }
        }
        return -1;
    }


    void EnemyApparition(int elementNumber, int pathNumber, int posX, int rightOrLeft)  //apparition of a ladybug with the number of the escorted character (-1 is default) and position of apparition
    {
        float posY = (float)pathNumber * (float)0.8;
        GameObject myObject = Instantiate(enemy, new Vector3((float)posX, posY, 0f), Quaternion.identity); //Creation of gameobject
        myObject.AddComponent<Movement>();
        myObject.GetComponent<Movement>().path = pathNumber + 1; //Setting up some public var
        
        myObject.GetComponent<Enemy>().rightOrLeft = rightOrLeft; //-1 is left, 1 is right and 0 is default
        if (elementNumber!=-1) myObject.GetComponent<Enemy>().associatedCharacterName = characters[elementNumber].name; //Name of escorted character, (null is default)
        else myObject.GetComponent<Enemy>().associatedCharacterName = null;
        
        myObject.GetComponent<SpriteRenderer>().enabled = false; //entities spawn invisible a bit before the path and appear on first tile of the Path
        myObject.GetComponent<Renderer>().sortingOrder = 11 -(pathNumber * 2); //Need to have SortingOrder higher than his own path and lower than path behind it (to stay behind elevated tile)
        myObject.transform.parent = parentTransform;
    }

    void CharacterApparition(int elementNumber, int pathNumber, int posX) //As above
    {
        characterState[elementNumber] = true;
        float posY = (float)pathNumber * (float)0.8;
        GameObject myObject = Instantiate(characters[elementNumber], new Vector3((float)posX, posY, 0f), Quaternion.identity);
        myObject.AddComponent<Movement>();
        myObject.GetComponent<Movement>().charNumber = elementNumber;
        myObject.GetComponent<Movement>().path = pathNumber + 1;
        myObject.GetComponent<Renderer>().sortingOrder = 11 -(pathNumber * 2);
        myObject.transform.parent = parentTransform;
        myObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
