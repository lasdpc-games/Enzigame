/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstrateSpawner : MonoBehaviour
{
    public GameObject substratePrefab;

     //=============================
    public GameObject game_area;
    //public GameObject ship_prefab;
 
    public int ship_count = 0;
    public int ship_limit = 150;
    public int ships_per_frame = 1;
 
    public float spawn_circle_radius = 80.0f;
    public float death_circle_radius = 90.0f;
 
    public float fastest_speed = 12.0f;
    public float slowest_speed = 0.75f;
    //================================

    // Start is called before the first frame update
    void Start()
    {
        InitialPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        MaintainPopulation();
    }

    void InitialPopulation()
    {
        /** To avoid having to wait for the ships to enter the screen at start up, create an
        initial set of ships for instant action. **/
 
        for(int i=0; i<ship_limit; i++)
        {
            Vector3 position = GetRandomPosition(true);
            SubstrateController substrateScript = AddShip(position);
            substrateScript.transform.Rotate(Vector3.forward * Random.Range(0.0f, 360.0f));
        }
    }
 
    void MaintainPopulation()
    {
        /** Create more ships as old ones are destroyed, while respecting the object limit. **/
 
        if(ship_count < ship_limit)
        {
            for(int i=0; i<ships_per_frame; i++)
            {
                Vector3 position = GetRandomPosition(false);
                SubstrateController substrateScript = AddShip(position);
                substrateScript.transform.Rotate(Vector3.forward * Random.Range(-45.0f,45.0f));
            }
        }
    }
 
    Vector3 GetRandomPosition(bool within_camera)
    {
        /** Get a random spawn position, using a 2D circle around the game area. **/
        Vector3 position = Random.insideUnitCircle;
        do{
            position = Random.insideUnitCircle;
            if(within_camera == false)
            {
                position = position.normalized;
            }
    
            position *= spawn_circle_radius;
            position += game_area.transform.position;

        }while((position.x > 320 || position.x < -320) || (position.y > 320 || position.y < -320) );

        return position;

    }
 
    SubstrateController AddShip(Vector3 position)
    {
        /**Add a new ship to the game and set the basic attributes. **/
 
        ship_count += 1;
        GameObject newSubstrate = Instantiate(
            substratePrefab,
            position,
            Quaternion.FromToRotation(Vector3.up, (game_area.transform.position-position)),
            gameObject.transform
        );
 
        SubstrateController substrateScript = newSubstrate.GetComponent<SubstrateController>();
        substrateScript.substrateSpawner = this;
        substrateScript.game_area = game_area;
        substrateScript.speed = Random.Range(slowest_speed, fastest_speed);
 
        return substrateScript;
    }
}
