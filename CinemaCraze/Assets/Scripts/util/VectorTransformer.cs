using UnityEngine;
public class VectorTransformer
{
    public static Vector2 ToVec2xz(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.z);
    }

}