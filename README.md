# Setup

## General

* Install the required USB drivers for ECR communication: [IngenicoUSBDrivers_2.80_setup.exe](./IngenicoUSBDrivers_2.80/)

* Set the following COM configuration via `Device Manager` -> `Ports (COM & LPT)` -> `Sagem Telium Comm Port` -> `Port Settings`:

  * `Bits per second`: 57600
  * `Advanced...` -> `COM Port Number`: COM3

## Development

* Expose USB from the host machine to your VM: `Devices` -> `USB` -> `Sagem`

* If Visual Studio complains about a missing `BBS` (Baxi API) reference when you try building/running the application, do the following:

  1. Right-click on the `Timma` project
  2. `Add` -> `Reference...`
  3. Select `..Baxi.net_1.4.2.1\baxi.net45\baxi_dotnet.dll`

* To share Windows `localhost` to your host machine see http://naxoc.net/2013/10/22/windows-in-virtualbox-on-the-mac/

  On your Windows VM the `C:\Windows\System32\drivers\etc\hosts` file should have the same `<TimmaDocker>` entries as in your `/etc/hosts`, something along the lines of

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
