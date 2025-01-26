// Borrowed from `https://stackoverflow.com/questions/217902/reading-writing-an-ini-file`.
// Adapted to this need, but mainly based in that first implementation.

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Emotii.Framework;

public class IniParse {
    private string path;
    private string exe = Assembly.GetExecutingAssembly().GetName().Name;

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string section, string key, string value, string fpath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string section, string key, string dflt, StringBuilder retval, int size, string fpath);

    public IniParse(string inipath = null) {
        this.path = new FileInfo(inipath ?? this.exe+".ini").FullName;
    }

    public string read(string key, string section = null) {
        StringBuilder result = new StringBuilder(255);
        IniParse.GetPrivateProfileString(section ?? this.exe, key, "", result, 255, this.path);
        return result.ToString();
    }

    public void write(string? key, string? value, string? section = null) {
        IniParse.WritePrivateProfileString(section ?? this.exe, key, value, this.path);
    }

    public void deleteKey(string key, string? section = null) {
        this.write(key, null, section ?? this.exe);
    }

    public void deleteSection(string? section = null) {
        this.write(null, null, section ?? this.exe);
    }

    public bool keyExists(string key, string? section = null) {
        return this.read(key, section).Length > 0;
    }
}