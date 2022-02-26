using UnityEngine;

namespace UrpTestbed {

[RequireComponent(typeof(Camera))]
public sealed class GlitchEffect : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float _drift;
    [SerializeField, Range(0, 1)] float _jitter;
    [SerializeField, Range(0, 1)] float _jump;
    [SerializeField, Range(0, 1)] float _shake;
    [SerializeField, HideInInspector] Shader _shader = null;

    public float Drift  { get => _drift;  set => _drift  = value; }
    public float Jitter { get => _jitter; set => _jitter = value; }
    public float Jump   { get => _jump;   set => _jump   = value; }
    public float Shake  { get => _shake;  set => _shake  = value; }

    public Material BlitMaterial => _material;

    static class IDs
    {
        public static int Drift = Shader.PropertyToID("_Drift");
        public static int Jitter = Shader.PropertyToID("_Jitter");
        public static int Jump = Shader.PropertyToID("_Jump");
        public static int Seed = Shader.PropertyToID("_Seed");
        public static int Shake = Shader.PropertyToID("_Shake");
    }

    Material _material;
    float _prevTime;
    float _jumpTime;

    void Start()
      => _material = new Material(_shader);

    void LateUpdate()
    {
        // Time parameters update
        var time = Time.time;
        var delta = time - _prevTime;
        _jumpTime += delta * _jump * 11.3f;
        _prevTime = time;

        // Drift parameters (time, displacement)
        var vdrift = new Vector2(time * 606.11f % (Mathf.PI * 2),
                                 _drift * 0.04f);

        // Jitter parameters (threshold, displacement)
        var jv = _jitter;
        var vjitter = new Vector3(Mathf.Max(0, 1.001f - jv * 1.2f),
                                  0.002f + jv * jv * jv * 0.05f);

        // Jump parameters (scroll, displacement)
        var vjump = new Vector2(_jumpTime, _jump);

        // Property update
        _material.SetInt(IDs.Seed, (int)(time * 10000));
        _material.SetVector(IDs.Drift, vdrift);
        _material.SetVector(IDs.Jitter, vjitter);
        _material.SetVector(IDs.Jump, vjump);
        _material.SetFloat(IDs.Shake, _shake * 0.2f);
    }
}

} // namespace UrpTestbed
