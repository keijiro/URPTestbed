using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UrpTestbed {

sealed class GlitchEffectPass : ScriptableRenderPass
{
    public override void Execute
      (ScriptableRenderContext context, ref RenderingData data)
    {
        if (!Application.isPlaying) return;

        var fx = data.cameraData.camera.GetComponent<GlitchEffect>();
        if (fx == null || !fx.enabled) return;

        var cmd = CommandBufferPool.Get("GlitchEffect");
        Blit(cmd, ref data, fx.BlitMaterial, 0);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}

public sealed class GlitchEffectFeature : ScriptableRendererFeature
{
    GlitchEffectPass _pass;

    public override void Create()
      => _pass = new GlitchEffectPass
           { renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing };

    public override void AddRenderPasses
      (ScriptableRenderer renderer, ref RenderingData data)
      => renderer.EnqueuePass(_pass);
}

} // namespace UrpTestbed
