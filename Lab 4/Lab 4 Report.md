1. What is in your scene? What is static? What is dynamic?
	Floor, inner, left, front, back, right walls, ceiling, area lights, spot lights, spheres, cubes, capsules, cylinders, light probes. A spot light and a cube are dynamic, all the rests are static.
	
	![image-20211027012406367](C:\Users\ybhba\AppData\Roaming\Typora\typora-user-images\image-20211027012406367.png)
	
2. Where are you lights placed? 
  Ceiling, front wall, and ceiling along right wall. 

3. For your wall of 8-10 point-light sources. What is the difference between mixed lighting and baked lighting? Illustrate using images in both settings. Does your lighting need to be re-baked after changing these settings?

  Mixed: ![image-20211027003140589](C:\Users\ybhba\AppData\Roaming\Typora\typora-user-images\image-20211027003140589.png)

  Baked: ![image-20211027002602688](C:\Users\ybhba\AppData\Roaming\Typora\typora-user-images\image-20211027002602688.png)

  In mixed mode, only 4 of 8 lights work, and they look a little brighter than in baked mode. Yes it needs to be re baked.

4. What happens when you turn some of these lights off, like say the first 4 in your hierarchy? Is there a difference when you turn off the first few in your hierarchy versus the last few?

  The rest 4 lights are on. 

  Yes. If the first 4 are off, the last 4 are on. Vice versa.

5. What seems to be a good number of light probes placed on a grid?

   Less than 4.

6. What seems to be a good number of light probes adjusted as in the videos?

   Only add to where lights generate. In this lab it will approximately be 50.

7. Why does my image of the right wall have a red tint?

   Because you have a glowing red sphere in the right-side room.

8. What is a PBR material?

   It means Physically Based Rendering, which can simulate how a material looks like under different lighting conditions by calculations.

9. What is the difference between real-time, baked and mixed light modes?

   Real-time calculates only direct lightings and shadows, baked calculates both direct and indirect lightings. 

   According to official manual, mixed mode combines both real-time and baked lighting, but different Lighting Modes have very different performance characteristics. 

10. How much memory did your baked lighting require?

    ![image-20211027012434292](C:\Users\ybhba\AppData\Roaming\Typora\typora-user-images\image-20211027012434292.png)

Extra Credit:

​	Transparent Sprite:

​		![image-20211027014220150](C:\Users\ybhba\AppData\Roaming\Typora\typora-user-images\image-20211027014220150.png)

Citation: 

​	9t5 PBR Textures Freebies, https://assetstore.unity.com/packages/2d/textures-materials/9t5-pbr-textures-freebies-171062