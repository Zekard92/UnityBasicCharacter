using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    #region fields

    [Range(2.5f, 10)]
    public float distance;
    [Range(-10, 40)]
    public float angle;

    #endregion fields

    void Update()
    {
        // calculates distance with the changes on mouse scroll wheel
        distance += Input.GetAxis("Mouse ScrollWheel");
        // calculates the angle of the camera with the changes on the Y movement of the mouse
        angle += Input.GetAxis("Mouse Y");

        // Apply limits to distance and angle
        Limits();

        // Calculates X and Y components of the new position of the camera
        float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = distance * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Defines the new position of the camera using the x and y components
        transform.localPosition = new Vector3(0, y, -x);
        // Rotates the camera using its x component and the desired angle
        transform.localEulerAngles = Vector3.right * angle;

    }

    private void Limits()
    {
        if (angle > 40)
            angle = 40;
        if (angle < -10)
            angle = -10;
        if (distance < 2.5f)
            distance = 2.5f;
        if (distance > 10)
            distance = 10;
    }
}
