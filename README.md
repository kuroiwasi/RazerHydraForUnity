# RazerHydraForUnity
## Preparation
1. Install Razer Hydra Driver : http://drivers.razersupport.com/index.php?_m=downloads&_a=view&parentcategoryid=181
2. Install Sixense SDK for the Razer Hydra on Steam : steam://install/42300
3. Import the installed "sixense_x64.dll" into your Unity project.
## How to use
1. Place a GameObject with `RazerHydraManager` attached to the scene that uses Razer Hydra.  
note: You can skip this step. When the `RazerHydraManager` is not in the scene and instance is referenced, a new GameObject will be created with the `RazerHydraManager` attached to it.
2. You can get the input for each controller through a `RazerHydraInput` script attached to a GameObject.
