#pragma once

#include "../types.h"

#ifndef _WIN32
    #include <SDL2/SDL.h>
#else
    #include "../../include-windows/SDL2/SDL.h"
#endif


struct gameobj_rgbcolour {
    u8 red, green, blue;
};
typedef struct gameobj_rgbcolour RGBColour;
#define literal_rgbcolour(r, g, b) (RGBColour){ (u8) r, (u8) g, (u8) b }

struct gameobj_rgbacolour {
    unsigned char red, green, blue, alpha;
};
typedef struct gameobj_rgbacolour RGBAColour;
#define literal_rgbacolour(r, g, b, a) (RGBAColour){ (u8) r, (u8) g, (u8) b, (u8) a }

#define literal_rgbtorgba(rgb_obj) (RGBAColour) { (u8) rgb_obj.red, (u8) rgb_obj.green, (u8) rgb_obj.blue, (u8) 0xff }
#define literal_rgbtosdlrgba(rgb_obj) (SDL_Color) { (u8) rgb_obj.red, (u8) rgb_obj.green, (u8) rgb_obj.blue, (u8) 0xff }
#define literal_rgbatorgb(rgba_obj) (RGBColour) { (u8) rgba_obj.red, (u8) rgba_obj.green, (u8) rgba_obj.blue }
#define literal_rgbatosdlrgb(rgba_obj) (SDL_Color) { (u8) rgba_obj.red, (u8) rgba_obj.green, (u8) rgba_obj.blue, (u8) 0xff }