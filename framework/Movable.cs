using Raylib_cs;
using System.Numerics;

namespace Emotii.Framework;

public abstract class EAMovable {
    protected Vec2<float> coords = new Vec2<float>(1f, 1f);
    protected Vec2<float> old_coords = new Vec2<float>(1f, 1f);
    protected float threshold = .0025f;

    protected bool movable_by_keyboard = false;

    public Vec2<float> oldCoords {
        get { return this.old_coords; }
    }

    protected KeyboardKey[] mvmt_keys = new KeyboardKey[4] {
        KeyboardKey.W, // up
        KeyboardKey.A, // left
        KeyboardKey.D, // rightp
        KeyboardKey.S // down
    };

    public void moveByKeyboard(bool to, KeyboardKey[] map, float by) {
        if(to) this.movable_by_keyboard = true;
        else this.movable_by_keyboard = false;

        this.mvmt_keys = map;
        this.threshold = by;
    }

    public Vec2<float> getUpdatedCoords() {
        this.old_coords = coords;

        if(Raylib.IsKeyDown(this.mvmt_keys[0])) {
            // up
            this.coords.Y -= this.threshold;
            return this.coords;
        } else if(Raylib.IsKeyDown(this.mvmt_keys[1])) {
            // left
            this.coords.X -= this.threshold;
            return this.coords;
        } else if(Raylib.IsKeyDown(this.mvmt_keys[2])) {
            // right
            this.coords.X += this.threshold;
            return this.coords;
        } else if(Raylib.IsKeyDown(this.mvmt_keys[3])) {
            // down
            this.coords.Y += this.threshold;
            return this.coords;
        } else return this.old_coords;
    }
}