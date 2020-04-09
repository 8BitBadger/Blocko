shader_type canvas_item;

uniform float distanceToPlayer;
uniform bool seen;

void fragment()
{
float finalAlpha;
COLOR = texture(TEXTURE, UV);
if(texture(TEXTURE, UV).a < 0.01)
{
	finalAlpha = texture(TEXTURE, UV).a;
}
else
{
	if(distanceToPlayer >= 0.1)
	{
		finalAlpha = clamp(0.1, 1.0, distanceToPlayer);
	}
	else
	{
		finalAlpha = 0.0;
	}
}
COLOR.a = finalAlpha;
}