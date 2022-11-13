using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSelfDestroyerScript : MonoBehaviour
{
    // This script destroys itself and gameObject it is attached to in two seconds.
    // Created for deathSoundPlayer objects, that are created after gameObject get's destroyed, to play their sounds.
    void Start()
    {
        Invoke("DestroySelf", 2f);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
