

#define D2D_INPUT_COUNT 1
#define D2D_INPUT0_COMPLEX

#include "d2d1effecthelpers.hlsli"


float UpperThresh = 0.52;

float LowerThresh = 0.5;


D2D_PS_ENTRY(main)
{
    // Look up the original color from the source image.
    float4 color = D2DGetInput(0);
    if (color.a == 0 || color.a == 1 || LowerThresh == 0)
    {
        return color;
    }

    if (UpperThresh < LowerThresh)
    {
        return color;
    }

    float4 resultColor = 0;
    float opacity = 1;
    if (color.a < LowerThresh)
    {
        opacity = 0;
    }
    if (color.a > LowerThresh && color.a < UpperThresh)
    {
        opacity = (color.a - LowerThresh) / (UpperThresh - LowerThresh);
    }

    if (opacity > 0)
    {
        resultColor.rgb = color.rgb / color.a * opacity;
    }

    resultColor.a = opacity;
    return resultColor;
}