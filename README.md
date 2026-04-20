# GDM-C-Sharp-Rootin-Shootin

Final Project for my C-Sharp class.



Project Description:

This is my final project for my C-Sharp class. The intent is to be a Top Down wave shooter based around

farmers, but the amount of implementation is up to interpretation. Within lies several hours of work

and code that breaks my understanding of how Unity works as it just doesn't make sense.

Any feedback on how to fix the issues would be greatly appreciated.



How to play:

Using the standard WSAD and a mouse pointer to shoot system, the purpose is to shoot down Zombies getting to your barn.

Dying merely stuns you for a hot second, what really matters is how your barn is doing. Try to see how long you can last.



Setup:

Download the file, Open in unity, and run from the TitleScreen scene.



How to run a mirror in Unity:

1. Open Unity
2. Go to Window -> Package Manager
3. Click on the + button in the top left corner
4. Select Add Package from git URL and insert the link below

https://github.com/VeriorPies/ParrelSync.git?path=/ParrelSync

5\. Once done downloading, you can see "ParralSync" in the options bar

6\. In the new bar select Clones Manager

7\. Within it, do Add new clone and open in New Editor once it is loaded

8\. Now you have to 2 instances of the game to test multiplayer



How to test multiplayer:

1. In the original Unity instance, start playtest and select the NetworkManager to start a Host connection.
2. In the cloned Unity instance, start playtest and select the NetworkManager to start a Client connection.



Project Structure:

The game is split amongst 5 scenes. One for the title screen, 2 for two different arenas, 1 for dying, and 1 seeing scores.

You'll typically going between the title to a arena to death and repeating.



Key Scripts:
Manager - GameManager, Spawner, AmmoPool, NetworkManager: Fulfills the Singleton/Manager requirement

Delegates - UiManager: Has the delegate code setup, even if it's not working correctly

ObjectPool - AmmoPool and EnemyWavePool are both used in managing multiple instances of basic entities



Known Issue:

\-When doing multiplayer, both of the barrels focus on the cursor at once, but only one still shoots

\-Leaving during a game puts the other player into limbo

