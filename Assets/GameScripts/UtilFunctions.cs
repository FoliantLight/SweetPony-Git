using UnityEngine;

public class ColliderFunctions {
    public static float colliderTop(BoxCollider2D col) {
        return col.transform.position.y + col.offset.y + col.size.y / 2.0F;
    }

    public static float colliderBottom(BoxCollider2D col) {
        return col.transform.position.y + col.offset.y - col.size.y / 2.0F;
    }

    public static float colliderLeft(BoxCollider2D col) {
        return col.transform.position.x + col.offset.x - col.size.x / 2.0F;
    }

    public static float colliderRight(BoxCollider2D col) {
        return col.transform.position.x + col.offset.x + col.size.x / 2.0F;
    }
}
