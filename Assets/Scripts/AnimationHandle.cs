using UnityEngine;
using System.Collections;

public class AnimationHandle : MonoBehaviour {

    public Animator anim;
    public CharacterMotor motor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!motor.grounded)
        {
            if (Input.GetButton("Jump"))
            {
                anim.SetBool("jumping", true);
            }
        }
        else
        {
            anim.SetBool("jumping", false);
        }

        anim.SetFloat("speed", Mathf.Abs(motor.movement.velocity.x));
	}
}
