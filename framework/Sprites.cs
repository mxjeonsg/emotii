using Raylib_cs;

using System.Numerics;

namespace Emotii.Framework;

// public class ESprite: EADrawable {}


unsafe public class ESpriteSheet: EADrawable {
    private Texture2D texture;
    private bool is_texture_valid = false;
    private Vec2<uint> s_sprite_sz = new Vec2<uint>(0);

    public ulong spriteCount = 1;

    public ESpriteSheet(string path, Vec2<uint> spritesz) {
        if(Raylib.FileExists(ConvertString.strToSbytePtr(path))) {
            this.texture = Raylib.LoadTexture(ConvertString.strToSbytePtr(path));

            if(Raylib.IsTextureValid(this.texture)) {
                this.is_texture_valid = true;
            }

            this.s_sprite_sz = spritesz;

            // Calculate the deducted amount
            // of sprites in the spritesheet.
            this.spriteCount = (ulong) this.texture.Width / spritesz.X;
        }
    }

    public void drawSpriteN(ushort idx, Vec2<uint> at) {
        Vec2<uint> new_sprite_coords = new Vec2<uint>(
            (uint)
            (idx % this.spriteCount) * this.s_sprite_sz.X,

            (uint)
            (idx % this.spriteCount) * this.s_sprite_sz.Y
        );

        Raylib.DrawTextureRec(
            this.texture,
            new Rectangle(
                new_sprite_coords.X,
                new_sprite_coords.X,
                new_sprite_coords.Y,
                new_sprite_coords.Y
            ),
            new Vector2(at.X, at.Y),
            new Colour().raylibColor
        );
    
    }
    public void drawSpriteNF32(ushort idx, Vec2<float> at) {
        Vec2<uint> new_sprite_coords = new Vec2<uint>(
            (uint)
            (idx % this.spriteCount) * this.s_sprite_sz.X,

            (uint)
            (idx % this.spriteCount) * this.s_sprite_sz.Y
        );

        Raylib.DrawTextureRec(
            this.texture,
            new Rectangle(
                new_sprite_coords.X,
                new_sprite_coords.X,
                new_sprite_coords.Y,
                new_sprite_coords.Y
            ),
            new Vector2(at.X, at.Y),
            new Colour().raylibColor
        );
    }

    override public void draw(Vec2<uint> at, Colour? tint) {
        Colour workaround = tint != null ? tint : new Colour();

        Raylib.DrawTextureV(this.texture, new Vector2(at.X, at.Y), workaround.raylibColor);
    }
}