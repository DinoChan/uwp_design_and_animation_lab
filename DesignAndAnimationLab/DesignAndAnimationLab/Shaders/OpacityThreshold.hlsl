

#define D2D_INPUT_COUNT 1
#define D2D_INPUT0_COMPLEX

#include "d2d1effecthelpers.hlsli"


float Thresh = 0.5;



D2D_PS_ENTRY(main)
{
    // Look up the original color from the source image.
    float4 color = D2DGetInput(0);
    if (color.a == 0 || color.a == 1 || Thresh == 0)
    {
        return color;
    }
    float4 resultColor = 0;
    float opacity = color.a > Thresh ? 1 : 0;
    if (opacity > 0)
    {
        resultColor.rgb = color.rgb / color.a * opacity;
    }

    resultColor.a = opacity;
    return resultColor;
}
