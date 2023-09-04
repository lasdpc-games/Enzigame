/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstrateController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] player;

    public SubstrateSpawner substrateSpawner;
    public GameObject game_area;
 
    public float speed;

    public GameObject destructionEffectPrefab;

    public int type;

    SpriteRenderer spriteRenderer;
    public Sprite[] substrateSprites;
    public Sprite[] destructionEffects;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        type = Random.Range(0,3);

        switch (type)
        {
            case 0:
                spriteRenderer.sprite = substrateSprites[0];
                break;
            case 1:
                spriteRenderer.sprite = substrateSprites[1];
                break;
            case 2:
                spriteRenderer.sprite = substrateSprites[2];
                break;   
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        float angleZ =Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)),transform.rotation);
    }

    void Move(){
        /** Move this ship forward per frame, if it gets too far from the game area, destroy it **/
 
        transform.position += transform.up * (Time.deltaTime * speed);
 
        float distance = Vector3.Distance(transform.position, game_area.transform.position);
        if(distance > substrateSpawner.death_circle_radius)
        {
            RemoveShip();
        }
    }
 
    public void RemoveShip(){
        /** Update the total ship count and then destroy this individual ship. **/
        Destroy(gameObject);
        substrateSpawner.ship_count -= 1;
    }

    public void DestructionEffect(){
        GameObject destructionEffect1 = Instantiate(destructionEffectPrefab,player[0].transform.GetChild(0).position,Quaternion.Euler(new Vector3(0, 0, Random.Range(0,360))));
        GameObject destructionEffect2 = Instantiate(destructionEffectPrefab,player[0].transform.GetChild(0).position,Quaternion.Euler(new Vector3(0, 0, Random.Range(0,360))));
        
        switch (type)
        {
            case 0:
                destructionEffect1.GetComponent<SpriteRenderer>().sprite = destructionEffects[0];
                destructionEffect2.GetComponent<SpriteRenderer>().sprite = destructionEffects[1];
                break;
            case 1:
                destructionEffect1.GetComponent<SpriteRenderer>().sprite = destructionEffects[2];
                destructionEffect2.GetComponent<SpriteRenderer>().sprite = destructionEffects[3];
                break;
            case 2:
                destructionEffect1.GetComponent<SpriteRenderer>().sprite = destructionEffects[4];
                destructionEffect2.GetComponent<SpriteRenderer>().sprite = destructionEffects[5];
                break;   
        }
    
    }
}
