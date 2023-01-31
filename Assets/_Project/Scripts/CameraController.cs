using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _rotateSpeed;

    private void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _target.rotation, _rotateSpeed * Time.deltaTime);
    }
}