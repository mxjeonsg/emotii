namespace Emotii;

using Emotii.Framework;

using Raylib_cs;

internal class GameLoop {
    public const uint RES_W = 800, RES_H = 600, RES_FPS = 60;
    public const String WINDOW_TITLE = "Untitled game";

    private void preMain() {
        // Raylib.SetTargetFPS((int) GameLoop.RES_FPS);
    }

    public void start() {
        // this.preMain();

        // screen.changeToScene(new SceneTransition(TransitionKind.None, SceneKind.Splash1, 5));
    }

    public void end()  {
    }

    unsafe public static void Main(String[] args) {
        GameLoop game = new GameLoop();
        EEnvironment env = new EEnvironment();
        EAudio audioCtx = new EAudio();
        EScreen screenCtx = new EScreen(GameLoop.RES_W, GameLoop.RES_H, GameLoop.WINDOW_TITLE);

        ESfx temp = new ESfx(audioCtx, "assets\\raylib_splash_sfx.wav");
        ESpriteSheet temp2 = new ESpriteSheet("assets\\pkmn-emerald-brendan.png", new Vec2<uint>(14, 20));
        ECounter counter = new ECounter(0, new EFont());
        MainCharacter prota = new MainCharacter();

        Vec2<int> sprite_coords = new Vec2<int>(20, 20);

        while(!Raylib.WindowShouldClose()) {
            Raylib.ClearBackground(Color.Brown);

            if(Raylib.IsKeyPressed(KeyboardKey.Three)) {
                temp.play();

                ulong oldcount = counter.count;

                counter.stepUp();
            }

            Raylib.BeginDrawing();

            // game.start();
            temp2.drawSpriteNF32(1, prota.getUpdatedCoords());
            // counter.draw(new Vec2<uint>(200, 200), new Colour(200, 200, 200));

            Raylib.EndDrawing();
        }

        // game.end();
    }
}