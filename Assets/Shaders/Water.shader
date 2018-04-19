Shader "Unlit/Water"
{
    Properties {
        _MainTex ("Texture to blend", 2D) = "White" {}
    }
    SubShader {
        Tags { "Queue" = "Transparent" }
        Pass {
            Blend OneMinusDstColor One
            SetTexture [_MainTex] { combine texture }
        }
    }
}