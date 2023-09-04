/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public bool GameIsPaused = false;
    
    TeacherSpeakManager teacherScript;
    public SideSliderController sliderScript;

    public GameObject pauseMenuUI;

    public GameObject pauseButton;

    void Start(){
        Time.timeScale = 1f;
        teacherScript = this.GetComponent<TeacherSpeakManager>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                if(sliderScript.lostTheGame != true){
                    Resume();
                }
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseButton.SetActive(true);
        pauseMenuUI.SetActive(false);
            if(teacherScript.pausedByTeacher != true){
                Time.timeScale = 1f;
            }
        GameIsPaused = false;
    }
    public void Pause(){
        pauseButton.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
