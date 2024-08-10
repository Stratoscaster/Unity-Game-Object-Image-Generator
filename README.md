This project was created using code from this Git repo, with some modifications for bulkification of work. 
https://gist.github.com/mickdekkers/5c3c62539c057010d4497f9865060e20

To use this tool:

1. Drop all of the prefabs you want images of in the `Resources/` folder of your project. Composite models for GameObjects may not work as well - it is best for it to be a singular model.
2. Enter Play Mode in Unity, and press the spacebar. This will only work once, so if you want to take more images, you will need to exit Play Mode, and re-enter it. This is to prevent you from accidentally taking pictures too much.

The images generated will be in the folder `<YourProjectName>/Snapshots`.

Feel free to tinker with this project to change the destination folder, scale of the images, or scale of the GameObjects during the image capturing process. 
You can change variables in `ResourceSnapshotGenerator.cs` such as `position`, `rotation`, `scalingCeiling`, `backgroundColor`, and the `width` and `height` parameters that are given in the `TakePrefabSnapShot()` call within `ResourceSnapShotGenerator.UpdatePreview()` method.

Note: There are some stray files within the project that aren't used as I was testing this code. This took a few different attempts and different pieces of code to get right.

Hope it helps!

