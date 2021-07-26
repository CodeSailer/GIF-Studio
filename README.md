# GIF-Studio
A simple program to make gif from images 
Blog post: https://codesailer.com/projects/imagestogif/

## Softwwrae used
To use this program you need to have ffmpeg(put "ffmpeg.exe" inside the directory of the executible).  
you can download it here: https://ffmpeg.org/download.html#build-windows  

## How to use
https://www.youtube.com/watch?v=s5LITo6rlLM  

## Batch files code
ToGIF.bat  
```console
type settings.txt
set /p dur=<settings.txt
ffmpeg -y -r %dur% -i images\%%d.jpg -c:v libx264 -pix_fmt yuv420p slide.mp4
ffmpeg -y -i slide.mp4 -filter_complex "fps=%dur%,scale=400:-1:" animated.gif
pause
```

ToGIFHQ.bat  
```console
type settings.txt
set /p dur=<settings.txt
ffmpeg -y -r %dur% -i images\%%d.jpg -c:v libx264 -pix_fmt yuv420p slide.mp4
ffmpeg -y -i slide.mp4 -vf fps=%dur%,scale=320:-1:flags=lanczos,palettegen palette.png
ffmpeg -y -i slide.mp4 -i palette.png -filter_complex "fps=%dur%,scale=400:-1:flags=lanczos[x];[x][1:v]paletteuse" animated.gif
pause
```  
The difference between the two is that the second one create a palette that is used to increase the quality of the gif but it will also make it a little bigger.