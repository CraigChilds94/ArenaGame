using UnityEngine;
using System.Collections;

/// <summary>
/// Handles networked characters
/// </summary>
public class NetworkCharacter : Photon.MonoBehaviour {

    public Animator anim;
    public CharacterMotor motor;

    Vector3 realPos = Vector3.zero;
    Quaternion realRot = Quaternion.identity;
    float velocity = 0;
    bool onGround = false, 
         jumping = false;

    /// <summary>
    /// What happens when this is loaded
    /// </summary>
	void Start () 
    {
	
	}
	
    /// <summary>
    /// On every frame update
    /// </summary>
	void Update () 
    {
        // if it's  us
        if (photonView.isMine)
        {
            // do nothing
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
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPos, 0.1f);
            transform.rotation = realRot;

            if (!onGround)
            {
                if (jumping)
                {
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                anim.SetBool("jumping", false);
            }

            anim.SetFloat("speed", velocity);
        }
	}

    /// <summary>
    /// Serializing the photon views
    /// </summary>
    /// <param name="stream"> the stream of information </param>
    /// <param name="info"> information about the messages </param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // if it's us tell them where we are, else it's everyone else telling use where they are
        if (stream.isWriting)
        {
            // send pos stuff
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            // send animation stuff
            stream.SendNext(Mathf.Abs(motor.movement.velocity.x));
            stream.SendNext(motor.grounded);
            stream.SendNext(Input.GetButton("Jump"));
        }
        else
        {
            // recieve pos
            realPos = (Vector3)stream.ReceiveNext();
            realRot = (Quaternion)stream.ReceiveNext();

            // receive anim info
            velocity = (float)stream.ReceiveNext();
            onGround = (bool)stream.ReceiveNext();
            jumping = (bool)stream.ReceiveNext();
        }
    }
}
