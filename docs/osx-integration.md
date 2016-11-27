# OS X Integration

1. Start by downloading Timma's pre-packaged Windows distribution: https://goo.gl/IAu97k (~10GB, so store it in a USB drive before going to the shop!)
2. Install [VirtualBox](https://www.virtualbox.org/wiki/Downloads) for OS X*
3. Open VirtualBox and import the pre-packaged Windows:
  1. Choose `File` -> `Import Appliance...`
  2. Import the downloaded Windows distribution (`.ova`)
4. Start the Virtual Machine via the [VirtualBox Manager dialog](../assets/images/virtuabox-manager-dialog.png)
5. Prevent the host machine (Mac) from going to sleep:

  Open `System Preferences` -> `Energy Saver` -> `Power Adapter` (tab) and make sure `Prevent computer from sleeping automatically when the display is off` checked. This will help preventing the VM from losing USB connectivity when the host machine (Mac) is left idle. If the host machine is put to sleep/restarted, the user will need to [share the USB connection to the VM](../assets/images/share-host-usb.jpeg) again (toggle `Sagem` off & on).

\* If the installer gets stuck on `Verifying...`, proceed as follows
  1. Open the downloaded `.dmg` file
  2. Then open the Terminal app: CMD + SPACE and type `Terminal` & hit enter
  3. On the terminal prompt, run: `sudo installer -package /Volumes/VirtualBox/VirtualBox.pkg -target /` (input the admin password when prompted for)
