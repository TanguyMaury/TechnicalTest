using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public static float speed = 1f; // move speed
    private Tilemap [] tileMaps;
    private Tilemap tileMap;
    private Vector3 offset = new Vector3 (0.0f,0.3f,0f);
    private bool isAppear = false;
    public int charNumber = -1;
    public int path;

    void Start() {
        string tileMapName ="Path" + path.ToString();
        //Debug.Log(tileMapName);
        tileMaps = (Tilemap[])FindObjectsOfType(typeof(Tilemap));
        for(int i = 0; i < tileMaps.Length; i++)
        {
            if(tileMaps[i].name == tileMapName)
            {
                tileMap = tileMaps[i]; // associate the tileMap of the path where we generate entities
            }
        }
    }

    void LateUpdate()
    {
        
        Vector3Int tilePosition = tileMap.WorldToCell(transform.position - offset); //We get position of the character
        //Debug.Log(tilePosition);
        TileBase tile = tileMap.GetTile(tilePosition);
        float posY = 0f;
        if (tile != null){
            if (!isAppear) Appearance(); // Entities appear when they are on the path and disappear when they go out
            switch (tile.name)
            {
                case "Ramp East" : //If they take a ramp, entities needs to move on Y axis too
                    posY= -0.4f;
                    break;
                case "Ramp West" : 
                    posY= 0.4f;
                    break;
                default : 
                break;
            }
        }
        else
        {
            if (isAppear) Disappearance();
        }
        Vector3 movement = new Vector3 (1.0f, posY, 0f );
        transform.position += movement * speed * Time.deltaTime;
        offset += new Vector3 (0f,posY,0f)* speed * Time.deltaTime;
    }


    void Appearance()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        isAppear = true;
    }

    void Disappearance() //Reach end of the Path
    {
        if (charNumber!=-1) WaveGeneration.characterState[charNumber] = false;
        Destroy(gameObject);
    }
}
