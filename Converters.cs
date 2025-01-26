using System.Text;

namespace Emotii;

public static class ConvertTime {
    public static UInt64 msToSec(UInt64 ms) {
        return (UInt64)(ms * 1000);
    }

    public static UInt64 secToMs(UInt64 sec) {
        return (UInt64)(sec / 1000);
    }
}

unsafe public static class ConvertString {
    unsafe public static sbyte* strToSbytePtr(string str) {
        fixed(byte* p = Encoding.ASCII.GetBytes(str)) { return (sbyte*) p; }
    }

    unsafe public static string sbytePtrToStr(sbyte* sbptr) {
        return new string((char*) sbptr);
    }
}