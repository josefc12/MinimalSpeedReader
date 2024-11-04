// See https://aka.ms/new-console-template for more information
using System;
using SDL2;

namespace MinimalSpeedReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize SDL video subsystem
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine("SDL could not initialize! SDL_Error: " + SDL.SDL_GetError());
                return;
            }

            // Create an SDL window
            IntPtr window = SDL.SDL_CreateWindow(
                "Minimal SDL2 Window",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                800, // Window width
                600, // Window height
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
            );

            // Check if window creation succeeded
            if (window == IntPtr.Zero)
            {
                Console.WriteLine("Window could not be created! SDL_Error: " + SDL.SDL_GetError());
                SDL.SDL_Quit();
                return;
            }

            // Create an SDL renderer
            IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine("Renderer could not be created! SDL_Error: " + SDL.SDL_GetError());
                SDL.SDL_DestroyWindow(window);
                SDL.SDL_Quit();
                return;
            }

            // Main loop flag
            bool quit = false;
            SDL.SDL_Event e;

            // Main loop
            while (!quit)
            {
                // Event handling
                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    // User requests quit
                    if (e.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        quit = true;
                    }
                }

                // Set the draw color (background color)
                SDL.SDL_SetRenderDrawColor(renderer, 0, 128, 255, 255); // Blue color

                // Clear the screen
                SDL.SDL_RenderClear(renderer);

                // Update the screen
                SDL.SDL_RenderPresent(renderer);

                // Add a small delay to avoid excessive CPU usage
                SDL.SDL_Delay(16); // ~60 FPS
            }

            // Cleanup
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }
    }
}

