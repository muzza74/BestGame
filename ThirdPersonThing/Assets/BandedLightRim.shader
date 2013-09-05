Shader "Custom/Banded Light Rim" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_LightCutoff("Maximum distance", Float) = 5.0
		_Steps ("Gradient steps", Float) = 20.0
		_RimColor ("Rim Color", Color) = (0.0,0.0,1,0.0)
      	_RimPower ("Rim Power", Range(8.0,8.0)) = 3.0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf WrapLambert
		
		uniform float _LightCutoff;
		uniform float _Steps;
		
		half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			//atten = step(_LightCutoff, atten) * atten;
			_Steps = int(_Steps);
			atten = atten * NdotL;
			atten = int(atten * _Steps) / float(_Steps);
			atten = atten < 1 - _LightCutoff ? 0 : atten;
			half vMax = (max(max(s.Albedo.r, s.Albedo.g), s.Albedo.b));
			half3 colorAdjust = vMax > 0 ? s.Albedo / vMax : 1;
			half4 c;
			//if(s.Normal.z > 0 && s.Normal.z < 0.15){
			//	atten = 0.5;
			//}
			//c.rgb = _LightColor0.rgb * atten;
			c.rgb = _LightColor0.rgb * atten * colorAdjust;
			c.a = s.Alpha;
			return c;
		}
	
		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};
		
		sampler2D _MainTex;
		half4 _Color;
		float4 _RimColor;
      	float _RimPower;
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _Color;
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          	o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		}
		ENDCG
	}
	Fallback "Diffuse"
}