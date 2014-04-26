using UnityEngine;
using System.Collections;

/// <summary>
/// Handles networked characters
/// </summary>
public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 realPos = Vector3.zero;
    Quaternion realRot = Quaternion.identity;

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
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPos, 0.1f);
            transform.rotation = realRot;
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
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realPos = (Vector3)stream.ReceiveNext();
            realRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
