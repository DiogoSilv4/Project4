
Shader "MyShaders/DissolvePlus"{
    Properties{  
        _MainColor("MainColor", Color) = (1,1,1,1)  
        _DissolveColorA("Dissolve Color A", Color) = (0,0,0,0)  
        _DissolveColorB("Dissolve Color B", Color) = (1,1,1,1)  
        _MainTex("MainText", 2D) = "white"{}  
        _NoiseText("NoiseText", 2D) = "white"{}  
        _DissolveThreshold("DissolveThreshold", Range(0,1)) = 0  
        _ColorFactorA("ColorFactorA", Range(0,1)) = 0.7  
        _ColorFactorB("ColorFactorB", Range(0,1)) = 0.8  
    }  
      
    CGINCLUDE  
    #include "Lighting.cginc"  
    uniform fixed4 _MainColor;  
    uniform fixed4 _DissolveColorA;  
    uniform fixed4 _DissolveColorB;  
    uniform sampler2D _MainTex;  
    uniform float4 _MainTex_ST;  
    uniform sampler2D _NoiseText;  
    uniform float _DissolveThreshold;  
    uniform float _ColorFactorA;  
    uniform float _ColorFactorB;  
      
    struct v2f  
    {  
        float4 pos : SV_POSITION;  
        float3 worldNormal : TEXCOORD0;  
        float2 uv : TEXCOORD1;  
    };  
      
    v2f vert(appdata_base v)  
    {  
        v2f o;  
        o.pos = UnityObjectToClipPos(v.vertex);  
        o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);  
        o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);  
        return o;  
    }  
      
    fixed4 frag(v2f i) : SV_Target  
    {  
        fixed4 dissolveValue = tex2D(_NoiseText, i.uv);  
        if (dissolveValue.r < _DissolveThreshold){discard;}  

        fixed3 worldNormal = normalize(i.worldNormal);  
        fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);  
        fixed3 lambert = saturate(dot(worldNormal, worldLightDir));  
        fixed3 albedo = lambert * _MainColor.xyz * _LightColor0.xyz + UNITY_LIGHTMODEL_AMBIENT.xyz;  
        fixed3 color = tex2D(_MainTex, i.uv).rgb * albedo;  

        float lerpValue = _DissolveThreshold / dissolveValue.r;  

        if (lerpValue > _ColorFactorA)  
        {  
            if (lerpValue > _ColorFactorB)  
                return _DissolveColorB;  
            return _DissolveColorA;  
        }  
        return fixed4(color, 1);  
    }  
    ENDCG  
      
    SubShader  
    {  
        Tags{ "RenderType" = "Opaque" }  
        Pass  
        {  
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag     
            ENDCG  
        }  
           
    }  
    FallBack "Diffuse"
}  