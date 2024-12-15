#define DISABLE_TRACELOG_CALLBACK
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

#ifndef _WIN32
    #include <SDL2/SDL.h>
    #include <SDL2/SDL_image.h>
    #include <SDL2/SDL_ttf.h>
#else
    #include "../include-windows/SDL2/SDL.h"
    #include "../include-windows/SDL2/SDL_image.h"
    #include "../include-windows/SDL2/SDL_ttf.h"
#endif

#include "customobjs/colour.h"
#include "customobjs/window.hh"
#include "customobjs/image.hh"
#include "customobjs/text.hh"

#include "macros.h"

#include "popups/language_selection.cc"

const i32 DEFAULT_RES_W = 1980, DEFAULT_RES_H = 1080;

bool splashscreenSlideshow(Window* window) {
    const icstr font_path = "assets/fonts/montserrat-var.ttf";
    const icstr text_splash[2] = {
        "Powered by SDL2 libraries.",
        "This game is not suitable for children or those that can get disturbed easily."
    };

    Image splash1("assets/dl_splash.png");
    if(!splash1.success) {
        fprintf(stderr, "splashscreenSlideshow(): Failed to display splash.\nError: %s.\n", SDL_GetError());
        // return false;
    }

    TTF_Font* font = TTF_OpenFont(font_path, 28);
    if(font == nullptr) {
        fprintf(stderr, "SDL2_ttf: Failed to open font \"%s\".\nError: %s.\n", font_path, TTF_GetError());
    }

    Text splash2(text_splash[0], font, literal_rgbcolour(210, 201, 211));
    Text splash3(text_splash[1], font, literal_rgbcolour(211, 210, 201));

    SDL_RenderClear(window->window_renderer);
    splash1.draw(window);
    SDL_RenderPresent(window->window_renderer);
    SDL_RenderClear(window->window_renderer);
    SDL_Delay(3000);
    splash2.draw(window);
    SDL_RenderPresent(window->window_renderer);
    SDL_RenderClear(window->window_renderer);
    SDL_Delay(4000);
    splash3.draw(window);
    SDL_RenderPresent(window->window_renderer);
    SDL_Delay(4000);
    SDL_RenderClear(window->window_renderer);

    return true;
}

#ifndef _WIN32
int main(i32 argc, icstr argv[]) {
#else
int WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
#endif
    printf("Process ID: %d.\n", getpid());
    #ifndef _WIN32
        const icstr art_argv[] = {
            "avoidLanguageDialog"
        };
        const i32 art_argc = 1;
        // const icstr lang = selectLanguage(nullptr, (i32*)&art_argc, (i8***)&art_argv);
    #else
        // const icstr lang = selectLanguage(nullptr, nullptr, nullptr);
    #endif

    if(SDL_Init(SDL_INIT_VIDEO) < 0) {
        fprintf(stderr, "SDL2: Failed to init video.\nError: %s\n", SDL_GetError());
    } else {
        // Create window
        Window main_window(DEFAULT_RES_W, DEFAULT_RES_H, "Howdy?");
        if(!main_window.isWindowCreated) {
            fprintf(stderr, "SDL2: Failed to create a window.\nError: %s.\n", SDL_GetError());
            SDL_Quit();

            return -1;
        }

        // Initialise img backend
        if(!(IMG_Init(IMG_INIT_PNG) & IMG_INIT_PNG)) {
            fprintf(stderr, "SDL2[SDL2_image]: Failed to initialise SDL_Image.\nError: %s.\n", IMG_GetError());
            SDL_Quit();

            return -1;
        }

        // Initialise font backend
        if(TTF_Init() == -1) {
            fprintf(stderr, "SDL2[SDL2_ttf]: Failed to initialise SDL_ttf.\nError: %s.\n", TTF_GetError());
            IMG_Quit();
            SDL_Quit();

            return -1;
        }


        SDL_Event event;
        bool quit = false;

        // Main game loop
        while(!quit) {
            while(SDL_PollEvent(&event)) {
                switch(event.type) {
                    // If "X" button on the window
                    // was pressed.
                    case SDL_QUIT: {
                        quit = true;
                    } break;


                    // If a key was pressed.
                    case SDL_KEYDOWN: {
                        switch(event.key.keysym.sym) {
                            // If key "ESC" was pressed.
                            case SDLK_ESCAPE: {
                                // Exit.
                                quit = true;
                            } break;

                            default: {
                                break;
                            };
                        }
                    } break;

                    default: continue;
                }
            }

            // main_window.clearBackgroundRGB(literal_rgbcolour(200, 200, 200));

            if(!splashscreenSlideshow(&main_window)) {
                fprintf(stderr, "Failed to display splash slideshow.\n");
                SDL_Delay(5000);
            }
            goto end_loop;

            end_loop:
                TTF_Quit();
                IMG_Quit();
                main_window.~Window();
                SDL_Quit();
                quit = true;
        }
    }

    SDL_Quit();
}