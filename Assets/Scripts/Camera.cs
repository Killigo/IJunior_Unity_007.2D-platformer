using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _position;
    private float _zCameraPosition = -10f;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        _position = _player.position;
        _position.z = _zCameraPosition;

        transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime);
    }
}
