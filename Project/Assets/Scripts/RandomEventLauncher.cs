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

public class RandomEventLauncher : MonoBehaviour
{
    public GameObject destroyer;
    public GameObject[] textPHAndTemperature;
    public TeacherSpeakManager teacherScript;

    void Start()
    {
        StartCoroutine (StartPHAndTemperature());
    }

    IEnumerator StartPHAndTemperature(){
        yield return new WaitForSeconds(12);
        for(int i = 0;i<2;i++){
            textPHAndTemperature[i].SetActive(true);
            FindObjectOfType<AudioManager>().Play("PHTempOn");
            destroyer.GetComponent<DestroyerScript>().increaseDestroyDelay();
        }
        teacherScript.ChangeLine();

        yield return new WaitForSeconds(5);

        for(int i = 0;i<2;i++){
            textPHAndTemperature[i].SetActive(false);
            FindObjectOfType<AudioManager>().Play("PHTempOff");
            destroyer.GetComponent<DestroyerScript>().reduceDestroyDelay();
        }
        
        for(int i = 0;i<2;i++){
            StartCoroutine (PHAndTemperatureEvent(i));  
        }
    }

    IEnumerator PHAndTemperatureEvent(int phOrTemperature){
        while(true){

            int temp = Random.Range(0,10);
            if (temp == 1){
                destroyer.GetComponent<DestroyerScript>().increaseDestroyDelay();
                textPHAndTemperature[phOrTemperature].SetActive(true);
                FindObjectOfType<AudioManager>().Play("PHTempOn");
            }
            yield return new WaitForSeconds(5);
            if(temp == 1){
                destroyer.GetComponent<DestroyerScript>().reduceDestroyDelay();
                textPHAndTemperature[phOrTemperature].SetActive(false);
                FindObjectOfType<AudioManager>().Play("PHTempOff");
            }
        }
    }
}
