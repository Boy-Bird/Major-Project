using Unity.Netcode;
using UnityEngine;

[RequireComponent (typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lookSensitivity = 10f;
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        /// Calculate movement 2velocity as a 3D vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        // Apply movement
        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        // Apply rotation
        motor.Rotate(_rotation);


        // Calculate Camera rotation as a 3D vector
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        // Apply rotation
        motor.RotateCamera(_cameraRotation);
    }

}
