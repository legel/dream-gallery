
Shader "Gogh/EdgeDetect" { 
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE
	
	#include "UnityCG.cginc"
	

	sampler2D _MainTex;
	uniform float4 _MainTex_TexelSize;

	sampler2D _CameraDepthNormalsTexture;
	sampler2D_float _CameraDepthTexture;

	uniform half4 _BgColor;
	uniform half _SampleDistance;
	uniform float _Exponent;

	uniform float _Threshold;

	struct v2flum {
		float4 pos : SV_POSITION;
		float2 uv[3] : TEXCOORD0;
	};

	v2flum vertLum (appdata_img v)
	{
		v2flum o;
		o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
		float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
		o.uv[0] = uv;
		o.uv[1] = uv + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y) * _SampleDistance;
		o.uv[2] = uv + float2(+_MainTex_TexelSize.x, -_MainTex_TexelSize.y) * _SampleDistance;
		return o;
	}


	fixed4 fragLum (v2flum i) : SV_Target
	{
		fixed4 original = tex2D(_MainTex, i.uv[0]);

		// a very simple cross gradient filter

		half3 p1 = original.rgb;
		half3 p2 = tex2D(_MainTex, i.uv[1]).rgb;
		half3 p3 = tex2D(_MainTex, i.uv[2]).rgb;
		
		half3 diff = p1 * 2 - p2 - p3;
		half len = dot(diff, diff);
		len = step(len, _Threshold);

		return len;			
	}	
	
	
		
	ENDCG 
	
Subshader {
 Pass {
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }   

      CGPROGRAM
	  #pragma target 3.0   
      #pragma vertex vertLum
      #pragma fragment fragLum
      ENDCG
  }
}

Fallback off
	
} // shader