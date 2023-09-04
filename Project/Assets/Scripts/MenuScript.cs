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
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static bool teacherIsEnabled = true;
    public static float volumeMusic = 0.05f;
    public static float volumeGame = 0.1f;
    
    public Slider volumeMusicController;
    public Slider volumeGameController;
    public GameObject teacherToogle;

    void Start(){
        if ((SceneManager. GetActiveScene () == SceneManager. GetSceneByName ("Menu"))){
            teacherToogle.GetComponent<Toggle>().isOn = teacherIsEnabled;

            volumeMusicController.value = volumeMusic;
            volumeGameController.value = volumeGame;
            FindObjectOfType<AudioManager>().Play("Thermal");

        }
    }

    void Update(){
        if(Input.GetKeyDown("r")){
            LoadGameScene();
            Debug.ClearDeveloperConsole();
        }
    }
    public void LoadMenuScene(){
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void LoadGameScene(){
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void SetTeacher(){
        teacherIsEnabled = !teacherIsEnabled; 
    }
    public void SetVolumeMusic(){
        volumeMusic = volumeMusicController.value;
        FindObjectOfType<AudioManager>().AudioAdjust();
    }
    public void SetVolumeGame(){
        volumeGame = volumeGameController.value;
        FindObjectOfType<AudioManager>().AudioAdjust();
    }
}
