using UnityEngine;

public static class LayerMaskUtility
{
    public static bool IsInLayerMask(GameObject gameObject, LayerMask targetLayer)
    {
        return (targetLayer.value & (1 << gameObject.layer)) != 0;
    }
}