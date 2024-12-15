#pragma once

#include "../types.h"

#ifndef _WIN32
    #include <SDL2/SDL.h>
    #include <SDL2/SDL_image.h>
#else
    #include "../../include-windows/SDL2/SDL.h"
    #include "../../include-windows/SDL2/SDL_image.h"
#endif

#include "window.hh"
#include "vector.h"

class Image {
    public:
    SDL_Surface* image_surface = nullptr;
    SDL_Texture* image_texture = nullptr;

    public:
    bool success = true;

    Image(const char* image_path) {
        this->image_surface = IMG_Load(image_path);
        if(this->image_surface == nullptr) {
            fprintf(stderr, "IMG_Load[SDL2]: Failed to open image: \"%s\".\nError: %s.\n", image_path, SDL_GetError());
            this->success = false;
        }
    }

    void draw(Window* window) {
        this->image_texture = SDL_CreateTextureFromSurface(window->window_renderer, this->image_surface);
        SDL_BlitSurface(this->image_surface, nullptr, window->window_surface, nullptr);
        SDL_UpdateWindowSurface(window->window);
    }

    // If both "-1", assume center
    void drawAt(Window* window, const Vec2<i32> at) {
        SDL_Rect rect;
        // Image size
        rect.w = this->image_surface->w;
        rect.h = this->image_surface->h;
        // Draw coordinates (x, y)
        rect.x = at.x != -1 ? at.x : (i32)(window->window_size.x - rect.w) / 2;
        rect.y = at.y != -1 ? at.y : (i32)(window->window_size.y - rect.h) / 2;

        i32 tw = 0, th = 0;

        SDL_QueryTexture(this->image_texture, nullptr, nullptr, &tw, &th);

        Vec4<i32> destRect = {
            .w = tw,
            .h = th,
            .x = (i32)(window->window_size.x - tw) / 2,
            .y = (i32)(window->window_size.y - th) / 2,
        };

        SDL_Rect destRect2 = literal_vec4tosdlrect(destRect);

        SDL_RenderCopy(window->window_renderer, this->image_texture, nullptr, &destRect2);
    }

    ~Image() {
        SDL_DestroyTexture(this->image_texture);
        SDL_FreeSurface(this->image_surface);
    }
};