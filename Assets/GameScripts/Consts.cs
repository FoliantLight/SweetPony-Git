using UnityEngine;
using System.Collections;

public class GameConsts {
    public const float PlayerSpeed = 2.0F;
    public const float JumpForce = 250.0F;
    public const float RunSpeedMultiplier = 2.0F;
    //смещение камеры по оси Y при минимальном масштабе
    public const float yDiff = 2.25F;
}

public class Axes {
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
}

public class Buttons {
    public const string Jump = "Jump";
    public const string Run = "Run";
    public const string Use = "Use";
}

public class Layers {
    public const int Player = 8;
    public const int Solid = 9;
    public const int Platforms = 10;
    public const int Triggers = 11;
}

public class Tags {
    public const string Player = "Player";
}