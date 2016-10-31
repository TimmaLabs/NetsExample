# Setup

## General

### For development/testing

* Install the required USB drivers for ECR communication: [IngenicoUSBDrivers_2.80_setup.exe](./IngenicoUSBDrivers_2.80/)

* Install the required [Visual C++ runtime components](https://www.microsoft.com/en-us/download/details.aspx?id=40784)

* Set the ECR's virtual COM port to `COM9`:

  * `Device Manager` -> `Ports (COM & LPT)` -> `Sagem Telium Comm Port` -> `Port Settings` -> `Advanced...` -> `COM Port Number`

### For production (merchant) deployment

  * Install Timma

  * [baxi.ini](app/baxi.ini):

    * Modify the fields in `VendorInfoExtended` to match the business ID and application version

    * Make `HostIpAddress` to point to the Nets production server

## Development

* Download & install the Windows 10 SDK: https://go.microsoft.com/fwlink/?LinkID=698771

* In your Windows VM, add the following entries to `C:\Windows\System32\drivers\etc\hosts`:

    ```
    # <TimmaDocker>
    192.168.0.10 db.timma.dev
    192.168.0.1 api.timma.dev
    192.168.0.2 timma.dev
    192.168.0.3 admin.timma.dev
    192.168.0.4 business.timma.dev
    192.168.0.5 customer-sites.timma.dev
    # </TimmaDocker>
    ```

  These should match the respective entries in your Mac's `/etc/hosts`.

* Expose USB from the host machine to your VM: `Devices` -> `USB` -> `Sagem`

* If Visual Studio complains about a missing `BBS` (Baxi API) reference when you try building/running the application, do the following:

  1. Right-click on the `Timma` project
  2. `Add` -> `Reference...`
  3. Select `..Baxi.net_1.4.2.1\baxi.net45\baxi_dotnet.dll`

* See [example.js](./example.js) for usage examples

* Optional: download [Microsoft Expression Design 4](https://www.microsoft.com/en-us/download/details.aspx?id=36180) for converting vector art (e.g. `.ai`) into XAML

  * .ico generator: https://iconverticons.com/online/
