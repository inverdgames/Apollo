using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

 
  public float Level;
  public int Money;

  public int RocketSelected;
  public int PlanetSelected;

  public int Rocket1status = 1;
  public int Rocket2status = 0;
  public int Rocket3status = 0;
  public int Rocket4status = 0;

  public float TopTime1=0;
  public float TopTime2=0;
  public float TopTime3=0;
  public float TopTime4=0;
  public float TopTime5=0;

 public PlayerData (Player player){

  Level =player.level;
  Money =player.money;
  
  TopTime1=player.HighScoreTime[1];
  TopTime2=player.HighScoreTime[2];
  TopTime3=player.HighScoreTime[3];
  TopTime4=player.HighScoreTime[4];
  TopTime5=player.HighScoreTime[5];

  Rocket1status = player.Rstatus[1];
  Rocket2status = player.Rstatus[2];
  Rocket3status = player.Rstatus[3];
  Rocket4status = player.Rstatus[4];
 }





}
