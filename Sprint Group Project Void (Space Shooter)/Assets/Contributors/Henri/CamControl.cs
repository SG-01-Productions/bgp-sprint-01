using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    /// <summary>
    /// Gets player from unity side to check its position and move camera accordingly
    /// </summary>
    public GameObject player;
    [SerializeField] float zoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Positions camera at a set height above the player ship
    /// </summary>
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - zoom);
    }
}
