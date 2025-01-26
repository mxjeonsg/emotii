using Raylib_cs;

namespace Emotii.Framework;

unsafe public class EFont {
    private Font _font;
    private bool is_font_valid = false;

    public Font font {
        get { return this._font; }
    }

    unsafe public EFont(string path = "null") {
        if(path == "" || path == "null") {
            this._font = Raylib.GetFontDefault();
        } else {
            if(Raylib.FileExists(ConvertString.strToSbytePtr(path))) {
                this._font = Raylib.LoadFont(ConvertString.strToSbytePtr(path));

                if(Raylib.IsFontValid(this._font)) {
                    this.is_font_valid = true;
                }
            }
        }
    }

    ~EFont() {
        Raylib.UnloadFont(this.font);
    }
}