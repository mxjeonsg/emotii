#pragma once

#include "../types.h"

#ifdef _WIN32
#include <windows.h>
#else
#include <gtk/gtk.h>
#endif

icstr _selected_wtf = nullptr;

#ifndef _WIN32
static void on_language_selected(GtkComboBoxText* combo, gpointer user_data) {
    const gchar* selected = gtk_combo_box_text_get_active_text(combo);
    _selected_wtf = (icstr) selected;
    g_print("Selected language: %s\n", selected);
    gtk_main_quit(); // Quit loop
}
#endif

const icstr default_languages[7] = {
    "English (United Kingdom)", "Spanish (Mexico)",
    "Romanian (Romania)", "Russian (Russian Federation)",
    "Ukrainian (Ukraine)", "Portuguese (Brazil)",
    "Diivagh"
};

icstr selectLanguage(const char* langs[], i32* argc, icstr* argv[]) {
    if(argc != nullptr && argv != nullptr && *argc > 1)
    for(i32 i = 0; i <= *argc; i++) {
        if(!strcmp(*argv[i], "avoidLanguageDialog")) {
            return "<avoided>";
        }
    }
    #ifdef _WIN32
        (void) argc;
        (void) argv;
        i32 selected = 0;
        WNDCLASS wc = {0};
        wc.lpfnWndProc = DefWindowProc;
        wc.lpszClassName = "LanguageSelectorClass";
        wc.hInstance = GetModuleHandle(nullptr);
        RegisterClass(&wc);

        HWND hwnd = CreateWindowEx(0,
            wc.lpszClassName, "Select Language",
            WS_OVERLAPPEDWINDOW | WS_VISIBLE,
            CW_USEDEFAULT, CW_USEDEFAULT, 400, 200,
            nullptr, nullptr, wc.hInstance, nullptr
        );

        HWND comboBox = CreateWindow(
            "COMBOBOX", "",
            CBS_DROPDOWNLIST | WS_CHILD | WS_VISIBLE
            | WS_TABSTOP, 50, 50, 300, 100, hwnd, (HMENU) 1,
            wc.hInstance, nullptr
        );

        if(langs != nullptr) {
            for(i32 i = 0; i <= sizeof(langs); i++) {
                SendMessage(comboBox, CB_ADDSTRING, 0, (LPARAM) langs[i]);
            }
        } else {
            for(i32 i = 0; i <= sizeof(default_languages); i++) {
                SendMessage(comboBox, CB_ADDSTRING, 0, (LPARAM) default_languages[i]);
            }
        }

        HWND button = CreateWindow(
            "BUTTON", "Confirm", WS_TABSTOP |
            WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            150, 120, 100, 30, hwnd, (HMENU) 2, wc.hInstance,
            nullptr
        );

        MSG event;
        while(GetMessage(&event, nullptr, 0, 0)) {
            if(event.message == WM_COMMAND) {
                if(HIWORD(event.wParam) == CBN_SELCHANGE) {
                    selected = SendMessage(comboBox, CB_GETCURSEL, 0, 0);
                    if(langs != nullptr) {
                        printf("Selected language: %s\n", langs[selected]);
                    } else {
                        printf("Selected language: %s\n", default_languages[selected]);
                    }
                }

                if(LOWORD(event.wParam) == 2) {
                    // Close window from "X".
                    PostMessage(hwnd, WM_CLOSE, 0, 0);
                }
            }

            TranslateMessage(&event);
            DispatchMessage(&event);
        }

        return langs != nullptr
        ? (icstr) langs[selected]
        : (icstr) default_languages[selected];
    #else
    GtkWidget *window, *comboBox, *label, *vbox, *button;

    // ATT: This might cause problems?
    gtk_init(argc, argv);

    window = gtk_window_new(GTK_WINDOW_TOPLEVEL);
    gtk_window_set_title(GTK_WINDOW(window), "Select Language");
    gtk_window_set_default_size(GTK_WINDOW(window), 300, 200);
    g_signal_connect(window, "destroy", G_CALLBACK(gtk_main_quit), nullptr);

    // Vertical container
    vbox = gtk_box_new(GTK_ORIENTATION_VERTICAL, 10);
    gtk_container_add(GTK_CONTAINER(window), vbox);


    // Label
    label = gtk_label_new("Please select your language:");
    gtk_box_pack_start(GTK_BOX(vbox), label, false, false, 5);

    // The damn combobox
    comboBox = gtk_combo_box_text_new();
    if(langs != nullptr) {
        for(i32 i = 0; i <= sizeof(langs); i++) {
            gtk_combo_box_text_append_text(GTK_COMBO_BOX_TEXT(comboBox), langs[i]);
        }
    } else {
        for(i32 i = 0; i <= sizeof(default_languages); i++) {
            gtk_combo_box_text_append_text(GTK_COMBO_BOX_TEXT(comboBox), default_languages[i]);
        }
    }

    gtk_box_pack_start(GTK_BOX(vbox), comboBox, FALSE, FALSE, 5);

    // What the
    g_signal_connect(comboBox, "changed", G_CALLBACK(on_language_selected), nullptr);

    button = gtk_button_new_with_label("Confirm");
    gtk_box_pack_start(GTK_BOX(vbox), button, false, false, 5);
    g_signal_connect(button, "clicked", G_CALLBACK(gtk_main_quit), nullptr);

    // Show all widgets
    gtk_widget_show_all(window);

    // Start da shi
    gtk_main();

    return _selected_wtf; // Fix this crap
    #endif
}