# OS X Integration

1. Start by downloading Timma's pre-packaged Windows distribution: https://goo.gl/IAu97k (~10GB, so store it in a USB drive before going to the shop!)
2. Install [VirtualBox](https://www.virtualbox.org/wiki/Downloads) for OS X:
  1. Open the downloaded `.dmg` file
  2. Then open the Terminal app: CMD + SPACE and type `Terminal` & hit enter
  3. On the terminal prompt, run: `sudo installer -package /Volumes/VirtualBox/VirtualBox.pkg -target /` (input your password when asked)
3. Open VirtualBox and import the pre-packaged Windows:
  1. Choose `File` -> `Import Appliance...`
  2. Import the downloaded Windows distribution (`.ova`)
4. Start the Virtual Machine via the [VirtualBox Manager dialog](../assets/images/virtuabox-manager-dialog.png)
