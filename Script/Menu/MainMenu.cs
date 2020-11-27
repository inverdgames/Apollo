using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MainPenal;
    public GameObject PlayPenal;

    [Header("Shop")]
    public GameObject ShopPenal;
    public Transform ShopSpawnPoint;

    public TextMeshProUGUI RocketName;
    public TextMeshProUGUI RocketPrice;
    public TextMeshProUGUI RocketPower;
    public TextMeshProUGUI BuyButtonText;
    bool Lock;
     RocketConteroller RocketData;

     [Header("selection")]
    public GameObject SelectionPenal;
    public GameObject[] Planets;
    public GameObject[] Rockets;
    public Transform RocketSpawnPoint;
    public Transform planetSpawnPoint;
    GameObject Rocket;
    GameObject ThePlanet;
    int CurrentRocket = 0;
    
    int CurrentPlanet = 0;
    int PlayMode = 0;

    public TransferData TransferData;
    [Header("All_Show_UI")]
    public TextMeshProUGUI moneyUI;
    public Slider LevelSlider;

    [Header("slow-run")]
    public TimeSpan HighscoreTime;
    
    public TextMeshProUGUI Recode1;
    public TextMeshProUGUI Recode2;
    public TextMeshProUGUI Recode3;
    public TextMeshProUGUI Recode4;
    public TextMeshProUGUI Recode5;




    public Player PLY;

    private void Start() {
     Recode1.text=changeToTimeFormat(PLY.HighScoreTime[1]);
     Recode2.text=changeToTimeFormat(PLY.HighScoreTime[2]);
     Recode3.text=changeToTimeFormat(PLY.HighScoreTime[3]);
     Recode4.text=changeToTimeFormat(PLY.HighScoreTime[4]);
     Recode5.text=changeToTimeFormat(PLY.HighScoreTime[5]);
  
      
      
        SetMainPenal();
    }
    
    public void SetActivePanel(string activePanel){
        PlayPenal.SetActive(activePanel.Equals(PlayPenal.name));   
        MainPenal.SetActive(activePanel.Equals(MainPenal.name));   
        ShopPenal.SetActive(activePanel.Equals(ShopPenal.name));   
        SelectionPenal.SetActive(activePanel.Equals(SelectionPenal.name));   
    }

public void Quit(){
  PLY.SavePlayer();
  Application.Quit();
}
   public void SetMainPenal(){
     Lock=true;
     CurrentPlanet=0;
      CurrentRocket=0;
   SetActivePanel(MainPenal.name);
    }

    public void SetPlayPenal(){
      Lock=true;
      CurrentPlanet=0;
      CurrentRocket=0;
   SetActivePanel(PlayPenal.name);
    }public void SetShopPenal(){
      Lock=true;
      CurrentPlanet=0;
      CurrentRocket=0;
      ShopNext();
   SetActivePanel(ShopPenal.name);
    }

    public void RewardMode(){
      PlayMode=1;
      Lock=true;
      CurrentPlanet=0;
      CurrentRocket=0;
      OnRocketNext();
      PlanetAdd();
    SetActivePanel(SelectionPenal.name);
    }

    public void FreeMode(){
      PlayMode=2;
      Lock=true;
      CurrentPlanet=0;
      CurrentRocket=0;
      OnRocketNext();
      PlanetAdd();
    SetActivePanel(SelectionPenal.name);
    
    }
    
    public void SlowRun(){
      PlayMode=3;
      Lock=true;
      CurrentPlanet=0;
      CurrentRocket=0;
      OnRocketNext();
      PlanetAdd();
    SetActivePanel(SelectionPenal.name);
    
    }

private void Update() {

        moneyUI.text=""+PLY.money;
        LevelSlider.value=PLY.level;


  if(Lock){
           BuyButtonText.text="BUY";
         }else{
           BuyButtonText.text="EQUIPPED";
         }
}



//Selection Penal Area
    public void OnRocketNext(){
        if(CurrentRocket >= 4){
        CurrentRocket=0;
       }
       CurrentRocket++;
      if(PLY.CheckRocket(CurrentRocket) != 1){
        OnRocketNext();
      }


       if(Rockets!= null)
         Destroy(Rocket); 
        Rocket = Instantiate(Rockets[CurrentRocket],RocketSpawnPoint.position,RocketSpawnPoint.rotation);
         Rocket.transform.parent=RocketSpawnPoint;
          Destroy(Rocket.GetComponent<Rigidbody>());

    }

public void PlanetAdd(){
  if(CurrentPlanet >= 4){
        CurrentPlanet=0;
  }
  if(PLY.level > 499 && CurrentPlanet ==1){
  CurrentPlanet++;
  }else if(PLY.level > 1499 && CurrentPlanet ==2){
  CurrentPlanet++;
  }else if(PLY.level > 2999 && CurrentPlanet ==3){
  CurrentPlanet++;
  }else{
    CurrentPlanet=1;
  }

  if(ThePlanet!= null)
  Destroy(ThePlanet); 
ThePlanet=Instantiate(Planets[CurrentPlanet],planetSpawnPoint.position,planetSpawnPoint.rotation);
ThePlanet.transform.parent=planetSpawnPoint;


}





//Shop System

public void ShopNext(){
  
if(CurrentRocket >= 4){
        CurrentRocket=0;
       }
       CurrentRocket++;
      if(PLY.CheckRocket(CurrentRocket) != 1){
        Lock=true;
      }else{
        Lock=false;
      }


       if(Rockets!= null)
         Destroy(Rocket); 
        Rocket = Instantiate(Rockets[CurrentRocket],ShopSpawnPoint.position,ShopSpawnPoint.rotation);
        Rocket.transform.parent=ShopSpawnPoint;
        Rocket.transform.localScale=new Vector3(1.5f,1.5f,1.5f);
        Destroy(Rocket.GetComponent<Rigidbody>());

       RocketData=Rocket.GetComponent<RocketConteroller>();
         
       RocketName.text ="Name :"+RocketData.Name;
       RocketPrice.text ="Price: $"+RocketData.Price;
       RocketPower.text ="Power: "+RocketData.Power;
    
}

public void BuyRocket(){
if(Lock && PLY.money >= RocketData.Price){
  PLY.money=PLY.money-RocketData.Price;
  PLY.Rstatus[CurrentRocket]=1;
  Lock=false;
  PLY.SavePlayer();
}
}




//On game Start

public void StartGame(){
  TransferData.selectedPlanet=CurrentPlanet;
  TransferData.selectedRocket=CurrentRocket;
  TransferData.selectedMode=PlayMode;
  PLY.SavePlayer();
  SceneManager.LoadScene(1);
  
}


public string changeToTimeFormat(float Time){
      HighscoreTime= TimeSpan.FromSeconds(Time);
      string timePlayingStr = HighscoreTime.ToString("mm':'ss'.'ff");
      return timePlayingStr;
}

}
