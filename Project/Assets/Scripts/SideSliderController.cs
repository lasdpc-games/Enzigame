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

public class SideSliderController : MonoBehaviour
{

    public Slider[] sliders;

    Coroutine[] sliderCoroutine = new Coroutine[3];
    public GameObject losePanel;

    GameObject[] player;
    public GameObject[] itens;

    public bool lostTheGame = false;

    public float timeUntilNow = 0;


    public float startingB;
    public float[] b = {0f,0f,0f};

    void Start()
    {
        for(int i =0;i<3;i++){
            b[i] = startingB;
        }
        player = GameObject.FindGameObjectsWithTag("Player");

        ShowSlider(0);
        CallSliderCoroutine(0);
    }

    void Update()
    {
        timeUntilNow += Time.deltaTime;
    }

    public void addFill(int sliderIndex){
        if(sliderIndex == 0){
            b[0] += 3f;
        } else if(sliderIndex == 1){
            b[1] += 3f;
        } else if(sliderIndex == 2){
            b[2] += 3f;
        }
    }

    public void CallSliderCoroutine(int sliderIndex){
        sliders[sliderIndex].value = 1;
        sliderCoroutine[sliderIndex] = StartCoroutine(SliderController(sliderIndex));
    }

    public void StopSliderCoroutine(int sliderIndex){
        StopCoroutine(sliderCoroutine[sliderIndex]);
    }

    IEnumerator SliderController(int sliderIndex){
        while(sliders[sliderIndex].value > 0.01f){
            yield return new WaitForSeconds (0.2f);
            //sliders[sliderIndex].value -= timeUntilNow/5000;
            sliders[sliderIndex].value = -1/(1+Mathf.Pow(1.2f,-1 * (timeUntilNow-b[sliderIndex])))+1;
        }
        losePanel.SetActive(true);
        lostTheGame = true;
        for (int i = 0; i <itens.Length;i++){
            Destroy(itens[i]);
        }
        if(LevelManager.level == 0){
            FindObjectOfType<AudioManager>().Stop("Thermal");
        }else if(LevelManager.level == 1){
            FindObjectOfType<AudioManager>().Stop("Often");
        }else if(LevelManager.level == 2){
            FindObjectOfType<AudioManager>().Stop("Anode");
        }
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().Play("Dingos");
        Destroy(player[0].transform.GetChild(0).GetComponent<SpriteRenderer>());
    }

    public void HideSlider(int sliderIndex){
        sliders[sliderIndex].gameObject.SetActive(false);
    }
    public void ShowSlider(int sliderIndex){
        sliders[sliderIndex].gameObject.SetActive(true);
    }
}


