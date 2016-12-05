# Setup

## General

### Production

See the relevant section in the [Deployment Checklist](./docs/deployment-checklist.md#installation-on-site--remote).

### Development

1. Install the required USB drivers for ECR communication: [IngenicoUSBDrivers_2.80_setup.exe](./IngenicoUSBDrivers_2.80/)

  * Enable the `Force COM Port Feature` and type in `9` in the [leftmost input field at the bottom of the dialog](./assets/images/force-com-port.png)

2. Install the required [Visual C++ runtime components](https://www.microsoft.com/en-us/download/details.aspx?id=40784) (both `x86` and `x64` architectures if you're running Windows 10)

3. Download & install the Windows 10 SDK: https://go.microsoft.com/fwlink/?LinkID=698771

4. In your Windows VM, add the following entries to `C:\Windows\System32\drivers\etc\hosts`:

    ```
    # <TimmaDocker>
    192.168.0.10 db.timma.dev
    192.168.0.1 api.timma.dev
    192.168.0.2 timma.dev
    192.168.0.3 customer.timma.dev
    192.168.0.4 business.timma.dev
    192.168.0.5 customer-sites.timma.dev
    192.168.0.6 admin.timma.dev
    # </TimmaDocker>
    ```

  These should match the respective entries in your Mac's `/etc/hosts`.

5. [Expose USB from the host machine to your VM](./assets/images/share-host-usb.jpeg) (`Devices` -> `USB` -> `Sagem`)

6. Optional: download [Microsoft Expression Design 4](https://www.microsoft.com/en-us/download/details.aspx?id=36180) for converting vector art (e.g. `.ai`) into XAML

  * .ico generator: https://iconverticons.com/online/

See [example.js](./example.js) for usage examples.

If Visual Studio complains about a missing `BBS` (Baxi API) reference when you try building/running the application, do the following:

  1. Right-click on the `Timma` project
  2. `Add` -> `Reference...`
  3. Select `..Baxi.net_1.4.2.1\baxi.net45\baxi_dotnet.dll`

## Version Control

If the release will include breaking changes, start with incrementing the version numbers (`major`/`minor`/`patch`):

1. Assembly version: right-click on `Timma` project -> `Properties` -> `Application` -> `Assembly Information...` -> `Assembly Version` and `File Version` (can be identical, see http://stackoverflow.com/a/65062 for more information)
2. Installer ([Setup.wxs](setup/Setup.wxs) and [Bundle.wxs](Bundle/bundle.wxs)) versions: https://www.firegiant.com/wix/tutorial/upgrades-and-modularization/
3. `NetsService` version in [timma_admin_new](https://bitbucket.org/lauriorkoneva/timma_admin_new)

The version format is `major.minor.patch.build`. The `build` version can be freely incremented for internal revisioning purposes, but changes to `major`/`minor`/`patch` versions require updating the version/GUID entries in the installer files as described in step 2 above.

## FAQ

### What to do if the test payment terminal rejects all/some of the test cards?
The card parameters need to be updated. You can do this via `3. Cards` -> `1. Get cards` in the terminal's merchant menu.
