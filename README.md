# OBNI - Objet Bruité Non Identifié

OBNI is a texture based vertex and color displacement Unity shader with several public properties to play with.

## What do you get ? 

- A noise shader based on the GPU-GEMS-Improved-Perlin-Noise (https://github.com/Scrawk/GPU-GEMS-Improved-Perlin-Noise) shader generates a noise texture in a Custom Render Texture.

- OBNI shader which takes the noise texture as a deformation texture and a color texture. 

## Results 

![Imgur](https://i.imgur.com/xrW9Os5.gif)  | 
![Imgur](https://i.imgur.com/0pCis9L.gif) |
![](https://github.com/alexbourgeois/OBNI/blob/master/Results/ezgif.com-gif-maker.gif)   |    ![](https://github.com/alexbourgeois/OBNI/blob/master/Results/ezgif.com-optimize.gif) 

## Create an OBNI

### Noise Shader

- Create a Material and set its shader to "Noise/PerlinNoiseShader";

- Duplicate the "PerlinNoise" custom render texture and set its material with the one you created just before;

- Add a NoiseController script to a GameObject and set its Render texture with the new one.

### OBNI shader

- Create a Material and set its shader to "Noise/OBNIShader";

- Set the previously created custom render texture as the displacement map;

- Put the texture you want as the color texture;

- Apply this material on any Mesh Renderer.
