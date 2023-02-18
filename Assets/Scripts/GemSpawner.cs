using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private Gem _prefab;

    private Transform[] _points;

    void Start()
    {
        _points = GetComponentsInChildren<Transform>();

        for (int i = 1; i < _points.Length; i++)
        {
            Instantiate(_prefab, _points[i].transform.position, _points[i].transform.rotation);
        }
    }
}
