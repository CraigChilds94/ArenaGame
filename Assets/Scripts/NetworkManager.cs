using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    public const string version = "0.1";
    public GameObject spawn;

	/// <summary>
	/// Initialise the Network Manager
	/// </summary>
	void Start () 
    {
        Connect();
	}

    /// <summary>
    /// Create a connection to the server
    /// </summary>
    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings(version);
    }

    /// <summary>
    /// When the GUI is displayed
    /// </summary>
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    /// <summary>
    /// What happens when a lobby is joined?
    /// </summary>
    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// Handle room join fails
    /// </summary>
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Failed to join a room.");
        Debug.Log("Creating a new room....");
        PhotonNetwork.CreateRoom(null);
    }

    /// <summary>
    /// What happens once we've joined a room
    /// </summary>
    void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        SpawnMyPlayer();
    }

    /// <summary>
    /// Spawn the player
    /// </summary>
    void SpawnMyPlayer()
    {
        PhotonNetwork.Instantiate("player", spawn.transform.position, Quaternion.identity, 0);
    }
}
