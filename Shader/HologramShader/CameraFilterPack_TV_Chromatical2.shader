////////////////////////////////////////////
// CameraFilterPack - by VETASOFT 2016 /////
////////////////////////////////////////////


Shader "CameraFilterPack/TV_Chromatical2" { 
Properties 
{
_MainTex ("Base (RGB)", 2D) = "white" {}
_TimeX ("Time", Range(0.0, 1.0)) = 1.0
_ScreenResolution ("_ScreenResolution", Vector) = (0.,0.,0.,0.)
}
SubShader
{
Pass
{
ZTest Always
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#pragma glsl
#include "UnityCG.cginc"
uniform sampler2D _MainTex;
uniform float _TimeX;
uniform float _Value;
uniform float4 _ScreenResolution;
struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};
struct v2f
{
half2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
fixed4 color    : COLOR;
};
v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}
float4 frag (v2f i) : COLOR
{
float2 q = i.texcoord.xy;
float2 uv = q;

float Effect = abs(uv.x - 0.5) * _Value;
//float3 aberration = float3(0, -0.1, -0.2);	// R, G, B 각각에 대한 shift 양
//aberration *= Effect;
float3 col;

// Galaxy Tab S2 9.7 세로형
// 일단 잘 나오는 것
// col.r = tex2D(_MainTex, float2((uv.x*0.980 + 0.02), uv.y)).x;
// col.g = tex2D(_MainTex, float2((uv.x*0.990 + 0.01), uv.y)).y;
// col.b = tex2D(_MainTex, float2((uv.x*1.000 + 0.00), uv.y)).z;

// Galaxy Tab S2 9.7 세로형 - 윈도우 기반
// 수치 추후 확인 필요
col.r = tex2D(_MainTex, float2((uv.x*0.990), uv.y)).x;
col.g = tex2D(_MainTex, float2((uv.x*0.995), uv.y)).y;
col.b = tex2D(_MainTex, float2((uv.x*1.000), uv.y)).z;


// Galaxy Tab S2 9.7 세로형 - 윈도우 기반
// 수치 추후 확인 필요
// col.r = tex2D(_MainTex, float2(uv.x, (uv.y*0.980 + 0.02))).x;
// col.g = tex2D(_MainTex, float2(uv.x, (uv.y*0.990 + 0.01))).y;
// col.b = tex2D(_MainTex, float2(uv.x, (uv.y*1.000 + 0.00))).z;


return  float4(col,1.0);

}
ENDCG
}
}
}
