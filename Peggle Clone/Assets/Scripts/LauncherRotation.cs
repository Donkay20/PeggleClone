using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherRotation : MonoBehaviour
{
    Camera playerCamera;
    void Start()
    {
        playerCamera = Camera.main;
    }
    void Update()
    {
        Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(angle, -65, 65));
    }
}