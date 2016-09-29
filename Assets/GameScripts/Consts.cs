public static class GameConsts {
    public const float PlayerSpeed = 2.0F;
    public const float JumpForce = 250.0F;
    public const float RunSpeedMultiplier = 2.0F;
    //смещение камеры по оси Y при минимальном масштабе
    public const float yDiff = 2.25F;
    public static readonly Vector2Int inventorySize = new Vector2Int(5, 8);
}

public static class Axes {
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
}

public static class Buttons {
    public const string Jump = "Jump";
    public const string Run = "Run";
    public const string Use = "Use";
    public const string Inventory = "Inventory";
}

public static class Layers {
    public const int Default = 0;

    public const int Player = 8;
    public const int Solid = 9;
    public const int Platforms = 10;
    public const int Triggers = 11;
    public const int Items = 12;
}

public static class SortingLayers {
    public const string Default = "Default";

    public const string Background = "Background";
    public const string OtherEnvironment = "OtherEnvironment";
    public const string InsideHouse = "InsideHouse";
    public const string FrontHouse = "FrontHouse";
    public const string AdditionalFrontHouse = "AdditionalFrontHouse";
    public const string Characters = "Characters";
    public const string Items = "Items";
    public const string Player = "Player";
    public const string Foreground = "Foreground";
}

public static class Tags {
    public const string Player = "Player";
    public const string Platform = "Platform";
}

public static class Inventories {
    public const int PlayerInventory = 0;
    public const int OthersInventory = 1;
}

public static class ObjectNames {
    public const string InventoryCanvas = "InventoryCanvas";
    public const string UICanvas = "UICanvas";
}

public static class Paths {
    public const string PrefabPath = "InventoryRes/Prefabs/";
    public const string IconPath = "InventoryRes/Icons/";
}