using Raylib_cs;

namespace Emotii.Framework;

unsafe public class EEnvironment {
    private string currentpath;

    public string cwd {
        get { return this.currentpath; }
        set { this.currentpath = value; }
    }

    public unsafe EEnvironment() {
        // Set `pwd` to the root of the project.
        Raylib.ChangeDirectory(ConvertString.strToSbytePtr("..\\..\\.."));
    }

    public void setCwd(string new_path) {
        Raylib.ChangeDirectory(ConvertString.strToSbytePtr(new_path));
        this.currentpath = new_path;
    }
}