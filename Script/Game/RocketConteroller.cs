using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketConteroller : MonoBehaviour
{
     Rigidbody rb;

     public float RocketSpeed;
     public ParticleSystem Fire;
     public ParticleSystem DestroyBlast;
     public float PetrolCapacity;


     public string Name;
     public int Price;
     public float Power;

     public int TotalMoneyHave;


     [Header("Part-For-Share")]

     public float Scored;
     public float PetrolLeft=100;
     
     public bool isReady;
     public bool isStart;
     public bool isLanded;
     public bool gameOver;

     public int CoinCollected;

     public int NumberOfCoinCollected;
     public int NumberOfCanCollected;

     [Header("planet-section")]
     public int PlanetNo;
     public float PlanetScore;
     public int PlanetCoin;

     public bool IsSlowRun;


    public AudioSource CoinSound;
    public AudioSource CanSound;


     


     
    private void Start() {
    rb=GetComponent<Rigidbody>();
    PetrolLeft=100;
    CoinCollected=0;
    gameOver=false;
    Scored=0;
    isReady=false;
    isStart=false;
    isLanded=false;
    NumberOfCanCollected=0;
    NumberOfCoinCollected=0;
    
    }



    
   
      private void Update() {
          if(PetrolLeft > 100 && !gameOver){
              PetrolLeft=100;
          }
          if(isStart){
          if(IsSlowRun==false){
          if(PetrolLeft > 0.1  && !gameOver){
              isReady=true;
          }else{
              isReady= false;
          }
          }
          }
        if(Input.GetKey(KeyCode.Space) && isReady){
        accelrate();
        }
        
    }
    public void accelrate(){
       rb.velocity = new Vector3(0,RocketSpeed,0);
       Fire.Play();
       PetrolLeft=PetrolLeft-PetrolCapacity;
   }




   private void OnCollisionEnter (Collision collision) 
	{
        if(gameOver==false){
        
        
		if (collision.transform.tag == "Lander") 
		{
          isLanded=true;
          Scored=Scored+PlanetScore;
          CoinCollected=CoinCollected+PlanetCoin;
          TotalMoneyHave=TotalMoneyHave+CoinCollected;
          Destroy(GetComponent<Rigidbody>());
          gameOver=true;
          isReady=false;
		}else if(collision.transform.tag == "coin"){
            CoinSound.Play();
            NumberOfCoinCollected++;
            Scored=Scored+2;
            CoinCollected=CoinCollected+4;
            Destroy(collision.gameObject);
        }else if(collision.transform.tag == "can"){
            CanSound.Play();
            NumberOfCanCollected++;
            Scored=Scored+5;          
            PetrolLeft=PetrolLeft+10;
            Destroy(collision.gameObject);
            
        }else{
            TotalMoneyHave=TotalMoneyHave+CoinCollected;
            gameOver=true;
            rb.constraints=RigidbodyConstraints.None;
            DestroyBlast.Play();
            isReady=false;
        }
        }
    }

    public void addPlanet(int Num){
        if(Num==1){
           PlanetScore=25;
           PlanetCoin=20;
        }else if(Num==2){
           PlanetScore=50;
           PlanetCoin=40;
        }else if(Num==3){
           PlanetScore=75;
           PlanetCoin=60;
        }else if(Num==4){
           PlanetScore=100;
           PlanetCoin=80;
        }else{
           PlanetScore=10;
           PlanetCoin=10;
        }


    }
}
