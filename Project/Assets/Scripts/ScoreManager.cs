/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highestScoreText;
    public int score;
    public int highestScore;
    public LevelManager levelManagerScript;

    void Start()
    {
        LoadScore();
    }

    void Update()
    { 
        if (!(SceneManager. GetActiveScene () == SceneManager. GetSceneByName ("Menu"))){
            if(Input.GetKeyDown("i")){
                AddScore();
            }
        }
    }

    public void AddScore(){
        score++;
        scoreText.text = "Pontos:" + score;
        if(score == 20 || score == 10){
            levelManagerScript.PassLevel();
        }
    }

    public void SaveScore(){
        if(score > highestScore){
            SaveSystem.SavePlayer(this);
        }
    }
    public void LoadScore(){
        HighestScoreData data = SaveSystem.LoadPlayer();
        highestScore = data.highestScore;
        if ((SceneManager. GetActiveScene () == SceneManager. GetSceneByName ("Menu"))){
            highestScoreText.text = "Maior Pontuação:" + highestScore;
        }
    }
}
