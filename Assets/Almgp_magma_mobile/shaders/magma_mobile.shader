// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.10 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.10;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,nrsp:0,limd:0,spmd:1,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.3647059,fgcg:0.2941177,fgcb:0.2431373,fgca:1,fgde:0.1,fgrn:0,fgrf:132,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6006,x:33340,y:32600,varname:node_6006,prsc:2|emission-9306-RGB;n:type:ShaderForge.SFN_Frac,id:681,x:32733,y:32662,varname:node_681,prsc:2|IN-7148-OUT;n:type:ShaderForge.SFN_Time,id:4296,x:32343,y:32548,varname:node_4296,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7148,x:32548,y:32662,varname:node_7148,prsc:2|A-4296-TSL,B-6812-OUT;n:type:ShaderForge.SFN_Slider,id:6812,x:32445,y:32900,ptovrint:False,ptlb:speed,ptin:_speed,varname:_speed,prsc:2,min:-5,cur:1,max:5;n:type:ShaderForge.SFN_TexCoord,id:3083,x:32733,y:32489,varname:node_3083,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:7175,x:32911,y:32549,varname:node_7175,prsc:2|A-3083-UVOUT,B-681-OUT;n:type:ShaderForge.SFN_Tex2d,id:9306,x:33095,y:32719,ptovrint:False,ptlb:texture,ptin:_texture,varname:_texture,prsc:2,tex:24395f129ba9f054da44ca7a286f0ada,ntxv:0,isnm:False|UVIN-7175-OUT;proporder:6812-9306;pass:END;sub:END;*/

Shader "Almgp/mobile/magma_mobile" {
    Properties {
        _speed ("speed", Range(-5, 5)) = 1
        _texture ("texture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _speed;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_4296 = _Time + _TimeEditor;
                float2 node_7175 = (i.uv0+frac((node_4296.r*_speed)));
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_7175, _texture));
                float3 emissive = _texture_var.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
