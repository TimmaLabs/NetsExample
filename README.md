[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)](http://commitizen.github.io/cz-cli/)

# Setup

## General

### Production

* [Ordering/registering a new POS](./docs/deployment-checklist.md#deployment-checklist)
* [Setting up the integration](./docs/deployment-checklist.md#installation-on-site--remote)

### Development

1. Install the required USB drivers for ECR communication: [IngenicoUSBDrivers_2.80_setup.exe](./IngenicoUSBDrivers_2.80/)

  * Enable the `Force COM Port Feature` and type in `9` in the [leftmost input field at the bottom of the dialog](./assets/images/force-com-port.png)

2. Install the required [Visual C++ runtime components](https://www.microsoft.com/en-us/download/details.aspx?id=40784) (both `x86` and `x64` architectures if you're running Windows 10)

3. Download & install the Windows 10 SDK: https://go.microsoft.com/fwlink/?LinkID=698771

4. In your Windows VM, add the following entries to `C:\Windows\System32\drivers\etc\hosts`:

    ```
    # <TimmaDocker>
    10.0.0.11 db.timma.dev
    10.0.0.12 api.timma.dev
    10.0.0.13 timma.dev
    10.0.0.14 customer.timma.dev
    10.0.0.15 business.timma.dev
    10.0.0.16 customer-sites.timma.dev
    10.0.0.17 controlpanel.timma.dev
    10.0.0.18 blog.timma.dev
    # </TimmaDocker>
    ```

  These should match the respective entries in your Mac's `/etc/hosts`.

5. [Expose USB from the host machine to your VM](./assets/images/share-host-usb.jpeg) (`Devices` -> `USB` -> `Sagem`). Refer to the [OS X Integration docs](./docs/osx-integration.md) for enabling USB auto-mounting.

6. Optional: download [Microsoft Expression Design 4](https://www.microsoft.com/en-us/download/details.aspx?id=36180) for converting vector art (e.g. `.ai`) into XAML

  * .ico generator: https://iconverticons.com/online/

See [example.js](./example.js) for usage examples.

If Visual Studio complains about a missing `BBS` (Baxi API) reference when you try building/running the application, do the following:

  1. Right-click on the `Timma` project
  2. `Add` -> `Reference...`
  3. Select `..Baxi.net_1.4.2.1\baxi.net45\baxi_dotnet.dll`

## Version Control

The version format is `major.minor.patch.revision`. The general revisioning principles are as follows:

* Breaking changes: bump `major`
* New features (backwards compatible): bump `minor` (increment by the number of new features)
* Bug fixes/refactoring: bump `patch`
* Stylistic changes, docs, build: bump `revision`

In .NET lingo, `patch` is often referred to as the `build` version.

If the release includes changes, start with incrementing the respective version numbers (`major`/`minor`/`patch`/`revision`):

1. Assembly version: right-click on `Timma` project -> `Properties` -> `Application` -> `Assembly Information...` -> `Assembly Version` and `File Version` (can be identical, see http://stackoverflow.com/a/65062 for more information)
2. Installer ([Setup.wxs](setup/Setup.wxs) and [Bundle.wxs](Bundle/bundle.wxs)) versions (see code comments & https://www.firegiant.com/wix/tutorial/upgrades-and-modularization/ for reference)
3. [package.json](./package.json) version
4. And, if `major` version bump: `NetsService` version in [TimmaCustomer](https://github.com/TimmaLabs/TimmaCustomer)

## FAQ

### What to do if the test payment terminal rejects all/some of the test cards?
Perform reconciliation for each of the Nets accounts via the [Payment Terminal](./assets/images/payment_terminal_version.png) view. If that doesn't help, try fetching the latest dataset via the _Print information_ button on the Payment Terminal page. If the cards continue to act up, just try each one until one starts working, or shoot [Nets](mailto:salessupport-fi@nets.eu) a message
