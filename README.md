## 2D-Mini-Top-down-Game

unitypackage：https://pan.baidu.com/s/14DWu1DtFEoJtZcccMmUECQ?pwd=p5uc

This is a 2D mini game. It may help novice get start with their game.

Unity Version: 2022.3.17f1c1 

Player Health:30
Melee Enemy Health:2
Ranged Enemy Health:1

Level Manager: 2 levels, 30 seconds for each level. In level1, 10 Melee Enemies. In level 2, 5 Ranged Enemies and 15 Melee Enemies.

Player Control: WASD, Left-mouse to fire, Right-mouse to release melee attack, Space button to Pause.

Obstacle: Randomly Generate in maps(15 destructible and 15 indestructible)

Enemy Behaviour Strategies:

1.Melee enemies can use abilities and will try to approach the hero when possible.
2.Ranged enemies can fire bullets, always aiming at the hero and will slowly retreat if the hero enters their safety zone.
3.While generally avoiding getting stuck, enemies may still become trapped(with very low possibility) in the corners of obstacles due to the obstacle colliders being 1*1 squares as per the Size Settings specification. Using circular colliders couldsolve this issue.
4.Melee enemies will only use abilities when the hero is within their 3x3 attack range.
5.Ranged enemies will avoid shooting when there are obstacles between them and the hero, only firing when there is a clear line of sight.
6.Ranged enemies aim to maintain a specific distance from the hero, ideally 5 units. They will move closer if the distance exceeds 5 units and move away if the hero is within that range.

AI Tool: Tongyi Qianwen is used for fixing compile errors and offering advice during the implementation of new features.

Sound Effects and Background Music: Sound effects are applied only to the player's pistol. Furthermore, the background music stops upon entering Level 2. 

Resolution: The game resolution is adjustable, but users may encounter menu buttons with disproportionate sizes when changing the resolution.

Pause Button: Press Space Button can pause the game, and repress it to resume.

In addition, I have also implemented several new features: firearms now rotate to face the mouse position (enemies' guns will perpetually point towards the player). The damage from the player's melee attacks is triggered only during the animation playback, thereby eliminating bugs where damage would register before the animation actually begins.

Finally, "Acknowledge" Button in Main Menu is clickable, and when pressed, it will display my name.

# UPDATE:
1. Fixed mismatch between attack animation and damage trigger.   (5/14/2024)
