Shader "Custom/StaticFade" {

	Properties{
	  _MainTex("Texture", 2D) = "white" {}
	  _BumpMap("Bumpmap", 2D) = "bump" {}
	  _AmountOfFade("Fade amount", Range(0,10)) = 0
	}
		SubShader{
		  Tags { "RenderType" = "Opaque" }
		  Cull Off
		  CGPROGRAM
		  #pragma surface surf Lambert
		  
		  struct Input 
		  {
			  float2 uv_MainTex;
			  float2 uv_BumpMap;
			  float3 worldPos;
		  };

		  sampler2D _MainTex;
		  sampler2D _BumpMap;
		  float _AmountOfFade;
		  
		  //NOT MY CODE
		  float rand(float2 co) {
			  return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
		  }
		  //END OF NOT MY CODE

		  void surf(Input IN, inout SurfaceOutput o) 
		  {
			  if (_AmountOfFade < 9) 
			  {
				  float2 input = float2 (IN.worldPos.x, IN.worldPos.y);
				  clip(frac(rand(input)) *  _AmountOfFade - 0.8);
			  }
			 
			  
			  
			  o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			  o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		  }

		

		  ENDCG
	}
		Fallback "Diffuse"
}