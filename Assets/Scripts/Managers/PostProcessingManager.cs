using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[ExecuteAlways]
public class PostProcessingManager : MonoBehaviour
{
    [System.Serializable]
    public class PostProcessLayer
    {
        public AnimationCurve WeightOverMentalState;
        public Volume volume;
    }
    [System.Serializable]
    public class ImageLayer
    {
        public AnimationCurve OpacityOverMentalState;
        public Image image;
    }

    public List<PostProcessLayer> PostProcessingLayers;
    public List<ImageLayer> ImageLayers;

    private void Update()
    {
        float mentalState = GameState.Instance.MentalState.State;
        foreach (var layer in PostProcessingLayers)
        {
            layer.volume.weight = layer.WeightOverMentalState.Evaluate(mentalState);
        }
        foreach (var layer in ImageLayers)
        {
            Color col = layer.image.color;
            col.a = layer.OpacityOverMentalState.Evaluate(mentalState);
            layer.image.color = col;
        }
    }
}
