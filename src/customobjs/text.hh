#pragma once

#ifndef _WIN32
    #include <SDL2/SDL.h>
    #include <SDL2/SDL_ttf.h>
#else
    #include "../../include-windows/SDL2/SDL.h"
    #include "../../include-windows/SDL2/SDL_ttf.h"
#endif

#include "colour.h"
#include "window.hh"

class Text {
    public:
    TTF_Font* text_font = nullptr;
    SDL_Surface* text_surface = nullptr;
    SDL_Color text_colour = {};
    SDL_Texture* text_texture = nullptr;

    Text(const char* string, TTF_Font* font, RGBColour colour) {
        this->text_font = font;
        this->text_colour = literal_rgbtosdlrgba(colour);
        this->text_surface = TTF_RenderText_Solid(font, string, this->text_colour);
    }

    ~Text() {
        SDL_FreeSurface(this->text_surface);
        SDL_DestroyTexture(this->text_texture);
    }

    void draw(Window* window) {
        this->text_texture = SDL_CreateTextureFromSurface(window->window_renderer, this->text_surface);
        SDL_BlitSurface(this->text_surface, nullptr, window->window_surface, nullptr);
        SDL_UpdateWindowSurface(window->window);
    }

    void drawAt(Window* window, const Vec2<i32> at) {
        SDL_Rect rect;
        // Image size
        rect.w = this->text_surface->w;
        rect.h = this->text_surface->h;
        // Draw coordinates (x, y)
        rect.x = at.x != -1 ? at.x : (i32)(window->window_size.x - rect.w) / 2;
        rect.y = at.y != -1 ? at.y : (i32)(window->window_size.y - rect.h) / 2;

        i32 tw = 0, th = 0;

        SDL_QueryTexture(this->text_texture, nullptr, nullptr, &tw, &th);

        Vec4<i32> destRect = {
            .w = tw,
            .h = th,
            .x = (i32)(window->window_size.x - tw) / 2,
            .y = (i32)(window->window_size.y - th) / 2,
        };
        SDL_Rect destRect2 = literal_vec4tosdlrect(destRect);

        SDL_RenderCopy(window->window_renderer, this->text_texture, nullptr, &destRect2);
    }
};