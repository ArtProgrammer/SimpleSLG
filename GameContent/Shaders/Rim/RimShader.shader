// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/RimShader"
{
	Properties{
		_MainTex("Main Tex", 2D) = "white" {}
		_Color("Main Color",color) = (1,1,1,1)//物体的颜色
		_Outline("Thick of Outline",range(0,0.1)) = 0.02//挤出描边的粗细
		_Factor("Factor",range(0,1)) = 0.5//挤出多远
		_ToonEffect("Toon Effect",range(0,1)) = 0.5//卡通化程度（二次元与三次元的交界线）
		_Steps("Steps of toon",range(0,9)) = 3//色阶层数
	}
		SubShader{
			pass {//处理光照前的pass渲染
				Tags{"LightMode" = "Always"}
				Cull Front
				ZWrite On
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				float _Outline;
				float _Factor;
				sampler2D _MainTex;

				struct v2f {
					float4 pos:SV_POSITION;
					float2 uv:TEXCOORD0;
				};

				v2f vert(appdata_full v) {
					v2f o;
					float3 dir = normalize(v.vertex.xyz);
					float3 dir2 = v.normal;
					float D = dot(dir,dir2);
					dir = dir * sign(D);
					dir = dir * _Factor + dir2 * (1 - _Factor);
					v.vertex.xyz += dir * _Outline;
					o.pos = UnityObjectToClipPos(v.vertex);

					//o.uv = v.uv;
					return o;
				}
				float4 frag(v2f i) :COLOR
				{
					float4 c = 0;
					return c;
				}
				ENDCG
			}//end of pass
			pass {//平行光的的pass渲染
				Tags{"LightMode" = "ForwardBase"}
				Cull Back
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				float4 _LightColor0;
				float4 _Color;
				float _Steps;
				float _ToonEffect;

				struct v2f {
					float4 pos:SV_POSITION;
					float3 lightDir:TEXCOORD0;
					float3 viewDir:TEXCOORD1;
					float3 normal:TEXCOORD2;
				};

				v2f vert(appdata_full v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);//切换到世界坐标
					o.normal = v.normal;
					o.lightDir = ObjSpaceLightDir(v.vertex);
					o.viewDir = ObjSpaceViewDir(v.vertex);

					return o;
				}
				float4 frag(v2f i) :COLOR
				{
					float4 c = 1;
					float3 N = normalize(i.normal);
					float3 viewDir = normalize(i.viewDir);
					float3 lightDir = normalize(i.lightDir);
					float diff = max(0,dot(N,i.lightDir));//求出正常的漫反射颜色
					diff = (diff + 1) / 2;//做亮化处理
					diff = smoothstep(0,1,diff);//使颜色平滑的在[0,1]范围之内
					float toon = floor(diff * _Steps) / _Steps;//把颜色做离散化处理，把diffuse颜色限制在_Steps种（_Steps阶颜色），简化颜色，这样的处理使色阶间能平滑的显示
					diff = lerp(diff,toon,_ToonEffect);//根据外部我们可控的卡通化程度值_ToonEffect，调节卡通与现实的比重

					c = _Color * _LightColor0 * (diff);//把最终颜色混合
					return c;
				}
				ENDCG
			}//
			pass {//附加点光源的pass渲染
				Tags{"LightMode" = "ForwardAdd"}
				Blend One One
				Cull Back
				ZWrite Off
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				float4 _LightColor0;
				float4 _Color;
				float _Steps;
				float _ToonEffect;

				struct v2f {
					float4 pos:SV_POSITION;
					float3 lightDir:TEXCOORD0;
					float3 viewDir:TEXCOORD1;
					float3 normal:TEXCOORD2;
				};

				v2f vert(appdata_full v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.normal = v.normal;
					o.viewDir = ObjSpaceViewDir(v.vertex);
					o.lightDir = _WorldSpaceLightPos0 - v.vertex;

					return o;
				}
				float4 frag(v2f i) :COLOR
				{
					float4 c = 1;
					float3 N = normalize(i.normal);
					float3 viewDir = normalize(i.viewDir);
					float dist = length(i.lightDir);//求出距离光源的距离
					float3 lightDir = normalize(i.lightDir);
					float diff = max(0,dot(N,i.lightDir));
					diff = (diff + 1) / 2;
					diff = smoothstep(0,1,diff);
					float atten = 1 / (dist);//根据距光源的距离求出衰减
					float toon = floor(diff * atten * _Steps) / _Steps;
					diff = lerp(diff,toon,_ToonEffect);

					half3 h = normalize(lightDir + viewDir);//求出半角向量
					float nh = max(0, dot(N, h));
					float spec = pow(nh, 32.0);//求出高光强度
					float toonSpec = floor(spec * atten * 2) / 2;//把高光也离散化
					spec = lerp(spec,toonSpec,_ToonEffect);//调节卡通与现实高光的比重


					c = _Color * _LightColor0 * (diff + spec);//求出最终颜色
					return c;
				}
				ENDCG
			}//
	}
    FallBack "Diffuse"
}
