COMPILER ?= clang++
WCOMPILER ?= x86_64-w64-mingw32-g++
OPTIMISER ?= 0
CXX_SHUT_UP = -Wwritable-strings

GTK_INCLUDES ?= \
-I/usr/include/gtk-3.0 \
-I/usr/include/pango-1.0 \
-I/usr/include/glib-2.0 \
-I/usr/lib/x86_64-linux-gnu/glib-2.0/include \
-I/usr/include/harfbuzz \
-I/usr/include/freetype2 \
-I/usr/include/libpng16 \
-I/usr/include/libmount \
-I/usr/include/blkid \
-I/usr/include/fribidi \
-I/usr/include/cairo \
-I/usr/include/pixman-1 \
-I/usr/include/gdk-pixbuf-2.0 \
-I/usr/include/x86_64-linux-gnu \
-I/usr/include/gio-unix-2.0 \
-I/usr/include/atk-1.0 \
-I/usr/include/at-spi2-atk/2.0 \
-I/usr/include/at-spi-2.0 \
-I/usr/include/dbus-1.0 \
-I/usr/lib/x86_64-linux-gnu/dbus-1.0/include \
-I/usr/include/glib-2.0 \
-I/usr/lib/x86_64-linux-gnu/glib-2.0/include

GTK_LIBS ?= $(GTK_INCLUDES) -lgtk-3 -lgdk-3 -lz -lpangocairo-1.0 -lpango-1.0 -lharfbuzz -latk-1.0 -lcairo-gobject \
-lcairo -lgdk_pixbuf-2.0 -lgio-2.0 -lgobject-2.0 -lglib-2.0 -lglib-2.0 

LIBS ?= -lSDL2 -lSDL2_image -lSDL2_ttf -lm $(GTK_LIBS)
WLIBS ?= -lm -lgdi32 -lcomdlg32 ./lib-windows/sdl2/SDL2.dll ./lib-windows/sdl2/libSDL2.a ./lib-windows/sdl2/libSDL2main.a ./lib-windows/sdl2/SDL2_image.dll ./lib-windows/sdl2/SDL2_ttf.dll

WINCLUDES ?= -I./include-windows

CXX_FLAGS ?= $(LIBS) -O$(OPTIMISER) -g $(CXX_SHUT_UP)

WCXX_FLAGS ?= $(WLIBS) -O$(OPTIMISER) -g -mwindows -static-libgcc -static-libstdc++

emotii: src/emotii.cc
	clear
	$(COMPILER) $(CXX_FLAGS) -o out/emotii_debug src/emotii.cc
	clear
	x86_64-w64-mingw32-windres assets/windows-res/prog-icon.rc -O coff -o assets/windows-res/prog-icon.res
	x86_64-w64-mingw32-windres assets/windows-res/prog-det-deb.rc -O coff -o assets/windows-res/prog-det-deb.res
	x86_64-w64-mingw32-windres assets/windows-res/prog-det-rel.rc -O coff -o assets/windows-res/prog-det-rel.res
	$(WCOMPILER) $(WCXX_FLAGS) -o out/emotii_debug.exe src/emotii.cc assets/windows-res/prog-icon.res assets/windows-res/prog-det-deb.res