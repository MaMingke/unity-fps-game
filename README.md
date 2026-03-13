# FPS Target Practice Game (Unity)

## Project Overview
This project is a 3D First-Person Shooter (FPS) training game developed using the Unity game engine.  
The goal of the game is to shoot targets scattered across a small town environment and obtain as many points as possible within a limited time.

The project was developed as a graduation thesis project for the Computer Science BSc program at Eszterházy Károly University.

The game focuses on implementing fundamental FPS mechanics such as player movement, shooting, collision detection, scoring systems, and UI interaction.

---

## Game Features

- First-person player control
- Target shooting mechanics
- Score calculation system
- Time-limited gameplay
- Interactive UI system (Start / Restart / Quit)
- Physics-based collision detection
- 3D environment with terrain and buildings

---

## Gameplay

The player explores a small town environment from a first-person perspective and shoots targets to earn points.

Key gameplay rules:

- The game starts when the player presses the **Start** button.
- A countdown timer begins immediately.
- Targets are placed throughout the map.
- The player gains points when hitting targets.
- When the timer ends, the final score is displayed.
- 
 Hitting the center of the target gives higher points, while hits farther from the center give fewer points.

---

## Controls

| Action | Key |
|------|------|
| Move Forward | W |
| Move Backward | S |
| Move Left | A |
| Move Right | D |
| Jump | Space |
| Sprint | Left Shift |
| Shoot | Left Mouse Button |

These controls follow standard FPS game conventions to provide a familiar experience for players. 

---

## Technical Implementation

The game is built using **Unity 2020.3** and programmed primarily in **C#**.

Main technical elements include:

### Player Controller
The player movement system reads keyboard inputs using Unity's input system 

### Shooting System
The weapon fires bullets using raycasting and projectile spawning.

Key features:
- Bullet direction based on camera raycast
- Weapon animation and sound
- Muzzle flash effects
- Fire rate control

### Collision Detection
Bullet collisions are handled through Unity's physics engine.  
When a bullet collides with a target, the system calculates the hit position and awards points.

### Scoring System
The score depends on the distance between the hit point and the center of the target.

The closer the hit is to the bullseye, the higher the score.

### UI System
The UI includes:

- Start Menu
- Countdown Timer
- Score Display
- Game Over Screen
- Restart and Quit Buttons

---
## Screenshots

![Gameplay](screenshot/gameplay.png)
![Score](screenshot/score.png)
