using UnityEngine;
/// <summary>
/// This class is used to update the sprite attatched to this gameobject
/// </summary>
public class UpdateSprite : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _sprite = default(SpriteRenderer);

    public void FlipSprite(float enemyPos) {
        if (enemyPos < 0f) {
            _sprite.flipX = false;
        }
        else {
            _sprite.flipX = true;
        }

    }
}
