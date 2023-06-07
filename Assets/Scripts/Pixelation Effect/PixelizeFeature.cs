using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PixelizeFeature : ScriptableRendererFeature
{
    [Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent RenderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public int ScreenHeight = 144;
    }

    [SerializeField] private CustomPassSettings settings;
    private PixelizePass customPass;

    public override void Create() => customPass = new(settings);

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
#if UNITY_EDITOR
        if (renderingData.cameraData.isSceneViewCamera) return;
#endif
        renderer.EnqueuePass(customPass);
    }
}