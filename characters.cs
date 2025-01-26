using Emotii;
using Emotii.Framework;
using Raylib_cs;

public class MainCharacter: ECharacter {
    public string setName {
        set {
            // TODO: prompt for protagonist name.
            this.name = value;
        }
    }
    public MainCharacter() {
        this.entity_id = 2;
        this.spritePath = [0, 0];
        this.name = "Protagonist";

        // set base stats for the main character
        this.hp = 200;
        this.lvl = 1;
        this.atk = 18;
        this.def = 14;
        this.spatk = 17;
        this.spdef = 20;
        this.vit = 12;
        this.sta = 200;
    }
}