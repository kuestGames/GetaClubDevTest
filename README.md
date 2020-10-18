# GetaClubDevTest por Octavio López
DevTest for GetaClub

Detalles del proyecto de prueba:

////////Detalles generales URP:
El proyecto está configurado en URP para Android (se menciona que es para móviles en las indicaciones)
Como técnicas de optimización, además de modelos LowPoly, se hizo un atlas con la textura que cada uno de los componentes. También se cambió la compresión de algunas texturas, específicamente los Normal Maps a formar ASTC, ya que el formato estándar no tiene la calidad suficiente en este caso.
Principalmente se hizo con los objetos del campamento por cuestiones de tiempo.

Modelo del campamento tomado de CGTrader con licencia comercial gratis de atribución:
https://www.cgtrader.com/animatedheaven/reviews
Autor: Shahid Abdullah

Sonido de Motor:
https://freesound.org/people/MarlonHJ/sounds/242740/

Sonido de Lluvia
https://freesound.org/people/MarlonHJ/sounds/242740/

Modelo de Automóviles:
https://www.cgtrader.com/free-3d-models/car/racing/low-poly-sports-car-20
https://www.cgtrader.com/free-3d-models/car/suv/jeep-renegade-a-5-doors-compact-suv-from-2016

Texturas:
CGTextures.com

Rocas y Tronco de Asset Store:
https://assetstore.unity.com/packages/3d/vegetation/photoscans-free-tree-01-160706
https://assetstore.unity.com/packages/3d/environments/landscapes/photoscanned-moutainsrocks-pbr-130876



////////ShaderGraph:
Se hizo los shaders indicados con una pequeña anotación. Se requiere que el primer shader tenga un heightmap sobre un plano primitivo. El heightmap se puede usar como displacement, o bump. El problema es la falta de polígonos en un plano primitivo. Por lo pronto no se puede hacer tesselation con shader graph por lo cual el vertex displacement no se puede llevar acabo. Otra opción es hacer pixel displacement, simulando el displacement por medio de parallax. Desafortunadamente el nodo requerido solo está disponible para HDRP y no URP. Hay otras formas de simularlo pero por cuestiones de tiempo me fui por otra opción: Usar el heightmap como Bump y fusionarlo con el Normal Map, cada uno teniendo su propia fuerza y Tiling.

////////Particulas:
Use las particulas para crear el efecto de lluvia. Para ahorrar partículas use el tiling para que la textura de la gota se viera doble sin tener que aumentar demasiado la cantidad de partículas. Viendolo detenidamente se ve obvio el efecto pero trabajando posteriormente la textura se puede mejorar el efecto.


////////Código:
Programe los waypoints como Bezier con puntos de control para que el movimiento no fuera tan lineal.

////////Git:
Aquí andamos