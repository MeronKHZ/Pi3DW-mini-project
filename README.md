# Pi3DW Mini-Project: Zombie Escape

## Overview of the Game
This project is a simple first-person shooter mini-game where the player explores a small indoor level, collects a key, avoids enemies, and reaches the exit door to win.  
The genre is a light first-person exploration and escape game, originally inspired by the K-drama *All of Us Are Dead* and *Duty After School*.

### Main Parts of the Game
- **Player Controller** – First-person movement using WASD, jump, and mouse look.
- **Camera** – Attached to the player for classic FPS aiming and view control.
- **Key Item** – A pick-up object required to unlock the exit door.
- **Enemies** – Zombies patrolling along predefined points using NavMesh. They chase the player when close and cause a lose-screen on collision.
- **Exit Door** – Finishes the game when the player has the key; otherwise displays a “Need Key” message.
- **Shooting System** – Raycast shooting with bullet trails, spread control, and enemy damage.
- **UI** – Mission text, win screen, lose screen, and need-key message.

## Game Features
- First-person movement with jumping and mouse-controlled camera.
- Enemy AI with patrol + chase behavior via NavMesh.
- Collectable key and exit-door win condition.
- Shooting system with bullet tracer and impact effects.
- Built using ProBuilder for quick environment creation.
- Basic lighting setup for atmosphere.
- Simple zombie enemy logic with animations.

## Running the Project
1. Download Unity (2021.2 or later recommended).
2. Clone or download this repository.
3. Open the project in Unity.
4. Press **Play** in the Editor.
5. Requires keyboard and mouse.

## Project Parts

### Scripts
- **PlayerController** – First-person character movement + camera look.
- **EnemyAI** – Patrol behavior and player-chasing logic using NavMeshAgent.
- **EnemyHit** – Detects enemy-player contact and triggers the lose UI.
- **EnemyHealth** – Handles enemy damage and death effects.
- **PlayerShooting** – Handles shooting, raycasts, bullet trails, and impact VFX.
- **KeyItem** – Key pickup and global key tracking.
- **ExitDoor** – Controls win-condition and key-check logic.

### Models & Prefabs
- Level created using ProBuilder shapes inside Unity.
- Zombie model from Unity Asset Store:  
  https://assetstore.unity.com/packages/3d/characters/hungry-zombie-99750
- Key object and shooting VFX.
- Particle Pack (impact, muzzle flash, hit effects) from Unity Asset Store:  
  https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325
- UI prefabs (win text, lose text, need-key text).

## References
- Unity FPS Movement Controller 2022/2023 Tutorial  
  https://www.youtube.com/watch?v=1tT2hz-tKTg  

- Level Design in Unity Using ProBuilder  
  https://www.youtube.com/watch?v=viYCikXOAJc  

- Hitscan Guns with Bullet Tracers (Raycast Shooting)  
  https://www.youtube.com/watch?v=cI3E7_f74MA
