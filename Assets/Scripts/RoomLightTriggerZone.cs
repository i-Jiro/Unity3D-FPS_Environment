using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLightTriggerZone : MonoBehaviour
{
    [SerializeField] private Light _computerDeskLight;

    [SerializeField] private Light _cornerRoomLight;
    private bool _activated = false;

    private IEnumerator TurnOnLights()
    {
        _computerDeskLight.enabled = true;
        yield return new WaitForSeconds(1f);
        _cornerRoomLight.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_activated)
        {
            _activated = true;
            StartCoroutine(TurnOnLights());
        }
    }
}
