using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public void MobUnlock()
    {
        foreach (Transform child in transform)
        {
            Door door = child.gameObject.GetComponent<Door>();
            door.DeactivateMobLock();
        }
    }

    public void MobLock()
    {
        foreach (Transform child in transform)
        {
            Door door = child.gameObject.GetComponent<Door>();
            door.ActivateMobLock();
        }
    }
}
