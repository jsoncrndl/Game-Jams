# Game Concept
Initial game concept for Trijam #197. The jam will take place over six 30-minute sprints, where the design document is revisted after each.

Sprints:
1. Spaceship control
2. Enemies
3. Camera splitting
4. Difficulty curves
5. Polish and menu
6. Catch-up time

## Gameplay
The main gameplay is that of a top-down shooter. A ship flies around and is barraged with enemy attacks. The ship must shoot the others to stay alive.

## Splitting
Whenever a ship is hit, the camera splits and each half of the ship takes a new screen. Each half gets some invincibility time, then the game continues. A ship can be divided 3 times (Changed to only twice). Any hits after that destroy the ship for good and that screen turns black.

## Difficulty
The difficulty slowly increases as the game goes on. Every time 15 enemies are defeated on a single screen, the level of that screen increases. When a screen splits, the new screen resets its level to 0. The maximum difficulty is 10. The difficulty affects the rate of enemy spawns and projectile speed.

## Scoring
A player gains a point for each enemy destroyed, multiplied by a bonus for the level on which that enemy was destroyed. The total score is the sum of all points after all ships are destroyed.

## Implementation
Needed classes
* [GameManager](GameManager.md)
* [ShooterGame](ShooterGame.md)
* [Shooter](Shooter.md)
* [Projectile](Projectile.md)