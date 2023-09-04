/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int level;
    TeacherSpeakManager teacherScript;
    public SideSliderController sliderScript;
    public Sprite[] backgroundSprites;

    public GameObject bodyIndicative;
    public Sprite[] bodyIndicativeSprites;
    Image imageRenderer;


    public PlayerController playerScript;

    public TMP_Text levelText; 

    GameObject[,] background = new GameObject[40,40];
    int backgroundCount = 40;
    public GameObject backgroundPrefab;
    SpriteRenderer backgroundSpriteRenderer;

    public GameObject[] changePlayerButtons = new GameObject[3];

    void Start()
    {   
        level = 0;
        FindObjectOfType<AudioManager>().SoftPlay("Thermal");
        teacherScript = this.gameObject.GetComponent<TeacherSpeakManager>();
        for(int i = 0;i<backgroundCount;i++){
            for(int j = 0;j<backgroundCount;j++){
                background[i,j] = Instantiate(backgroundPrefab, new Vector3(i*20 -400, j*20 - 400, 0), Quaternion.Euler(new Vector3(0, 0, Random.Range(1,5)*90)));
                backgroundSpriteRenderer = background[i,j].GetComponent<SpriteRenderer>();
                backgroundSpriteRenderer.sprite = backgroundSprites[0];
            }
        }

        imageRenderer = bodyIndicative.GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void PassLevel(){
        Debug.Log("AAAAAAAAAAAA");
        Debug.Log(level);
        sliderScript.timeUntilNow = 0;
        for(int i = 0; i<3; i++){
            sliderScript.b[i] = sliderScript.startingB;
        }

        if(level == 0){
            FindObjectOfType<AudioManager>().SoftStop("Thermal");
            FindObjectOfType<AudioManager>().SoftPlay("Often");

            levelText.text = "Estômago";


            playerScript.ChangePlayer(2);

            sliderScript.StopSliderCoroutine(0);
            sliderScript.HideSlider(0);

            sliderScript.CallSliderCoroutine(1);
            sliderScript.ShowSlider(1);


            teacherScript.ChangeLine();
            imageRenderer.sprite = bodyIndicativeSprites[1];

            backgroundSpriteRenderer.sprite = backgroundSprites[1];

            for(int i = 0;i<backgroundCount;i++){
                for(int j = 0;j<backgroundCount;j++){
                    backgroundSpriteRenderer = background[i,j].GetComponent<SpriteRenderer>();
                    backgroundSpriteRenderer.sprite = backgroundSprites[1];
                }
            }
            
        }else if(level == 1){
            FindObjectOfType<AudioManager>().SoftStop("Often");
            FindObjectOfType<AudioManager>().SoftPlay("Anode");

            levelText.text = "Intestino";

            sliderScript.CallSliderCoroutine(0);
            sliderScript.ShowSlider(0);

            sliderScript.CallSliderCoroutine(2);
            sliderScript.ShowSlider(2);

            teacherScript.ChangeLine();

            imageRenderer.sprite = bodyIndicativeSprites[2];

            for(int i = 0;i<backgroundCount;i++){
                for(int j = 0;j<backgroundCount;j++){
                    backgroundSpriteRenderer = background[i,j].GetComponent<SpriteRenderer>();
                    backgroundSpriteRenderer.sprite = backgroundSprites[2];
                }
            }
            for(int i = 0;i<changePlayerButtons.Length;i++){
                changePlayerButtons[i].SetActive(true);
            }
        }
        
        level++;
        teacherScript.level = level;
        playerScript.level = level;
    }
}
