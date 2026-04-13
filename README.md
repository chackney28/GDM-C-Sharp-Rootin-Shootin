# GDM-C-Sharp-Rootin-Shootin

Final Project for my C-Sharp class.



Project Description:

This is my final project for my C-Sharp class. The intent is to be a Top Down wave shooter based around

farmers, but the amount of implementation is up to interpretation. Within lies several hours of work

and code that breaks my understanding of how Unity works as it just doesn't make sense.

Any feedback on how to fix the issues would be greatly appreciated.



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



Key Scripts:
Manager - GameManager, Spawner, AmmoPool, NetworkManager: Fulfills the Singleton/Manager requirement

Delegates - UiManager: Has the delegate code setup, even if it's not working correctly

ObjectPool - AmmoPool and EnemyWavePool are both used in managing multiple instances of basic entities



Known Issue:

\-When doing multiplayer, both of the barrels focus on the cursor at once, but only one still shoots

\-If spawning does work it doesn't work with the Client side

&#x20; -To compound this, when checking there is no way to check for server and client, despite the engine yelling at my for not doing so

\-Shooting is weird with distance and what not because of the way I set up the pool for Bullets, most likely the first thing to get fixed

\-Zombies do not chase you because the logic for them casing you was abandoned when using a Network based system

&#x20; -They still have collision, they just won't actively hunt you

\-The player doesn't game over, mostly because the Scene to go to game over doesn't exist currently



This project has been rough, mostly because of the Network implementation, but it seems more like a rough growing phase then anything. The plan is to iron out the numerous bugs with some much needed insight. I know it's not up to my standards, but I hope you can see that I put in effort so far and am willing to keep on going for the rest of the project.

