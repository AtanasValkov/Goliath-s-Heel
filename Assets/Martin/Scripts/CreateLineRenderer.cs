using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLineRenderer : MonoBehaviour
{
    public Material cableMaterial;
    public Color lineColor;

    public LineRenderer Create()
    {
        LineRenderer renderer = gameObject.AddComponent<LineRenderer>();

        renderer.startWidth = 0.15f;
        renderer.endWidth = 0.15f;
        renderer.startColor = lineColor;
        renderer.endColor = lineColor;
        renderer.numCornerVertices = 10;
        renderer.numCapVertices = 10;
        renderer.material = cableMaterial;

        return renderer;
    }
}
