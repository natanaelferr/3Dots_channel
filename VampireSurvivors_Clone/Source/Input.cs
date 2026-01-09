using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;

public class Input
{

    public KeyboardState keyboardCurrent;
    public MouseState mouseCurrent;

    public Input()
    {

    }

    public void Update(float deltaTime)
    {
        keyboardCurrent = Keyboard.GetState();
        mouseCurrent = Mouse.GetState();
    }
}