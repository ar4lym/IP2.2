# IP2.2

# ITD

# Background Content

- The concept of this project is a VR experience that aims to simulate the daily life of an individual living with OCD,
  portraying both their personal daily routines and emotionally driven outdoor scenarios.
- Users can choose between 5 different interactive tasks within a Singapore-inspired neighbourhood.
- The tasks range from locating a lost child driven by OCD-related anxiety, to cleaning benches and bedroom, arrangement of objects and coloured tiles alignment.
- After completing each tasks, the time taken for users to complete the task will be recorded with the corresponding achievements

# Project Objectives

- Raise awareness on OCD and its challenges
- Building Empathy using VR and how it can be applied for social good
- Reinforce IMH's mission on promoting mental health

# Game Objectives

- Complete five interactive tasks that simulate OCD-related behaviours
- Manage time effectively while performing each task
- Complete tasks to unlock corresponding achievements
- Experience the emotional and cognitive challenges faced by individuals with OCD

# Target Audience

- General public
- Students and educators
- Mental health professionals
- Community groups

# Recommended Requirements

- Meta Horizon Mobile App
- VR Headset (Meta Quest 2)
- Updated to latest system software
- Minimum 64GB storage
- Two Quest Touch Controllers
- A minimum 2m x 2m play area is recommended for safe interaction

# Controls:

- W (Moving forward)
- A (Moving to the left side)
- S (Moving to the back)
- D (Moving into right side)

- Shift + I (Moving forward)
- Shift + J (Moving to the left side)
- Shift + L (Moving to the back)
- Shift + K (Moving into right side)

- G (Grab)
- I (Teleport)

# Limitations/bugs of mechanics

- Mechanic 1 (Park): Clues when not picked up properly tend to fall through the terrain. If the clue falls through the terrain before the player can hold for 3s to register the clue, the player would be unabe to move on with the game and would need to restart the entire game.
- Mechanic 2 (Bedroom) :
- Mechanic 3 (Supermarket): Some objects may unintentionally launch away when released due to XR velocity tracking and physics behaviour (can be within the store or totally gone)
- Mechanic 4 (Bench Cleaning): This mechanic can only be played once. When game completed = true, there is no restart logic.
    Spray point must be within close proximity and directly facing the bench dirt, or ray cast will not be detected.
- Mechanic 5 (Tiles):

# Answers to the puzzles/ Game Hack

- Mechanic 1 (Park): Find the clues in Park to determine the lost OCD child
- Mechanic 2 (Bedroom) : Mopping spilled water and disposing trash into the bin
- Mechanic 3 (Supermarket): Rearrange the 10 items correctly inside the store ( 5 on pringles shelf, 2 Noodles, 1 Meiji Biscuit, 1 Hershey and 1 Preggo Sauce sections )
- Mechanic 4 (Bench Cleaning): Each bench has a green block representing dirt. When the green block is properly sprayed and disappears, it counts as one bench cleaned. Clean all 10 benches to complete the game.
- Mechanic 5 (Tiles): Teleport onto every red tile for 2 seconds to turn them green

Referneces and credits to external assets
Girl in park: (https://sketchfab.com/3d-models/child-changli-wuthering-waves-3bd583c6433c48929524ed8bc26d6be7)
Boy in park: (https://sketchfab.com/3d-models/victorian-child-low-poly-character-model-ac62dc5a6b9148719201d8ba0f495d65)
Textures for models: (https://substance3d.adobe.com/community-assets?assetType=substanceMaterial)
