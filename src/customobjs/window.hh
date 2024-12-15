#pragma once

#ifndef _WIN32
    #include <SDL2/SDL.h>
#else
    #include "../../include-windows/SDL2/SDL.h"
#endif


#include "colour.h"
#include "vector.h"

#include "../macros.h"
#include "../types.h"

class Window {
    public:
    SDL_Window* window = nullptr;
    SDL_Surface* window_surface = nullptr;
    SDL_Renderer* window_renderer = nullptr;

    Vec2<u32> window_size = {};

    public:
    bool isWindowCreated = true;

    Window(const i32 window_width, const i32 window_height, const icstr window_title) {
        this->window = SDL_CreateWindow(window_title, SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, window_width, window_height, SDL_WINDOW_SHOWN);
        if(this->window == nullptr) {
            fprintf(stderr, "SDL2: Failed to create window.\nError: %s.\n", SDL_GetError());
            this->isWindowCreated = false;
        } else {
            this->window_surface = SDL_GetWindowSurface(this->window);
            this->window_size.x = window_width;
            this->window_size.y = window_height;
        }
    }

    void clearBackgroundRGB(RGBColour rgb_colour) {
        SDL_FillRect(this->window_surface, nullptr, SDL_MapRGB(this->window_surface->format, rgb_colour.red, rgb_colour.green, rgb_colour.blue));
        SDL_UpdateWindowSurface(this->window);
    }

    void clearBackgroundRGBA(RGBAColour rgba_colour) {
        this->clearBackgroundRGB(literal_rgbatorgb(rgba_colour));
    }

    ~Window() {
        SDL_DestroyRenderer(this->window_renderer);
        SDL_DestroyWindow(this->window);
    }
};