using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerRotation : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Target.rotation.eulerAngles.x + 180f);

    }
}
