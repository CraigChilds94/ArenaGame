using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float dir = 0, 
               speed = 0.0001f;

    public float life = 100;
    
	void Start () {}
	
	void FixedUpdate () {
        if (life > 0)
        {
            life -= Time.deltaTime;
            // move
            transform.position = new Vector3(transform.position.x + (speed), transform.position.y, transform.position.z);
        }
        else
        {
            PhotonNetwork.Destroy(GetComponent<PhotonView>());
        }
	}
}
