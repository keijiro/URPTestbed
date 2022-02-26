using UnityEngine;
using CoreUtils = UnityEngine.Rendering.CoreUtils;

namespace UrpTestbed {

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public sealed class BokehEffect : MonoBehaviour
{
    [SerializeField] float _radius = 0.01f;
    [SerializeField] int _sampleCount = 32;

    public float Radius { get => _radius; set => _radius = value; }
    public int SampleCount { get => _sampleCount; set => _sampleCount = value; }

    [SerializeField, HideInInspector] Shader _shader = null;

    public Material BlitMaterial => _material;

    static class IDs
    {
        public static int Radius = Shader.PropertyToID("_Radius");
        public static int SampleCount = Shader.PropertyToID("_SampleCount");
    }

    Material _material;

    void OnDestroy()
      => CoreUtils.Destroy(_material);

    void LateUpdate()
    {
        if (_material == null)
            _material = CoreUtils.CreateEngineMaterial(_shader);

        _material.SetFloat(IDs.Radius, _radius);
        _material.SetFloat(IDs.SampleCount, _sampleCount);
    }
}

} // namespace UrpTestbed
