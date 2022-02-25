void Bokeh_float
  (UnityTexture2D source, float2 uv, float radius, float stepCount,
   out float3 outColor)
{
    float3 acc = 0;
    float total = 0;
    for (uint i = 0; i < stepCount; i++)
    {
        for (uint j = 0; j < stepCount; j++)
        {
            float dx = radius * (i - (stepCount - 1) * 0.5) / stepCount * 2;
            float dy = radius * (j - (stepCount - 1) * 0.5) / stepCount * 2;
            float3 s = tex2D(source, uv + float2(dx, dy)).rgb;
            float w = dx * dx + dy * dy < radius * radius;
            acc += s * w;
            total += w;
        }
    }
    outColor = acc / total;
}
