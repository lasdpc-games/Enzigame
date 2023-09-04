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

public class TeacherSpeakManager : MonoBehaviour
{
    string[] teacherLine = {
    /*PC:*/ "Olá aluno, eu sou a Professora! Para que eu continue meu diálogo clique nesse painel ou aperte a tecla espaço.", 
    "Esse é o EnziGame, use \"AWSD\" ou as setinhas para se mover e \"J e L\" para rotacionar!",

    "No corpo agem várias enzimas, essas são responsáveis por acelerar as reações químicas que acontecem em nosso corpo.",
    "Uma enzima pode acelerar uma reação quimica em 10^6 até 10^12 vezes. Elas são diversas e com vários tipos de funcionamentos.",
    "Nesse jogo vamos conhecer algumas das principais enzimas digestivas e o modelo chave-fechadura de funcionamento delas.",

    "A primeira fase do nosso jogo se passa na boca. A função dela na digestão dos alimentos é principalmente mecânico, lubrificante e de certo de controle de temperatura.",
    "Apesar disso, temos nela já algumas enzimas digestivas. Nela a principal enzima digestiva que temos é a Amilase salivar, que quebra polissacarídeos em maltose e glicose.",
    "As enzimas de modo geral aceleram a reação e quebram os substratos em produtos menores, cada enzima é extremamente específica ao seu substrato.",
    "Isso é chamado de modelo chave-fechadura. Devido ao encaixe único da enzima no seu substrato.",
    "Essa quebra é o começo de uma série de eventos que acontecerão a substância para que ela possa ser absorvida pelo nosso corpo.",

    "No Enzigame você controla uma enzima, e o que está navegando pela tela ao seu redor são os substratos.",
    "Perceba que elas tem um forma específica, vá atrás das que encaixam na enzima e quebre-as enconstando nelas.",
    "Assim você prepara as substâncias para serem quebradas por outras enzimas e outras coisas que vão vir a acontecer com elas...",
    "e equilibra os nutrientes do corpo (o que é mostrado na barra lateral) e ganha pontos.",
    "Consiga 10 pontos sem deixar que a barra lateral fique vazia para passsar de fase!",

    "As enzimas trabalham em um ph e temperatura ideias. Portanto uma enzima fora de seu ph ou temperatura ideal irá trabalhar mais devagar.",
    "No Enzigame, eventualmente o ph e a temperatura são alterados, isso pode significar...",
    "que a pessoa ingeriu algo que alterou o ph dentro de determinado orgão ou que está com febre, por exemplo.",
    "A enzima trabalhar mais devagar irá dificultar a regulação do corpo. Lembrando que se alguma barra esvazia totalmente o jogo é perdido!",

    "Parabéns você passou de fase! Agora saímos da boca e vamos para o estômago.",
    "No estômago, os alimentos que passaram pela boca são misturados e acidificados.",
    "Algumas enzimas entram em ação, a principal enzima digestiva que age no estômago é a pepsina...",
    "Essa enzima quebra as proteínas em partes menores que futuramente serão quebradas em partes ainda menores por outras enzimas.",

    "Parabéns você passou de fase! Agora saímos do estômago e vamos para o intestino.",
    "No intestino delgado, muitas enzimas entram em ação, enzimas que quebram lipídios, carboidratos, proteínas etc. Algumas dessas enzimas são produzidas pelo pâncreas...",
    "e aqui finalmente estamos perto de onde a maioria dos nutrientes é absorvida pelo corpo humano.",
    /*PC:*/"No Enzigame aqui é ultima fase, aperte as teclas \"1, 2 e 3\" ou os botões \"C P L\" em cimas das barras laterais para transitar entre as 3 enzimas existentes e quebrar os substratos.",
    "Você apenas pode mudar entre enzimas quando não houver substrato encaixado na sua enzima!",
    "Aqui você precisará equilibrar as 3 barras de nutrientes, então lembre-se de não deixar nenhuma delas zerar. Mude para a enzima necessária...",
    "quando a barra respectiva a ela estiver perto de 0 para não perder o jogo!"};
    public TMP_Text teacherText;
    public GameObject teacherPanel;
    
    int teacherSpeakIndex = 0;
    public int level = 0;
    public SideSliderController sideSliderScript;
    public GameObject enzymeTimer; 

    public bool pausedByTeacher = false;
    int helper = -1;

    public GameObject playerController;
    public GameObject playerController2;
    public PlayerController playerScript;

    void Start()
    { 
        if(PlayerController.isMobile == true){
            teacherLine[0] = "Olá aluno, eu sou a Professora! Para que eu continue meu diálogo toque nesse paínel.";
            teacherLine[1] = "Esse é o EnziGame, use o controle da esquerda para se mover e o da direta para rotacionar!";
            teacherLine[25] = "No Enzigame aqui é ultima fase, aperte os botões \"C P L\" em cima das barras laterais para transitar entre as 3 enzimas existentes e quebrar os substratos. Aqui você..."; 
        }
        if(MenuScript.teacherIsEnabled == true){
            teacherText.text = teacherLine[teacherSpeakIndex];
            ShowTeacher();
        }
    }

    void Update(){
        if(pausedByTeacher == true){
            if(this.gameObject.GetComponent<PauseMenuScript>().GameIsPaused != true){
                if(Input.GetKeyDown(KeyCode.Space)){
                    ChangeLine();
                }
            }
        }
    }

    public void ChangeLine(){
        if(MenuScript.teacherIsEnabled){
            FindObjectOfType<AudioManager>().Play("MenuJump");
            if(teacherSpeakIndex == 1 || teacherSpeakIndex == 4 || teacherSpeakIndex == 9){
                teacherSpeakIndex++;
                StartCoroutine(TeacherAwait(teacherSpeakIndex));
                teacherText.text = teacherLine[teacherSpeakIndex];

            }else if(teacherSpeakIndex == teacherLine.Length - 1 || teacherSpeakIndex == 14 || teacherSpeakIndex == 18 || teacherSpeakIndex == 22){

                if(helper == -1){
                    HideTeacher();
                }else{
                    ShowTeacher();
                    teacherSpeakIndex++;
                    teacherText.text = teacherLine[teacherSpeakIndex];
                }
                helper *= -1;

            }else{
                teacherSpeakIndex++;
                teacherText.text = teacherLine[teacherSpeakIndex];
            }
        }
    }

    IEnumerator TeacherAwait(int teacherSpeakIndex){
        HideTeacher();
        yield return new WaitForSeconds(3f);
        ShowTeacher();
    }

    public void ShowTeacher(){

        pausedByTeacher = true;
        Time.timeScale = 0f;


        playerController.transform.GetChild(0).GetComponent<RectTransform>().localPosition = Vector3.zero;
        playerController2.transform.GetChild(0).GetComponent<RectTransform>().localPosition = Vector3.zero;

        if(PlayerController.isMobile){
            playerController.SetActive(false);
            playerController2.SetActive(false);
        }

        teacherPanel.SetActive(true);
        //enzymeTimer.SetActive(false);
    }

    public void HideTeacher(){

        pausedByTeacher = false;
        Time.timeScale = 1f;

        if(PlayerController.isMobile){
            playerController.SetActive(true);
            playerController2.SetActive(true);
        }

        teacherPanel.SetActive(false);
        //enzymeTimer.SetActive(true);
    }
}
