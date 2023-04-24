using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private float yaw;
    [SerializeField] private float yawAmount = 120f;

    [SerializeField] private VariableJoystick variableJoystick;
    private Vector3 moveDir;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * speed * Time.deltaTime;

        float horizontal = variableJoystick.Horizontal;
        float vertical = variableJoystick.Vertical;
        float roll = Mathf.Lerp(0f,45f, Mathf.Abs(horizontal)* -Mathf.Sign(horizontal));

        yaw += horizontal * yawAmount * Time.deltaTime;

        moveDir = Vector3.right * horizontal + Vector3.forward * vertical;
        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
           // transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.forward * roll);
        }

       // 

        // yaw += variableJoystick.Horizontal * yawAmount * Time.deltaTime;
        // transform.localRotation = Quaternion.Euler(Vector3.up * yaw);
        //Debug.Log(variableJoystick.Horizontal.ToString());
    }
}
