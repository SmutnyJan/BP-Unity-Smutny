using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static FullScreenShaderManager;

public class FullScreenShaderManager : MonoBehaviour
{
    public static FullScreenShaderManager Instance;

    [SerializeField]
    public List<ShaderEntry> ShaderList;

    private Dictionary<FullScreenShader, Material> _fullScreenShaders;


    private string _featureName = "FullScreenPassRendererFeature";
    private UniversalRenderPipelineAsset _universalRenderPipelineAsset;
    private FullScreenPassRendererFeature _feature;

    public enum FullScreenShader
    {
        None,
        Pearl,
        Freezing,
        Magic,
        NoneForced
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        _fullScreenShaders = new Dictionary<FullScreenShader, Material>();

        foreach (ShaderEntry entry in ShaderList)
        {
            _fullScreenShaders.Add(entry.shader, entry.material);
        }

        _universalRenderPipelineAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        _feature = _universalRenderPipelineAsset.rendererDataList[0].rendererFeatures
            .FirstOrDefault(f => f != null && f.name == _featureName) as FullScreenPassRendererFeature;

        _feature.passMaterial = _fullScreenShaders[FullScreenShader.None];


    }

    public void SwitchToShader(FullScreenShader fullScreenShader)
    {
        if (fullScreenShader == FullScreenShader.NoneForced)
        {
            _feature.passMaterial = _fullScreenShaders[FullScreenShader.None];
            return;
        }

        if (_feature.passMaterial == _fullScreenShaders[FullScreenShader.Freezing] && SeasonsManager.Instance.CurrentSeason == SeasonsManager.Season.Winter)
        {
            return;
        }

        if (fullScreenShader == FullScreenShader.None)
        {
            _feature.passMaterial = _fullScreenShaders[fullScreenShader];
            return;
        }


        switch (fullScreenShader)
        {
            case FullScreenShader.Pearl:
                _feature.passMaterial = _fullScreenShaders[fullScreenShader];
                break;
            case FullScreenShader.Freezing:
                _feature.passMaterial = _fullScreenShaders[fullScreenShader];
                break;
            case FullScreenShader.Magic:
                _feature.passMaterial = _fullScreenShaders[fullScreenShader];
                break;
        }
    }
}

[Serializable]
public class ShaderEntry
{
    public FullScreenShader shader;
    public Material material;
}
