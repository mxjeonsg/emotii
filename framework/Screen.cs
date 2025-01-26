using Raylib_cs;

namespace Emotii.Framework;

unsafe public class EScreen {
    private Vec2<uint> resolution = new Vec2<uint>(100, 100);
    private bool contextStarted = false;
    private ushort monitorIdx = 0;

    private void refreshStatus() {
        if(!this.contextStarted) return;

        this.isFocused = Raylib.IsWindowFocused();
        this.isReady = Raylib.IsWindowReady();
        this.isFullscreen = Raylib.IsWindowFullscreen();
        this.isHidden = Raylib.IsWindowHidden();
        this.isMinimised = Raylib.IsWindowMinimized();
        this.isMaximised = Raylib.IsWindowMaximized();
        this.isResized = Raylib.IsWindowResized();

        this.monitorIdx = (ushort) Raylib.GetCurrentMonitor();

        this.isCursorVisible = !Raylib.IsCursorHidden();
    }


    public bool
    isFocused, isReady, isFullscreen, isHidden,
    isMinimised, isMaximised, isResized, isCursorVisible,
    isCursorEnabled;

    public string screenName = "<unset>";
    public string windowTitle = "<unset>";

    unsafe public EScreen(uint width, uint height, string title) {
        this.resolution.X = width;
        this.resolution.Y = height;
        this.windowTitle = title;

        Raylib.InitWindow((int) this.resolution.Y, (int) this.resolution.X, ConvertString.strToSbytePtr(this.windowTitle));

        this.monitorIdx = (ushort) Raylib.GetCurrentMonitor();
        this.screenName = ConvertString.sbytePtrToStr(Raylib.GetMonitorName(Raylib.GetCurrentMonitor()));
    }

    ~EScreen() {
        Raylib.CloseWindow();
    }

    public void start() {
    }

    public void close() {
        Raylib.CloseWindow();
    }

    public bool shouldClose() {
        return Raylib.WindowShouldClose();
    }

    public void switchCursorVisibility() {
        if(this.isCursorVisible) {
            Raylib.HideCursor();
            this.isCursorVisible = false;
        } else {
            Raylib.ShowCursor();
            this.isCursorVisible = true;
        }
    }
}