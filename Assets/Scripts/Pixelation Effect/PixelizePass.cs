using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizePass : ScriptableRenderPass
{
    private readonly PixelizeFeature.CustomPassSettings settings;

    private RenderTargetIdentifier colorBuffer, pixelBuffer;
    private readonly int pixelBufferID = Shader.PropertyToID("_PixelBuffer");

    //private RenderTargetIdentifier pointBuffer;
    //private int pointBufferID = Shader.PropertyToID("_PointBuffer");

    private readonly Material material;
    private int pixelScreenHeight, pixelScreenWidth;
    private static readonly int blockCount = Shader.PropertyToID("_BlockCount");
    private static readonly int blockSize = Shader.PropertyToID("_BlockSize");
    private static readonly int halfBlockSize = Shader.PropertyToID("_HalfBlockSize");

    public PixelizePass(PixelizeFeature.CustomPassSettings settings)
    {
        this.settings = settings;
        renderPassEvent = settings.RenderPassEvent;
        if (material == null) material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize");
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
#pragma warning disable CS0618
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
#pragma warning restore CS0618
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

        //cmd.GetTemporaryRT(pointBufferID, descriptor.width, descriptor.height, 0, FilterMode.Point);
        //pointBuffer = new RenderTargetIdentifier(pointBufferID);

        pixelScreenHeight = settings.ScreenHeight;
        pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

        material.SetVector(blockCount, new Vector2(pixelScreenWidth, pixelScreenHeight));
        material.SetVector(blockSize, new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
        material.SetVector(halfBlockSize, new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

        descriptor.height = pixelScreenHeight;
        descriptor.width = pixelScreenWidth;

        cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
        pixelBuffer = new(pixelBufferID);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, new("Pixelize Pass")))
        {
            // No-shader variant
            //Blit(cmd, colorBuffer, pointBuffer);
            //Blit(cmd, pointBuffer, pixelBuffer);
            //Blit(cmd, pixelBuffer, colorBuffer);

#pragma warning disable CS0618
            Blit(cmd, colorBuffer, pixelBuffer, material);
            Blit(cmd, pixelBuffer, colorBuffer);
#pragma warning restore CS0618
        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new ArgumentNullException(nameof(cmd));
        cmd.ReleaseTemporaryRT(pixelBufferID);
        //cmd.ReleaseTemporaryRT(pointBufferID);
    }
}