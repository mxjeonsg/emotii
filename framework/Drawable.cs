using Raylib_cs;

using System.Numerics;

namespace Emotii.Framework;

public abstract class EADrawable {
    protected ulong times_drawn = 0;
    public ulong drawLimit = 0;
    public abstract void draw(Vec2<uint> at, Colour? tint);
}

unsafe public class EText: EADrawable {
    private string text = "<unset>";
    private Font font;

    public EText(string content) =>
        this.text = content;
    
    public EText(ulong content) =>
        this.text = content.ToString();
    
    public EText(string content, Font font) {
        this.font = font;
        this.text = content;
    }

    public EText(ulong content, Font font) {
        this.font = font;
        this.text = content.ToString();
    }

    override unsafe public void draw(Vec2<uint> at, Colour? tint) {
        if(this.drawLimit > 0) { // Check if any limit was set
            if(this.times_drawn == this.drawLimit) return;
            this.times_drawn++;
        }

        Colour workaround = tint != null ? tint : new Colour();

        Raylib.DrawText(ConvertString.strToSbytePtr(this.text), (int) at.X, (int) at.Y, 25, workaround.raylibColor);
    }
}

unsafe public class ETexture2D: EADrawable {
    private Texture2D texture;
    private bool is_texture_ready = false;

    public Vec2<uint> size {
        get { return new Vec2<uint>((uint) this.texture.Width, (uint) this.texture.Height); }
    }

    unsafe public ETexture2D(string path) {
        if(Raylib.FileExists(ConvertString.strToSbytePtr(path))) {
            this.texture = Raylib.LoadTexture(path);
            if(Raylib.IsTextureValid(this.texture))
                this.is_texture_ready = true;
        }
    }

    ~ETexture2D() {
        Raylib.UnloadTexture(this.texture);
    }

    override public void draw(Vec2<uint> at, Colour? tint) {
        if(this.drawLimit > 0) { // Check if any limit was set
            if(this.times_drawn == this.drawLimit) return;
            this.times_drawn++;
        }

        Colour workaround = tint != null ? tint : new Colour();

        Raylib.DrawTexture(this.texture, (int) at.X, (int) at.Y, workaround.raylibColor);
    }
}

unsafe public class ECounter: EADrawable {
    private ulong _counter = 0;
    private EFont font;

    public ECounter(ulong from, EFont? font) {
        EFont workaround = font != null ? font : new EFont();

        this._counter = from;
        this.font = workaround;
    }

    public void stepUp() => this._counter++;
    public void stepDown() => this._counter--;

    public void stepUpBy(ulong by) => this._counter += by;
    public void stepDownBy(ulong by) => this._counter -= by;

    public ulong count {
        get { return this._counter; }
    }

    override unsafe public void draw(Vec2<uint> at, Colour? tint) {
        Colour workaround = tint != null ? tint : new Colour();

        if(this.drawLimit > 0) { // Check if any limit was set
            if(this.times_drawn == this.drawLimit) return;
            this.times_drawn++;
        }

        Raylib.DrawTextEx(
            this.font.font,
            this.asText(),
            new Vector2((float) at.X, (float) at.Y),
            25, 1, workaround.raylibColor
        );
    }

    public string asText() => this._counter.ToString();
}