using UnityEngine;
using System.Collections;

/// <summary>
/// Handles meta data for spawn locations
/// </summary>
public class SpawnSpot : MonoBehaviour {

    public int teamId = 0;
    private bool isActivated = true;

    /// <summary>
    /// Is this spawn point active?
    /// </summary>
    /// <returns>
    /// True if active, false otherwise
    /// </returns>
    public bool isActive()
    {
        return isActivated;
    }
}
