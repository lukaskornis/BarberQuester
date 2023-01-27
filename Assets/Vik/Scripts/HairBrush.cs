using UnityEngine;

public enum BrushType
{
    Color,
    Add,
    Set,    
}

public class HairBrush : MonoBehaviour
{
    public HairMesh hair;

    public float radius = 0.1f;
    public float strength = 0.1f;
    public Color color;
    public float maxDistance = 1;         
    public BrushType brushType;
    public LayerMask layerMask;

    Ray _ray;
    RaycastHit _hit;


    public void Use()
    {
        _ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(_ray, out _hit, maxDistance, layerMask))
        {
            var d =  _hit.distance / maxDistance;
            
            var pos = _hit.point;

            if (brushType == BrushType.Color)
                hair.PaintColor(pos, radius, color, strength * d);

            if (brushType == BrushType.Add)
                hair.PaintLength(pos, radius, 1,strength);
            
            if (brushType == BrushType.Set)
                hair.PaintLength(pos, radius, strength,3);
        }
    }
}