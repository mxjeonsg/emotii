using Raylib_cs;

namespace Emotii.Framework;

public abstract class EAEntity: EAMovable {
    protected uint entity_id = 10;
    protected uint[] spritePath = {0, 0};
    protected string name = "UwU-inator";

    // Stats
    protected ushort
    hp, atk, def, spatk, spdef,
    vit, sta, lvl, uns, und;

    public ushort[] stats {
        get { return new ushort[10] {
            this.hp, this.atk, this.def,
            this.spatk, this.spdef, this.vit,
            this.sta, this.lvl, this.uns, this.und
        };}
    }
}

public class ECharacter: EAEntity {
    public ECharacter() {
        // set base stats for characters
        this.hp = 200;
        this.lvl = 1;
        this.atk = 15;
        this.def = 13;
        this.spatk = 16;
        this.spdef = 13;
        this.vit = 8;
        this.sta = 200;

        // keyboard mappings
        KeyboardKey[] move_mapp = [
            KeyboardKey.W, KeyboardKey.A,
            KeyboardKey.D, KeyboardKey.S
        ];

        this.moveByKeyboard(true, move_mapp, .0055f);
    }
}