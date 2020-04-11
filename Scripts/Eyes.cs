using Godot;
using System;

public class Eyes : Sprite
{
    float fadeSpeed = .3f;
    float alpha = 0f;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (SelfModulate.a <= 0f)
        {
            fadeSpeed = Mathf.Abs(fadeSpeed);
        }
        else if (SelfModulate.a >= 1f)
        {
            fadeSpeed = -fadeSpeed;
        }

        alpha += fadeSpeed * delta;

        SelfModulate = new Color(0.6f, 0, 0, alpha);
    }
}
