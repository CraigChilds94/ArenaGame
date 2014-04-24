using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private const int LEFT = -1,        /** Define some default directions **/
                      RIGHT = 1;

    int dir = RIGHT;
    int velocity = 0;
    bool onGround = false;
    BoxCollider collider = null;

	void Start () {
        collider = GetComponent<BoxCollider>();
	}

	void Update () {
	    
	}
}
