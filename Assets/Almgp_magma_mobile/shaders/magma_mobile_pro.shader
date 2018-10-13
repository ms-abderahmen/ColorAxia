// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.10 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.10;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,nrsp:0,limd:0,spmd:1,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.3647059,fgcg:0.2941177,fgcb:0.2431373,fgca:1,fgde:0.1,fgrn:0,fgrf:132,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7369,x:33590,y:32399,varname:node_7369,prsc:2|emission-8383-OUT;n:type:ShaderForge.SFN_Tex2d,id:8042,x:32451,y:32901,varname:_mask,prsc:2,tex:7cd3ddadc1cd7fc478d34f98274ef318,ntxv:0,isnm:False|UVIN-9767-OUT,TEX-7358-TEX;n:type:ShaderForge.SFN_Tex2d,id:6382,x:32944,y:32541,ptovrint:False,ptlb:gradient,ptin:_gradient,varname:_gradient,prsc:2,tex:ea0c8943ad55dc748ab007ee624420d7,ntxv:0,isnm:False|UVIN-4279-OUT;n:type:ShaderForge.SFN_Time,id:6665,x:31740,y:32719,varname:node_6665,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6814,x:31892,y:32545,varname:node_6814,prsc:2|A-6665-TSL,B-5846-OUT;n:type:ShaderForge.SFN_Slider,id:5846,x:31771,y:33061,ptovrint:False,ptlb:speed,ptin:_speed,varname:_speed,prsc:2,min:-5,cur:1,max:5;n:type:ShaderForge.SFN_Frac,id:7969,x:32184,y:32543,varname:node_7969,prsc:2|IN-6814-OUT;n:type:ShaderForge.SFN_Add,id:9767,x:32426,y:32553,varname:node_9767,prsc:2|A-9262-UVOUT,B-7969-OUT;n:type:ShaderForge.SFN_TexCoord,id:9262,x:32229,y:32490,varname:node_9262,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:9995,x:32540,y:32796,varname:node_9995,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:4279,x:32925,y:32804,varname:node_4279,prsc:2|A-9995-OUT,B-3507-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:9398,x:32744,y:32693,varname:node_9398,prsc:2,min:0.1,max:0.8|IN-3370-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:7358,x:32084,y:33307,ptovrint:False,ptlb:height,ptin:_height,varname:_height,tex:7cd3ddadc1cd7fc478d34f98274ef318,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7853,x:32431,y:33093,varname:_node_7853,prsc:2,tex:7cd3ddadc1cd7fc478d34f98274ef318,ntxv:0,isnm:False|UVIN-1233-OUT,TEX-7358-TEX;n:type:ShaderForge.SFN_OneMinus,id:7384,x:32118,y:33044,varname:node_7384,prsc:2|IN-7816-OUT;n:type:ShaderForge.SFN_Add,id:1233,x:32321,y:33140,varname:node_1233,prsc:2|A-7384-OUT,B-9262-UVOUT;n:type:ShaderForge.SFN_Blend,id:3370,x:32615,y:32901,varname:node_3370,prsc:2,blmd:10,clmp:False|SRC-8042-R,DST-7853-A;n:type:ShaderForge.SFN_Multiply,id:1645,x:31982,y:32719,varname:node_1645,prsc:2|A-6665-TSL,B-3765-OUT,C-5846-OUT;n:type:ShaderForge.SFN_Frac,id:7816,x:32169,y:32851,varname:node_7816,prsc:2|IN-1645-OUT;n:type:ShaderForge.SFN_Vector1,id:3765,x:31663,y:32981,varname:node_3765,prsc:2,v1:0.9;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3507,x:32847,y:33137,varname:node_3507,prsc:2|IN-3370-OUT,IMIN-4337-OUT,IMAX-2322-OUT,OMIN-4794-OUT,OMAX-9771-OUT;n:type:ShaderForge.SFN_Vector1,id:4337,x:32658,y:33146,varname:node_4337,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:2322,x:32588,y:33264,varname:node_2322,prsc:2,v1:1.3;n:type:ShaderForge.SFN_Slider,id:4794,x:32590,y:33372,ptovrint:False,ptlb:oMin,ptin:_oMin,varname:_oMin,prsc:2,min:0.05,cur:0.05,max:0.4;n:type:ShaderForge.SFN_Slider,id:9771,x:32898,y:33393,ptovrint:False,ptlb:oMax,ptin:_oMax,varname:_oMax,prsc:2,min:0.5,cur:0.5,max:0.99;n:type:ShaderForge.SFN_Fresnel,id:8478,x:32689,y:32218,varname:node_8478,prsc:2|NRM-2523-OUT,EXP-8820-OUT;n:type:ShaderForge.SFN_NormalVector,id:2523,x:32418,y:32159,prsc:2,pt:False;n:type:ShaderForge.SFN_Color,id:9323,x:32592,y:31972,ptovrint:False,ptlb:Rim color,ptin:_Rimcolor,varname:_Rimcolor,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:9372,x:32887,y:32044,varname:node_9372,prsc:2|A-7524-OUT,B-8478-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:8383,x:33401,y:32462,ptovrint:False,ptlb:use RIM,ptin:_useRIM,varname:_useRIM,prsc:2,on:False|A-6382-RGB,B-4634-OUT;n:type:ShaderForge.SFN_Slider,id:8820,x:32598,y:32450,ptovrint:False,ptlb:exp,ptin:_exp,varname:_exp,prsc:2,min:0.5,cur:1.033779,max:5;n:type:ShaderForge.SFN_Add,id:4634,x:33103,y:32265,varname:node_4634,prsc:2|A-9372-OUT,B-6382-RGB;n:type:ShaderForge.SFN_Multiply,id:7524,x:32780,y:31909,varname:node_7524,prsc:2|A-5972-OUT,B-9323-RGB;n:type:ShaderForge.SFN_ValueProperty,id:5972,x:32633,y:31812,ptovrint:False,ptlb:power,ptin:_power,varname:_power,prsc:2,glob:False,v1:2;proporder:6382-5846-7358-4794-9771-9323-8383-8820-5972;pass:END;sub:END;*/

