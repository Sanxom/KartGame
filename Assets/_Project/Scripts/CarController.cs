using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public TrackZone currentTrackZone;
    public bool canControlCar;
    public int zonesPassed;
    public int racePosition;
    public int currentLap;

    [SerializeField] private Transform _carModel;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _groundCheckRate;

    private Vector3 _startModelOffset;
    private bool _accelerateInput;
    private float _currentYRotation;
    private float _turnInput;

    private void Start()
    {
        _startModelOffset = _carModel.transform.localPosition;
        GameManager.instance.cars.Add(this);
        transform.position = GameManager.instance.spawnPoints[GameManager.instance.cars.Count - 1].position;
    }

    private void Update()
    {
        if (!canControlCar)
            _turnInput = 0;

        float turnRate = Vector3.Dot(_rigidBody.velocity.normalized, _carModel.forward);
        turnRate = Mathf.Abs(turnRate);

        _currentYRotation += _turnInput * _turnSpeed * turnRate * Time.deltaTime;

        // Sets the car model's position and rotation appropriately every frame in order to not rotate it with the parent object
        _carModel.position = transform.position + _startModelOffset;
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (!canControlCar)
            return;

        if (_accelerateInput)
        {
            _rigidBody.AddForce(_carModel.forward * _acceleration, ForceMode.Acceleration);
        }
    }

    public void OnAccelerateInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            _accelerateInput = true;
        }
        else
        {
            _accelerateInput = false;
        }
    }

    public void OnTurnInput(InputAction.CallbackContext context)
    {
        _turnInput = context.ReadValue<float>();
    }

    private void CheckGround()
    {
        Ray ray = new(transform.position + new Vector3(0, -0.75f, 0), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 1))
        {
            _carModel.up = hit.normal;
        }
        else
        {
            _carModel.up = Vector3.up;
        }

        _carModel.Rotate(new Vector3(0, _currentYRotation, 0), Space.Self);
    }
}