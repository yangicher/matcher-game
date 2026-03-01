Memory Match Game

A classic memory pairs matching game built in Unity. My main focus here was to create a clean, scalable architecture and a highly optimized UI that doesn't drop frames.

🛠 Tech Stack & Setup
Engine: Unity 6000.3.9f1

Backend: Firebase Firestore

How to Run
Clone the repo: git clone <your-repo-url>

Open the project in Unity 6000.3.9f1.

Mac Users Note: Firebase SDK sometimes gets blocked by Apple's security. If you get a "library load disallowed" error, run this in your terminal:

Bash

sudo xattr -r -d com.apple.quarantine Assets/Firebase/Plugins
Open Splash scene, hit Play, and the game will initialize Firebase and load the Main Menu.

🗄️ Database (Firestore)
I used Firestore to keep track of global high scores.

Collection: leaderboard

Document structure:

playerName (String)

score (Integer) - calculated from time and moves

moves (Integer)

difficulty (Integer) - used to separate difficulty levels (e.g., 4 vs 8 pairs)

timestamp (Number) - Unix time

Note: To make the leaderboard work properly, I set up a Composite Index in Firebase on pairsCount (Ascending) and score (Descending).

🏗️ Architecture & Cool Stuff
I tried to keep the codebase as clean and modular as possible:

MVP UI: The UI is completely separated from the logic. Views just handle clicks and animations, while Controllers talk to the database and manage the game state.

Zero-Allocation Leaderboard: Instead of spawning 100 prefabs for 100 players, I wrote a custom RecycledListView with an Object Pool. It only spawns about 12 items (what fits on the screen). When you scroll, it just moves the top item to the bottom and updates the text. Super smooth!

Scriptable Objects for Configs: All game settings (difficulty, time limits, card themes) are driven by ScriptableObjects. No magic numbers in the code. You can literally create a new "Pictures mode" by just making a new asset in the editor and typing in some emojis.

🚧 Known Limitations
Open Database: Right now, Firestore security rules are set to allow read, write: if true; to make testing easier. Obviously, not for production.

No Offline Error UI: If you drop your internet connection while opening the leaderboard, you'll just see an endless loading spinner.

Hardcoded Name: The player's name is currently grabbed from the settings or hardcoded. There's no popup to type your name yet.

💡 What's Next (If I had more time)
Firebase Auth: Add anonymous login so I can lock down the Firestore rules (if request.auth != null) and prevent bot spam.

Offline Caching: Save scores to PlayerPrefs or JSON if there's no internet, and push them to Firebase once the connection is back.

UI Polish: Add a proper "Enter your name" screen and handle network error popups gracefully.

