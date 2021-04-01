# SpaceMouse Macro icons maker
This program allows to add icon to existing macro on the space mouse devices by 3Dconnexion.

## How it works
The program has 1 window:
![Capture d’écran 2021-04-01 212605](https://user-images.githubusercontent.com/28806724/113344068-e7b45300-9330-11eb-991d-63f17206c55a.png)

- File: Enter here the file path to the 3Dx configuration you would like to edit. 3Dx stores the files in two locations (by default):
  - %AppData%/Roaming\3Dconnexion\3DxWare\Cfg => Configuration files created by the user using the 3Dx Smart UI:
![image](https://user-images.githubusercontent.com/28806724/113344529-79bc5b80-9331-11eb-932a-34db0f521ccc.png)
  - [DriverInstallDir]\3DConnexion\3DxWinCore64\Cfg => default configuration files provided by 3Dx

- Select existing macro: After a configuration fil has been selected, this combo list the existing macros
- Image : Allow the user to select an image to associate with the macro A preview of this image is shown.
- Add icon => will modify the configuration file to asociate the selected macro with the selected image


The macro must have been created using the 3dx smart UI first.

## Get the tool
- Download the file 3DxConfigurationEditor, then run it.

## Compile
- Download the source
- Open the solution (with visual studio preferably)
- Compile
