using UnityEngine;

public class Planet : MonoBehaviour
{
   
  void OnCollisionEnter (Collision collision) 
	{
		if(collision.transform.tag == "coin"){
            Destroy(collision.gameObject);
        }else if(collision.transform.tag == "can"){
            Destroy(collision.gameObject);
        }else if(collision.transform.tag == "Rock"){
            Destroy(collision.gameObject);
        }
        
    }
}
