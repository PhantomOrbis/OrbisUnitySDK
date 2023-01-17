using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FixedEditorShader : MonoBehaviour
{
#if UNITY_EDITOR
    void OnEnable()
    {
        AllFixed(gameObject);
    }

    public static void AllFixed(GameObject prefab)
    {
        var renderers = prefab.GetComponentsInChildren<Renderer>(true);
        foreach (var renderer in renderers)
        {
            ReplaceShaderForEditor(renderer.sharedMaterials);
        }

        var meshRenderers = prefab.GetComponentsInChildren<MeshRenderer>(true);
        foreach (var renderer in meshRenderers)
        {
            Material material = renderer.sharedMaterial;
            ReplaceShaderForEditor(material);
            renderer.sharedMaterial = material;
        }

        var tmps = prefab.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var tmp in tmps)
        {
            ReplaceShaderForEditor(tmp.material);
            ReplaceShaderForEditor(tmp.materialForRendering);
        }

        var spritesRenderers = prefab.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var spriteRenderer in spritesRenderers)
        {
            ReplaceShaderForEditor(spriteRenderer.sharedMaterials);
        }

        var images = prefab.GetComponentsInChildren<Image>(true);
        foreach (var image in images)
        {
            ReplaceShaderForEditor(image.material);
        }

        var rawImages = prefab.GetComponentsInChildren<RawImage>(true);
        foreach (var image in rawImages)
        {
            ReplaceShaderForEditor(image.material);
        }

        var particleSystemRenderers = prefab.GetComponentsInChildren<ParticleSystemRenderer>(true);
        foreach (var particleSystemRenderer in particleSystemRenderers)
        {
            ReplaceShaderForEditor(particleSystemRenderer.sharedMaterials);
        }

        var particles = prefab.GetComponentsInChildren<ParticleSystem>(true);
        foreach (var particle in particles)
        {
            var renderer = particle.GetComponent<Renderer>();
            if (renderer != null) ReplaceShaderForEditor(renderer.sharedMaterials);
        }

        var terrains = prefab.GetComponentsInChildren<Terrain>(true);
        foreach (var terrani in terrains)
        {
            var renderer = terrani.GetComponent<Terrain>();
            if (renderer != null) ReplaceShaderForEditor(renderer.materialTemplate);
        }
    }

    private static void ReplaceShaderForEditor(Material[] materials)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            ReplaceShaderForEditor(materials[i]);
        }
    }

    private static void ReplaceShaderForEditor(Material material)
    {
        if (material == null) return;

        var shaderName = material.shader.name;
        var shader = Shader.Find(shaderName);

        if (shader != null) material.shader = shader;
    }
#endif
}
