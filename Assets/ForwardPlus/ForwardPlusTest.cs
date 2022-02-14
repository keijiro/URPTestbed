using UnityEngine;

sealed class ForwardPlusTest : MonoBehaviour
{
    [SerializeField] GameObject _template = null;

    void Start()
    {
        for (var s = 0; s < 8; s++)
        {
            for (var t = 0; t < 4; t++)
            {
                var pos = new Vector3(s - 3.5f, t - 1.5f, 0) * 0.5f;
                pos += _template.transform.position;

                var go = Instantiate(_template, pos, Quaternion.identity);

                var color = Color.HSVToRGB(Random.value, 1, 1);;
                go.GetComponentInChildren<Light>().color = color;
                go.GetComponentInChildren<Renderer>().material.color = color * 1.5f;
            }
        }

        Destroy(_template);
    }
}
