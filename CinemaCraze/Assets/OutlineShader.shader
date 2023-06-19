Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineColor("Outline Color", Color) = (1,1,0,1)
        _OutlineWidth("Outline Width", Range(0, 1)) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
        };

        sampler2D _MainTex;

        uniform float4 _OutlineColor;
        uniform float _OutlineWidth;

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Basisfarbe des Materials
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

            // Überprüfe, ob der Normalenvektor an der Kante liegt
            float3 normal = normalize(IN.worldNormal);
            float edgeFactor = 1 - saturate(dot(normal, float3(0, 0, 1)));

            // Erhöhe die Farbe um die Umrandungsfarbe, abhängig vom Kantenfaktor
            fixed4 outlineColor = _OutlineColor * edgeFactor * _OutlineWidth;
            c.rgb += outlineColor.rgb;

            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}