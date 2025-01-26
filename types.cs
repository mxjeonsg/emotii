using System.Drawing;
using System.Numerics;
using Raylib_cs;

namespace Emotii;

public class Vec2<VectorType> {
    private VectorType x, y;

    public VectorType X {
        get { return this.x; }
        set { this.x = value; }
    }

    public VectorType Y {
        get { return this.y; }
        set { this.y = value; }
    }

    public Vec2(VectorType x, VectorType y) {
        this.x = x;
        this.y = y;
    }

    public Vec2(VectorType x) {
        this.x = x;
        this.y = x;
    }
}

public class Vec3<VectorType> {
    private VectorType x, y, z;

    public VectorType X {
        get { return this.x; }
        set { this.x = value; }
    }

    public VectorType Y {
        get { return this.y; }
        set { this.y = value; }
    }

    public VectorType Z {
        get { return this.z; }
        set { this.z = value; }
    }

    public Vec3(VectorType x, VectorType y, VectorType z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vec3(VectorType x) {
        this.x = x;
        this.y = x;
        this.z = x;
    }
}

public class Colour {
    private byte r = 180, g = 180, b = 180, a = 255;

    public byte red {
        get { return this.r; }
        set { this.r = value <= (byte) 255 ? value : (byte) 255; }
    }

    public byte green {
        get { return this.g; }
        set { this.g = value <= (byte) 255 ? value : (byte) 255; }
    }

    public byte blue {
        get { return this.b; }
        set { this.b = value <= (byte) 255 ? value : (byte) 255; }
    }

    public byte alpha {
        get { return this.a; }
        set { this.a = value <= (byte) 255 ? value : (byte) 255; }
    }

    public Raylib_cs.Color raylibColor {
        get {
            return new Raylib_cs.Color(this.r, this.g, this.b, this.a);
        }
    }

    public Colour(byte red = 180, byte green = 180, byte blue = 180, byte? alpha = 255) {
        this.r = red;
        this.g = green;
        this.b = blue;
        if(alpha != null) this.a = (byte) alpha;
    }
}