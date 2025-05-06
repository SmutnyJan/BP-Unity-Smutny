log_file_path = "Player.log"

mainmenu_sum = 0
mainmenu_count = 0
lobby_sum = 0
lobby_count = 0
levelg_sum = 0
levelg_count = 0

with open(log_file_path, 'r', encoding='utf-8') as file:
    for line in file:
        line = line.strip()
        if (line.startswith("MainMenu FPS:")
            or line.startswith("Settings FPS:")):
            fps = float(line.split(":")[1].strip().replace(",", "."))
            mainmenu_sum += fps
            mainmenu_count += 1
        elif (line.startswith("LobbyMenza FPS:")
              or line.startswith("LobbyG FPS:")):
            fps = float(line.split(":")[1].strip().replace(",", "."))
            lobby_sum += fps
            lobby_count += 1
        elif line.startswith("LevelG FPS:"):
            fps = float(line.split(":")[1].strip().replace(",", "."))
            levelg_sum += fps
            levelg_count += 1

def calc_avg(total, count):
    return total / count if count > 0 else 0

print(
    f"Průměrné FPS pro MainMenu (včetně Settings): "
    f"{calc_avg(mainmenu_sum, mainmenu_count):.2f}")
print(f"Průměrné FPS pro Lobby (LobbyMenza/LobbyG):"
      f"{calc_avg(lobby_sum, lobby_count):.2f}")
print(f"Průměrné FPS pro LevelG: {calc_avg(levelg_sum, levelg_count):.2f}")
