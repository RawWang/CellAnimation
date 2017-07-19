Shader "Custom/CellPanel" {

	Properties{
		[PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
		[PerRendererData]_Color("Tint", Color) = (1,1,1,1)
		[PerRendererData]_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		[PerRendererData]_StencilOp("Stencil Operation", Float) = 0
		[PerRendererData]_StencilWriteMask("Stencil Write Mask", Float) = 255
		[PerRendererData]_StencilReadMask("Stencil Read Mask", Float) = 255
		[PerRendererData]_ColorMask("Color Mask", Float) = 15
		_Width("Width", Range(0, 1.0)) = 1.0
		_Height("Height", Range(0, 1.0)) = 1.0
	}

	SubShader{

		Tags{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}


		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]


		Pass{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag


			#include "UnityCG.cginc"
			#include "UnityUI.cginc"


			struct appdata_t
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};


			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
			};


			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float _Width;
			float _Height;


			v2f vert(appdata_t IN){
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				return OUT;
			}


			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target{

				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                float2 uv = IN.texcoord.xy;

                float areaA = (_Width * _Height) / 2;

                float areaB = (uv.x * (_Height - uv.y)) / 2;

                float areaC = (uv.x * uv.y);

                float areaD = ((_Width - uv.x) * uv.y) / 2; 
               
                //左下角
                if (uv.y < _Height && uv.x < _Width)
                {
                	if(areaB + areaC + areaD < areaA)
                  		color.a = 0;
                }

                clip(color.a - 0.01);

                return color;

			}

			ENDCG
		}
	}

	FallBack "Diffuse"
}