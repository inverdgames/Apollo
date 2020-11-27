using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float level;
    public int money;

    public int[] Rstatus;

    public float[] HighScoreTime;

    



    private void Update() {
        Rstatus[1]=1;
    }

    private void Start() { 
        LoadPlayer();
    }
    

    public void SavePlayer(){
        SaveFiles.savePlayer(this);
    }
     public void LoadPlayer(){
       PlayerData data = SaveFiles.LoadData();
       level= data.Level;
       money= data.Money;
       HighScoreTime[1]=data.TopTime1;
       HighScoreTime[2]=data.TopTime2;
       HighScoreTime[3]=data.TopTime3;
       HighScoreTime[4]=data.TopTime4;
       HighScoreTime[5]=data.TopTime5;
       Rstatus[2]=data.Rocket2status;
       Rstatus[3]=data.Rocket3status;
       Rstatus[4]=data.Rocket4status;
    }

    public int CheckRocket(int RocketNo){
       
        int Value=Rstatus[RocketNo];
        return Value;
    }


}
