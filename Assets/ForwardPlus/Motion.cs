using UnityEngine;
using Unity.Mathematics;
using Klak.Math;

namespace UrpTestbed.ForwardPlus {

sealed class Motion : MonoBehaviour
{
    [SerializeField] float _speed = 0.1f;
    [SerializeField] float _displace = 0.1f;

    float3 _position;

    void Start()
      => _position = transform.position;

    void Update()
    {
        var np = _position * 10 + math.float3(0, 0, _speed * Time.time);
        transform.position = _position + NoiseHelper.Float3(np, 0) * _displace;
    }
}

} // namespace UrpTestbed.ForwardPlus
