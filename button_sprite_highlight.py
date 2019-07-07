from sys import argv
from PIL import Image
import os

def read_image(image_route):
    return Image.open(image_route)

def combine_sprites(foreground_route, background_route):
    foreground_img = read_image(foreground_route).convert("RGBA")
    background_img = read_image(background_route).convert("RGBA")

    new_sprite = Image.new("RGBA", (background_img.width, background_img.height))

    offset = (int((background_img.width - foreground_img.width) / 2),
                int((background_img.height - foreground_img.height) / 2))
    new_sprite.paste(background_img)
    new_sprite.paste(foreground_img, offset, foreground_img)
    
    return new_sprite
    
if __name__ == "__main__":
    if len(argv) < 4:
        print("Not enough input arguments")
    else:
        text_to_append = argv[1]
        selector_route = argv[2]
        button_sprite_routes = argv[3:]
        for button_sprite_route in button_sprite_routes:
            new_sprite = combine_sprites(button_sprite_route, selector_route)

            old_route_parts = os.path.splitext(button_sprite_route)
            new_button_sprite_route = "{}{}{}".format(old_route_parts[0], text_to_append, old_route_parts[1])
            print(new_button_sprite_route)
            new_sprite.save(new_button_sprite_route)