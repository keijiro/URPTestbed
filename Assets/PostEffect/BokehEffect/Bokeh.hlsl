void Bokeh_float
  (UnityTexture2D source, float2 uv,
   float aspect, float radius, float sampleCount,
   out float3 outColor)
{
    float3 acc = 0;
    for (uint i = 0; i < sampleCount; i++)
    {
        float r = sqrt(i / sampleCount) * radius;
        float phi = PI * (1 + sqrt(5)) * i;
        float2 sp = uv + float2(cos(phi), sin(phi) * aspect) * r;
        acc += tex2D(source, sp).rgb;
    }
    outColor = acc / sampleCount;
}
