# Task 2: Singleton Implementation

## Student Info

- Name: Gabriel Lugo-Maldonado
- ID: 01102327

## Pattern: Singleton

### Implementation

The Singleton pattern is very simple. It is just a self-referencing variable which is checked upon
initialization. If the variable is not set, then it is set to the current GameObject, otherwise the
GameObject destroys itself. This ensures only a single instance is created (hence the name Singleton).

### Game Integration

I implemented the Singleton pattern in two places in the project: the main menu and the UI manager.
The main menu uses a singleton to manage the state the menu is in (buttons being pressed).
The UI manager in the game manages the score UI to update it depending on the current score of the player.

## Game Description

- Title: TASK1_Lugo
- Controls: WASD - Directions
- Objective: Collect as high a score as possible

## Repository Stats

- Total Commits: 11
- Development Time: 10 hours (combined)
