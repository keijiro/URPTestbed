using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UrpTestbed {

sealed class PostEffectPass : ScriptableRenderPass
{
    public override void Execute
      (ScriptableRenderContext context, ref RenderingData data)
    {
        if (!Application.isPlaying) return;

        var fx = data.cameraData.camera.GetComponent<PostEffect>();
        if (fx == null || !fx.enabled) return;

        var cmd = CommandBufferPool.Get("PostEffect");
        Blit(cmd, ref data, fx.BlitMaterial, 0);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}

public sealed class PostEffectFeature : ScriptableRendererFeature
{
    PostEffectPass _pass;

    public override void Create()
      => _pass = new PostEffectPass
           { renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing };

    public override void AddRenderPasses
      (ScriptableRenderer renderer, ref RenderingData data)
      => renderer.EnqueuePass(_pass);
}

} // namespace UrpTestbed
