using UnityEngine;
using System.Collections;

/// <summary>
/// Handles management of the networking applications
/// </summary>
public class NetworkManager : MonoBehaviour {

    public const string version = "0.1";

    private SpawnSpot[] spawns;

    #region Network setup stuff
    /// <summary>
	/// Initialise the Network Manager
	/// </summary>
	void Start () 
    {
        spawns = GameObject.FindObjectsOfType<SpawnSpot>();
        Connect();
	}

    /// <summary>
    /// Create a connection to the server
    /// </summary>
    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings(version);
    }
    #endregion

    #region GUI stuff
    /// <summary>
    /// When the GUI is displayed
    /// </summary>
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    #endregion

    #region Handling server join
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
    #endregion

    #region Player spawning

    /// <summary>
    /// Spawn the player
    /// </summary>
    void SpawnMyPlayer()
    {
        if (spawns == null)
        {
            Debug.LogError("There are no spawn locations in this scene.");
            return;
        }

        SpawnSpot spawn = getRandSpawn();
        setupMyPlayer(PhotonNetwork.Instantiate("player", spawn.transform.position, Quaternion.identity, 0));
    }

    /// <summary>
    /// Get a random (Active) spawn location from the level
    /// </summary>
    /// <returns>
    /// An active spawn location
    /// </returns>
    private SpawnSpot getRandSpawn()
    {
        SpawnSpot spawn = spawns[Random.Range(0, spawns.Length)];
        while (!spawn.isActive())
        {
            spawn = spawns[Random.Range(0, spawns.Length)];
        }
        return spawn;
    }

    /// <summary>
    /// Handling enable and disable of local stuff
    /// </summary>
    /// <param name="player">An instance of the player</param>
    private void setupMyPlayer(GameObject player)
    {
        // ENABLES
        player.GetComponent<CharacterMotor>().enabled = true;
        player.GetComponent<PlatformInputController>().enabled = true;
        player.GetComponent<AnimationHandle>().enabled = true;
    }
    #endregion
}
