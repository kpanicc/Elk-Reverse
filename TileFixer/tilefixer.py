from sys import argv
from PIL import Image

default_frame_size = 5

usage = """tileset_route tile_size_x tile_size_y frame_size output_file"""


def read_image(image_route):
    return Image.open(image_route)


def extract_tiles(img, tile_size, number_of_tiles):
    tiles = []
    for i in range(number_of_tiles[0]):
        row_tiles = []
        for j in range(number_of_tiles[1]):
            left = j * tile_size[0]
            top = i * tile_size[1]
            right = left + tile_size[0]
            bottom = top + tile_size[1]

            cropped_img = img.crop((left, top, right, bottom))
            cropped_img.load()
            row_tiles.append(cropped_img)

        tiles.append(row_tiles)

    return tiles


def expand_tile(tile, frame_size, new_tile_size):
    framed_tile = Image.new("RGBA", new_tile_size)

    framed_tile.paste(tile, (frame_size, frame_size))

    # Left frame
    left_frame = tile.crop((0, 0, 1, tile.size[1]))
    for i in range(frame_size):
        framed_tile.paste(left_frame, (i, frame_size))

    # Top frame
    top_frame = tile.crop((0, 0, tile.size[0], 1))
    for i in range(frame_size):
        framed_tile.paste(top_frame, (frame_size, i))

    # Right frame
    right_frame = tile.crop((tile.size[0] - 1, 0, tile.size[0], tile.size[1]))
    for i in range(frame_size):
        framed_tile.paste(right_frame, (tile.size[0] + frame_size + i, frame_size))

    # Bottom frame
    bottom_frame = tile.crop((0, tile.size[1] - 1, tile.size[0], tile.size[1]))
    for i in range(frame_size):
        framed_tile.paste(bottom_frame, (frame_size, tile.size[1] + frame_size + i))

    return framed_tile


def expand_tiles(tiles, tile_size, frame_size):
    new_tile_size = (tile_size[0] + frame_size * 2, tile_size[1] + frame_size * 2)

    framed_tiles = []

    for i in range(len(tiles)):
        row_tiles = tiles[i]
        new_tiles = []
        for j in range(len(row_tiles)):
            expanded_tile = expand_tile(row_tiles[j], frame_size, new_tile_size)
            new_tiles.append(expanded_tile)

        framed_tiles.append(new_tiles)

    return framed_tiles, new_tile_size


def generate_tileset(tiles, tile_size):
    tileset_size = (tile_size[0] * len(tiles[0]), tile_size[1] * len(tiles))
    tileset = Image.new("RGBA", tileset_size)

    for i in range(len(tiles)):
        row_tiles = tiles[i]
        for j in range(len(row_tiles)):
            tileset.paste(row_tiles[j], (j * tile_size[1], i * tile_size[0]))
            
    return tileset


def main(image_route, tile_size, frame_size, output_file):
    img = read_image(image_route)

    number_of_tiles = []
    number_of_tiles.append(int(img.size[1] / tile_size[1]))
    number_of_tiles.append(int(img.size[0] / tile_size[0]))

    tiles = extract_tiles(img, tile_size, number_of_tiles)
    (new_tiles, new_tile_size) = expand_tiles(tiles, tile_size, frame_size)
    fixed_tileset = generate_tileset(new_tiles, new_tile_size)

    fixed_tileset.save(output_file)


if __name__ == "__main__":
    if len(argv) < 5:
        print("Not enough input arguments")
        print("Usage: " + usage)
    elif len(argv) < 6:
        main(argv[1], (int(argv[2]), int(argv[3])), default_frame_size, argv[4])
    else:
        main(argv[1], (int(argv[2]), int(argv[3])), argv[4], argv[5])
