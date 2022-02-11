using UnityEngine;

sealed class ForwardPlusTest : MonoBehaviour
{
    [SerializeField] Light _template = null;

    void Start()
    {
        for (var s = 0; s < 8; s++)
        {
            for (var t = 0; t < 4; t++)
            {
                var pos = new Vector3(s - 3.5f, t - 1.5f, -0.5f) * 2;
                var light = Instantiate(_template, pos, Quaternion.identity);
                light.color = Color.HSVToRGB(Random.value, 1, 1);
            }
        }

        Destroy(_template.gameObject);
    }
}
