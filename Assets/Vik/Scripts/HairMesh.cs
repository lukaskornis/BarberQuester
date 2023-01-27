using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class HairMesh : MonoBehaviour
{
    [Header("Hair")]
    public Vector2 hairLength = new Vector2(0, 0.3f);
    [Header("Noise")]
    public float noiseScale = 0.1f;

    public Vector2 lengthRange = new Vector2(0.5f, 0.7f);
    public Color hairColor;
    
    Vector3[] _vertices;
    Vector3[] _startVertices;
    Vector3[] _normals;
    float[] _values;
    Color[] _colors;

    Mesh _mesh;
    MeshFilter _mf;
    MeshCollider _mc;
    
    
    private void Start()
    {
        InitMesh();
        SetRandomValues();
        UpdateVertices();
        ApplyMesh();
    }
    

    void InitMesh()
    {
        _mf = GetComponent<MeshFilter>();
        _mc = GetComponent<MeshCollider>();
        
        _mesh = _mf.mesh;
        _startVertices = _mesh.vertices;
        _vertices = _mesh.vertices;
        _normals = _mesh.normals;
        _colors = _mesh.colors;
        
        _values = new float[_vertices.Length];

        for (int i = 0; i < _vertices.Length; i++)
        {
            _values[i] = 0;
            _colors[i] = new Color(hairColor.r, hairColor.g, hairColor.b, _colors[i].a);
            
        }
        _mesh.colors = _colors;
    }
    
    void SetRandomValues()
    {
        for (int i = 0; i < _vertices.Length; i++)
        {
            var pos = transform.TransformPoint(_vertices[i]);
            var n  = noise.snoise(pos * noiseScale);
            _values[i] = math.lerp(lengthRange.x, lengthRange.y, n);
        }
    }

    void UpdateVertices()
    {
        for( int i = 0; i < _vertices.Length; i++ )
        {
            if (_colors[i].a < 0.1f) _vertices[i] = _startVertices[i];
            else _vertices[i] = _startVertices[i] + _normals[i] * (_values[i] * hairLength.y * _colors[i].a);
        }
    }


    void ApplyMesh()
    {
        _mesh.SetVertices(_vertices);
        _mesh.SetNormals(_normals);
        _mesh.SetColors(_colors);
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        
        _mc.sharedMesh = _mesh;
    }
    
    public void PaintColor( Vector3 pos, float radius, Color color,float strength )
    {
        for( int i = 0; i < _vertices.Length; i++ )
        {
            
            // falloff
            var v = transform.TransformPoint(_vertices[i]);
            var d = Vector3.Distance(v, pos);
            if( d > radius ) continue;
            
            // smooth falloff
            var f = 1 - d / radius;

            // paint rgb, keep alpha
            var c = color;
            c.a = _colors[i].a;
            _colors[i] = Color.Lerp(_colors[i], c, strength * f);
        }
        
        ApplyMesh();
    }
    
    public void PaintLength( Vector3 pos, float radius, float length,float speed)
    {
        
        for( int i = 0; i < _vertices.Length; i++ )
        {
            // ignore alpha 0
            if (_colors[i].a < 0.1f) continue;
            
            // falloff
            var v = transform.TransformPoint(_vertices[i]);
            var d = Vector3.Distance(v, pos);
            if( d > radius ) continue;
            
            // smooth falloff
            var f = 1 - d / radius;
            
            _values[i] = Mathf.MoveTowards( _values[i], length, speed * f * Time.deltaTime);
        }
        
        UpdateVertices();
        ApplyMesh();
    }
}
