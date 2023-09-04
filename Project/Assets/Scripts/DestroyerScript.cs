/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DestroyerScript : MonoBehaviour
{
    public Slider enzymeTimer;
    public GameObject slidersController; 

    public ScoreManager scoreManagerScript;

    public int timeEnzyme;
    public int numberOfEnzymeBarSubtractions;

    void OnTriggerStay2D(Collider2D other){
        DestroyObjects(other.gameObject);
    }

    void DestroyObjects(GameObject other){
        float angleZ = Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)),this.transform.rotation);
        float angleZOther = Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)),other.transform.rotation);

        if (other.gameObject.CompareTag("Substrate")){
            if(this.gameObject.transform.parent.GetComponent<PlayerController>().type == other.gameObject.GetComponent<SubstrateController>().type){
                if(angleZ + angleZOther > 155 && angleZ + angleZOther <205){
                    StartCoroutine(EnzymeTimer(other));
                    FindObjectOfType<AudioManager>().Play("SubstrateAcquisition");
                }
            }
        }
    }
    
    IEnumerator EnzymeTimer(GameObject other){

        this.gameObject.transform.parent.GetComponent<PlayerController>().ChangePlayer(-1);

        this.gameObject.GetComponent<BoxCollider2D>().enabled = false; 

        other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        other.gameObject.GetComponent<SpriteRenderer>().enabled = false; 

        float timeOfEachSubtratction = (float)timeEnzyme/numberOfEnzymeBarSubtractions;
        float valueOfEachSubstraction = (float)1/numberOfEnzymeBarSubtractions;

        for(int i = 0;i<=numberOfEnzymeBarSubtractions;i++){
            yield return new WaitForSeconds(timeOfEachSubtratction);
            enzymeTimer.value -= valueOfEachSubstraction;
        }

        enzymeTimer.value = 1;

        FindObjectOfType<AudioManager>().Play("SubstrateDestruction");
        
        other.GetComponent<SubstrateController>().DestructionEffect();

        other.GetComponent<SubstrateController>().RemoveShip();

        this.gameObject.transform.parent.GetComponent<PlayerController>().ChangePlayer(-2);

        scoreManagerScript.AddScore();

        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        slidersController.GetComponent<SideSliderController>().addFill(this.transform.parent.GetComponent<PlayerController>().type);

    }

    IEnumerator EnzymeTimeBarAnimation(){
        float timeOfEachSubtratction = (float)timeEnzyme/numberOfEnzymeBarSubtractions;
        float valueOfEachSubstraction = (float)1/numberOfEnzymeBarSubtractions;

        for(int i = 0;i<=numberOfEnzymeBarSubtractions;i++){
            yield return new WaitForSeconds(timeOfEachSubtratction);
            enzymeTimer.value -= valueOfEachSubstraction*1.1f;
        }

        enzymeTimer.value = 1;
    }

    public void reduceDestroyDelay(){
        timeEnzyme -= 2;
    }
    
    public void increaseDestroyDelay(){
        timeEnzyme += 2;
    }
}