Shader "Almgp/mobile/magma_mobile_pro" {
    Properties {
        _gradient ("gradient", 2D) = "white" {}
        _speed ("speed", Range(-5, 5)) = 1
        _height ("height", 2D) = "white" {}
        _oMin ("oMin", Range(0.05, 0.4)) = 0.05
        _oMax ("oMax", Range(0.5, 0.99)) = 0.5
        _Rimcolor ("Rim color", Color) = (0.5,0.5,0.5,1)
        [MaterialToggle] _useRIM ("use RIM", Float ) = 1
        _exp ("exp", Range(0.5, 5)) = 1.033779
        _power ("power", Float ) = 2
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
            uniform sampler2D _gradient; uniform float4 _gradient_ST;
            uniform float _speed;
            uniform sampler2D _height; uniform float4 _height_ST;
            uniform float _oMin;
            uniform float _oMax;
            uniform float4 _Rimcolor;
            uniform fixed _useRIM;
            uniform float _exp;
            uniform float _power;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_6665 = _Time + _TimeEditor;
                float2 node_9767 = (i.uv0+frac((node_6665.r*_speed)));
                float4 _mask = tex2D(_height,TRANSFORM_TEX(node_9767, _height));
                float2 node_1233 = ((1.0 - frac((node_6665.r*0.9*_speed)))+i.uv0);
                float4 _node_7853 = tex2D(_height,TRANSFORM_TEX(node_1233, _height));
                float node_3370 = ( _node_7853.a > 0.5 ? (1.0-(1.0-2.0*(_node_7853.a-0.5))*(1.0-_mask.r)) : (2.0*_node_7853.a*_mask.r) );
                float node_4337 = 0.0;
                float2 node_4279 = float2(0.0,(_oMin + ( (node_3370 - node_4337) * (_oMax - _oMin) ) / (1.3 - node_4337)));
                float4 _gradient_var = tex2D(_gradient,TRANSFORM_TEX(node_4279, _gradient));
                float3 emissive = lerp( _gradient_var.rgb, (((_power*_Rimcolor.rgb)*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_exp))+_gradient_var.rgb), _useRIM );
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
