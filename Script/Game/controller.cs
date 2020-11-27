using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class controller : MonoBehaviour
{


[Header("Selected-rocket & Planet")]
    public GameObject[] rocket;
    public GameObject[] plenet;

[Header("Rokcet and Planet In Script")]
    GameObject Player;
    Rigidbody PlayerRb;
    GameObject Planet;
    RocketConteroller Playerscript;

    PlanetScript PlanetScript;

   
[Header("Planet and Surrouning Item Like Spwners")]
   public Transform PlayerStart;
    public Transform PlanetStart;
    public GameObject[] SpaceOBJ;
    public float planetSpeed;
    public GameObject planetArea; 
    public Transform[] SawnpPoint;
    public Transform CoinSection;

[Header("Player-UI-Area")]
    
    public TextMeshProUGUI CollectedCoin;
    public TextMeshProUGUI ScoreScored;
   
    public Slider PertorlTank;
    public GameObject PerTank;

[Header("Gaame-Over-Panel")]
public GameObject gameOverPanel;
//Score Area;
public TextMeshProUGUI TotalScore;
public TextMeshProUGUI CanScore;
public TextMeshProUGUI CoinScore;
public TextMeshProUGUI LandScore;

//Coin Area;
public TextMeshProUGUI TotalCoin;
public TextMeshProUGUI CoinCoin;
public TextMeshProUGUI LandCoin;

[Header("Data-From And To Player")]
public Player DATA;
public TransferData TransferData;
public TextMeshProUGUI PlayerCoin;
public GameObject Lander;

[Header("time-Contoller")]

public TimerController TimerController;
public GameObject Timer;

public float TopTime;
bool isOver;

    



 
   
   


    private void Start() {
    AddPlayer();
    isOver=true;
    
      planetSpeed=15;
    PlayerRb.constraints=RigidbodyConstraints.FreezeAll;
    Playerscript.addPlanet(TransferData.selectedPlanet);
    Playerscript.TotalMoneyHave=DATA.money;
    if(TransferData.selectedMode==2){
      Destroy(Lander);
      Destroy(Timer);
      Playerscript.IsSlowRun=false;
      PerTank.SetActive(true);
      InvokeRepeating("CreateCoin",0,1);
    }else if(TransferData.selectedMode==3){
      Destroy(Lander);
      Destroy(PerTank);
      Playerscript.IsSlowRun=true;
     InvokeRepeating("CreateCoin",0,1);
    }else{
      Destroy(Timer);
      Playerscript.IsSlowRun=false;
      PerTank.SetActive(true);
      InvokeRepeating("CreateCoin",0,2);
    }
    }


    private void Update() {
    planetSpeed=planetSpeed+0.01f;
    PlayerCoin.text=""+Playerscript.TotalMoneyHave;

//PlanetRotation;
    planetArea.transform.Rotate(new Vector3(0,0,planetSpeed)*Time.deltaTime);
//planetSpeed Controller;
 
//UI Setting;
        PertorlTank.value=Playerscript.PetrolLeft;
        CollectedCoin.text=""+Playerscript.CoinCollected; 
        ScoreScored.text=""+Playerscript.Scored;

//GameOver Penal

      if(Playerscript.gameOver==true && isOver!=false){
        GameOverFunction();
     
      }

  if(Playerscript.isLanded==true){
    Player.transform.parent=PlanetStart;
  }

}



void GameOverFunction(){
    ShowGameOverDetails();
        gameOverPanel.SetActive(true);
        isOver=true;
     TopTime=TimerController.EndTimer();
           
           for (int i = 1; i <= 5; i++)
           {
             if(DATA.HighScoreTime[i] < TopTime && isOver==true){
               for (int j= 5-i; j > 0; j--)
               {
                   DATA.HighScoreTime[j+i]=DATA.HighScoreTime[j+i-1];
               }
               DATA.HighScoreTime[i]=TopTime;
               isOver=false;
               
             }
               
           }
       
}

public void Startgame(){
  PlayerRb.constraints=RigidbodyConstraints.None;
  PlayerRb.constraints=RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionZ;
  Playerscript.isStart=true;
  Playerscript.isReady=true;
  if(TransferData.selectedMode==3){
    TimerController.BeginTimer();
  }

}












//atmostphere Elements adding ;
  public void CreateCoin(){
     int RR=Random.Range(0,15);
     int OO;
     if(TransferData.selectedMode==3){
       OO=Random.Range(1,10);
     }else{
       OO=Random.Range(0,10);
     }
     
     GameObject aa = Instantiate(SpaceOBJ[OO],SawnpPoint[RR].position,SawnpPoint[RR].rotation);
     aa.transform.parent=CoinSection;
 }




//Adding Player And Planet;
  void AddPlayer(){
   Player = Instantiate(rocket[TransferData.selectedRocket],PlayerStart.position,PlayerStart.rotation);
   PlayerRb=Player.GetComponent<Rigidbody>();
   Playerscript=Player.GetComponent<RocketConteroller>();
   Planet = Instantiate(plenet[TransferData.selectedPlanet],PlanetStart.position,PlanetStart.rotation);
   Planet.transform.parent=PlanetStart;
   PlanetScript=Planet.GetComponent<PlanetScript>();
   PlanetStart.localScale=new Vector3(5.3f,5.3f,5.3f);
     }

//GameOver panel Items

public void ShowGameOverDetails(){

  //Score-Area
  CanScore.text=""+Playerscript.NumberOfCanCollected*5;
  CoinScore.text=""+Playerscript.NumberOfCoinCollected*2;
  TotalScore.text=""+Playerscript.Scored;
  //Coin-Area
  CoinCoin.text=""+Playerscript.NumberOfCoinCollected*4;
  TotalCoin.text=""+Playerscript.CoinCollected;

  if(Playerscript.isLanded==true){
  LandScore.text=""+Playerscript.PlanetScore;
  LandCoin.text=""+Playerscript.PlanetCoin;
  }else{
    LandScore.text="Nil";
  LandCoin.text="Nil";
  }

}


public void PlayAgain(){
AddPlayerDetails();
SceneManager.LoadScene(1);
}
public void Home(){
AddPlayerDetails();
SceneManager.LoadScene(0);
}






public void AddPlayerDetails(){
  DATA.money=DATA.money+Playerscript.CoinCollected;
  DATA.level=DATA.level+Playerscript.Scored;
  DATA.SavePlayer();
}


  
}