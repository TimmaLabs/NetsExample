[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)](http://commitizen.github.io/cz-cli/)

# NetsExample

Run your web application on a fully compliant ES6 environment based on the Chromium web browser engine with access to OS resources &ndash; JavaScript &rarr; .NET (CEFSharp & CEF) &rarr; Baxi.NET &rarr; Payment terminal

## Development

1. Install the required USB drivers for terminal communication: [IngenicoUSBDrivers_2.80_setup.exe](./IngenicoUSBDrivers_2.80/)

  * Enable the `Force COM Port Feature` and type in `9` in the [leftmost input field at the bottom of the dialog](./assets/images/force-com-port.png)

2. Install the required [Visual C++ runtime components](https://www.microsoft.com/en-us/download/details.aspx?id=40784) (both `x86` and `x64` architectures if you're running Windows 10)

3. Download & install the Windows 10 SDK: https://go.microsoft.com/fwlink/?LinkID=698771

4. Optional: if you're developing inside a VM, [expose USB from the host machine to your VM](./assets/images/share-host-usb.jpeg) (`Devices` -> `USB` -> `Sagem`)

5. Optional: download [Microsoft Expression Design 4](https://www.microsoft.com/en-us/download/details.aspx?id=36180) for converting vector art (e.g. `.ai`) into XAML

    * .ico generator: https://iconverticons.com/online/

6. Run `npm install` (make sure you have Node.js installed and available in your %PATH%)

7. Launch the application:

    * run `npm run example` to start example web application
    * open the solution (`app/NetsExample.sln`) in Visual Studio (or alike) and run it.

If Visual Studio complains about a missing `BBS` (Baxi API) reference when you try building/running the application, do the following:

  1. Right-click on the `NetsExample` project
  2. `Add` -> `Reference...`
  3. Select `..Baxi.net_1.4.2.1\baxi.net45\baxi_dotnet.dll`

When running in `DEBUG` mode, a browser console will be opened alongside the application to allow interactive debugging/inspection of the application. See [example/README.md](./example/README.md) for usage examples.

The solution consist of 3 projects: the application (`NetsExample`) and two installer projects (`setup` and `bundle`). `setup` packages the installer (`.msi`) for the application whereas `bundle` includes the application installer together with its dependencies (USB driver and .NET 4.5.2). Run `npm run` for an overview of all the available scripts for building & signing the application (e.g. `npm run build:release` will build a release version of the application under `./bin`)

## Tips for Versioning

The version format is `major.minor.patch.revision`. The general revisioning principles are as follows:

* Breaking changes: bump `major`
* New features (backwards compatible): bump `minor` (increment by the number of new features)
* Bug fixes/refactoring: bump `patch`
* Stylistic changes, docs, build: bump `revision`

In .NET lingo, `patch` is often referred to as the `build` version.

If the release includes changes, start with incrementing the respective version numbers (`major`/`minor`/`patch`/`revision`):

1. Assembly version: right-click on `NetsExample` project -> `Properties` -> `Application` -> `Assembly Information...` -> `Assembly Version` and `File Version` (can be identical, see http://stackoverflow.com/a/65062 for more information)
2. Installer ([Setup.wxs](setup/Setup.wxs) and [Bundle.wxs](Bundle/bundle.wxs)) versions (see code comments & https://www.firegiant.com/wix/tutorial/upgrades-and-modularization/ for reference)
3. [package.json](./package.json) version

## FAQ

### What to do if the test payment terminal rejects all/some of the test cards?
Perform reconciliation for each Nets accounts configured on the terminal. If that doesn't help, try fetching the latest dataset (_Fetch cards_). If the test cards continue to act up, just try each one until one starts working, or shoot [Nets](mailto:salessupport-fi@nets.eu) a message.

### What to do if terminal connection can not be established?
Confirm that the USB driver was successfully installed. Open `Device Manager` and upon connecting the payment terminal via USB you should see `Sagem Telium` appear under `Ports (COM & LPT)`. Also, confirm that [Force COM port is enabled and set to the appropriate value](./assets/images/com-port-settings.png) (`COM9`)
