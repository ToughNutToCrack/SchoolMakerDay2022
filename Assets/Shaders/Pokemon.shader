Shader "TNTC/Pokemon"{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [HDR]_VFXColor ("Color for Captured VFX", Color) = (1,1,1,1)
        _Weight ("VFX Weight", Range(0,1)) = 0
    }
    SubShader{
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf WrapLambert fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input{
            float2 uv_MainTex;
        };


        fixed4 _Color;

        half _Weight;
        fixed4 _VFXColor;

        half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = dot (s.Normal, lightDir);
            half diff = NdotL * 0.5 + 0.5;
            half4 c;
            c.rgb = lerp(s.Albedo * _LightColor0.rgb * (diff * atten), _VFXColor, _Weight);
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o){
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
