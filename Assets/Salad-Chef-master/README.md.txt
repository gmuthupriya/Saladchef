# SALAD CHEF #
A "programmer-art” version of a salad chef simulation in Unity 


## TOOLS USED ##
* UNITY 3D 2019.4.3f1
* Visual Studio Code


## CODE STRUCTURE ##
Most of the codes can be found inside *Scripts/Controllers* Folder. Games Constants are defined in Constants.cs.
**Gamecontroller.cs** handles the game play of the application . It also handles Reloading the game and resetting everyting.
**HUDController.cs** handles the display of Scores and Times. 

**Player Controller.cs** handles the follwing functionalities.
* Movement of Player
* Picking Up Vegetables from Sides
* Droping Vegetables to Chopping Board
* Droping Combinations to Trash
* Droping to Customer Tables.

## How to Play
### Controls
- Use [WASD] or arrow keys to move the player1.
- [E] key to interact and pick up vegetables.
- [R] to place a vegetable on the chopping board.
- [E then R] to serve a combination to a waiting customer.

## Gameplay Features

### Vegetable Implementation
- Choose from a variety of vegetables to interact with in the game.
- Vegetables available on both sides for player interaction.

### Item Placing by Player
- Ability to pick up two vegetables at a time.
- Chopping vegetables on the black chopping board incurs a delay, during which the player cannot move.
- Combinations can be created by placing multiple vegetables on the chopping board.

### Customer Interaction
- Customers wait for a defined period based on the number of ingredients.
- Delivering correct combinations in time increases the player's score.
- Delayed or incorrect combinations result in penalties and angry customers.
- Angry customers decrease waiting times faster and might lead to doubled penalties if not satisfied promptly.

### Other Interactions
- Ability to throw the prepared salad into a trashcan, resulting in point deduction.

Most of the collisions are handled using Triggers and Tags. All the definations of the tags can be found in constants.cs







