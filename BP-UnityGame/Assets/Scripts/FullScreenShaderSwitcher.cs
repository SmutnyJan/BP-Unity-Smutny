using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullScreenShaderSwitcher : MonoBehaviour
{
    public TextMeshProUGUI ToggleButtonText;
    public TMP_Dropdown ShaderDropdown;
    public List<Material> FullScreenMaterials;

    private string _featureName = "FullScreenPassRendererFeature";
    private UniversalRenderPipelineAsset _universalRenderPipelineAsset;
    private FullScreenPassRendererFeature _feature;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _universalRenderPipelineAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        _feature = _universalRenderPipelineAsset.rendererDataList[0].rendererFeatures
            .FirstOrDefault(f => f != null && f.name == _featureName) as FullScreenPassRendererFeature;

        _feature.SetActive(false);

        ShaderDropdown.options = FullScreenMaterials
            .Select(material => new TMP_Dropdown.OptionData(material.name))
            .ToList();

        ShaderDropdown.gameObject.SetActive(false);

        ToggleButtonText.text = "Zapnout";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDropDownChanged()
    {
        int selectedIndex = ShaderDropdown.value;

        Material selectedMaterial = FullScreenMaterials[selectedIndex];

        _feature.passMaterial = selectedMaterial;

    }

    public void ToggleFeature()
    {

        _feature.SetActive(!_feature.isActive);

        if (_feature.isActive)
        {
            ToggleButtonText.text = "Vypnout";
            ShaderDropdown.gameObject.SetActive(true);
            OnDropDownChanged();
        }
        else
        {
            ToggleButtonText.text = "Zapnout";
            ShaderDropdown.gameObject.SetActive(false);

        }

        Debug.Log($"Renderer feature '{_feature.name}' je nyní {(_feature.isActive ? "zapnutá" : "vypnutá")}.");
    }
}
