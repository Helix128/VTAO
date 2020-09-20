# VTAO
 Vertex ambient occlusion baking tool for Unity.
 
 # Requirements
 
 A vertex color compatible shader (i used ProBuilder shaders for the demo scene).
 
 Unity 2020.1.2f1 was used for the creation of this tool and proper functionality of it is only ensured on the current and later versions.
 
 # Usage
 
 Open the VTAO tab and click Bake Object. 
 
 In the VTAO GUI there are 3 baking modes:
 
 - Bake
 
 - Bake with Children
 
 - Bake entire scene
 [google.com]
 For the 2 first modes, you must input a target object in the GUI slot for it.
 
 "Bake" bakes the target object only.
 
 "Bake with Children" bakes the target object and its child meshes.
 
 "Bake entire scene" bakes every mesh in the scene, and it does not require to select a target object. It may require a larger baking time for larger scenes. 
 
 
 # F.A.Q
 
 Can VTAO be baked at runtime?
 
 -Yes,just call VGI_Main.BakeObject([Object to bake],[Ray length]);
 
 However, its not suggested to use it in realtime, and even though a realtime demo script is provided its only meant to be used for demonstration/benchmarking purposes.
 
 Does it work on mobile? 
 
 -It works in any platform where you can use a vertex color shader. However,calling baking methods might be slower so its suggested to bake everything in the editor.
