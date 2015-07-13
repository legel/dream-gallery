Shader "Gogh/SignedDistanceField" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass{
		CGPROGRAM
	#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

	uniform sampler2D _MainTex;
//	struct vertexInput {
//   		float3 local     : TEXCOORD0;  
//  		float3 triangle  : TEXCOORD1;  
//  		float3 nv0       : TEXCOORD2;  
// 		float3 nv1       : TEXCOORD3;  
//  		float3 nv2       : TEXCOORD4; 
//   		float3 ne0       : TEXCOORD5;  
//   		float3 ne1       : TEXCOORD6;  
//   		float3 ne2       : TEXCOORD7; 
//	};
  	struct vertexOutput {
   		float4 dist : COLOR;  
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
   	//	float  absDist  : DEPTH;
 	};
 	
  //	uniform float narrowbandSize;
  
  	vertexOutput vert(appdata_full v){
  		
		vertexOutput o;
		o.pos=mul(UNITY_MATRIX_MVP,v.vertex);
		float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
		o.uv = uv;
		
		
  		 // Copy to temoraries.  
  		float   a = v.vertex.x;  
  		float   b = v.vertex.y;  
  		float   h = v.vertex.z;  
  		float   r = local.x;  
  		float   s = local.y;  
  		float   t = local.z;  
  		float3 nv = nv1;  
  		float3 ne = ne1; 
  		// Normalize to half-space r >= 0.  
   		if (r < 0) {  
    		r = -r;  
    		a =  b;  
    		nv = nv0;  
    		ne = ne2;  
    	}
		// Transform to the primed coordinate frame.  
   		float lensqr = (a * a + h * h);  
		float rprime = (a * r + h * h - h * s) / lensqr;  
		float sprime = (a * s + h * r - h * a) / lensqr; 
		// Case analysis 
   		// Default to region I  
  		float3 c = float3(0, 0, 0);  
		float3 n = float3(0, 0, 1);
		if (s < 0) {    
  			// Region III or II  
  			c.x = a;  
  			n = (r > a) ? nv : ne0;  
		}
		else if (sprime > 0) {  
  			if (rprime < 0) {  
    			// Region VI  
    			c.y = h;  
    			n = nv2;  
  			}  
  			else {  
    			// Region IV or V  
    			c.x = a;  
    			n = (rprime > 1) ? nv : ne;  
  			}  
		}  
		// IV, V, VI  
		rprime = max(max(- rprime,0), rprime - 1);  
		// I, V  
		sprime = max(sprime,0);  
		// II, III  
		r = max(r-a,0);  
		// Compute the distance.  
   		float tmp = (s < 0) ? (r * r + s * s) : ((rprime * rprime + sprime * sprime) * lensqr);  
		absDist = sqrt(tmp + t * t);  
		// Compute the sign.  
   		float sign_tst = sign(dot(n, local - c));  
		dist = float4(sign_tst * absDist, local);  
		// Depth buffer is clamped to 0..1, so we rescale.  
		absDist /= narrowbandSize; 
		return o;
	}    
	fixed4 frag (vertexOutput i) : SV_Target
	{
		fixed4 original = tex2D(_MainTex, i.uv);
	
		return original;			
	}	
		ENDCG
		}
	} 
	FallBack "Diffuse"
}
