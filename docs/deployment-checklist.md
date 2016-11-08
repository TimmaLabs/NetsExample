# Deployment Checklist

## General

### Supported payment terminals

Payment terminals provided by Nets that we currently support:

  * [iCT250](https://shop.nets.eu/fi/web/fin/40?terminal_id=TFIN4400-7648-R) (preferred)
  * [iPP350](https://ingenico.us/smart-terminals/pinpads-terminals/ipp-350.html) (requires an external receipt printer)
  * [iWL250B](https://shop.nets.eu/fi/web/fin/40?terminal_id=TFIN4300-7648-R) (uses Bluetooth, not yet "fully supported")

### Supported terminal version

`4.81` (equivalent test terminal version: `43.19`)


## Before Going On-site

### Which operating system is the customer using / looking to use?

* Windows: the supported versions are 7, 8 and 10 (both 32-bit and 64-bit)

* OS X (Mac): the customer will need to run Windows in a VM (Virtual Machine). See the docs for [OS X Integration](./osx-integration.md).

### What type of payment terminal solution does the customer currently have?

* None: ask which one of the [supported terminals](#supported-payment-terminals) the customer would be interested in. We will then deal with contacting [Nets sales](https://shop.nets.eu/) for ordering it.

* Standalone or integrated:
  * If the terminal is **not** provided by Nets, ask the customer to terminate his/her current contract and proceed as above
  * If the terminal is **not** one that we support, proceed as above.

### What do I need to register a customer with Nets?

Once the customer has a Nets provided terminal we support, ask the customer for their [payment terminal ID](#how-do-i-figure-out-the-id-of-the-payment-terminal). Then, contact Nets to check if the customer's payment terminal is running the latest (supported) version. Nets will need the **payment terminal ID** for this. Any updates required to the payment terminal software will be done during the [installation process](#installation-on-site--remote).

### What type of business is the customer running? Does it include multiple proprietorships or is everybody under a single company?

If the customer consists of multiple proprietorships, Nets will need to enable a so-called **Multi-banking support** on the payment terminal. Contact Nets about this and they'll provide us the respective IDs (`OperID`) for identifying each sole proprietorship. **Forward this information to the dev team!**

### What extra equipment & materials does the customer need?

* Receipt rolls (Nets ships only 2 along with the original packaging)
* [USB A to USB B](../assets/images/usb-a-to-usb-b.jpg) cable (with a 90° angle)
* If the customer is running on a Mac: pre-packaged Windows on a USB stick (see [OS X Integration](./osx-integration.md) for more)


## Installation (on-site / remote)

### 1. Installing the Timma software

  * Download the installer from [bit.ly/timma-for-windows](http://bit.ly/timma-for-windows)
  * If the customer is running Windows, install the version corresponding to the processor (either `32-bit` or `64-bit`)
  * If the customer is running OS X, see the docs for [OS X Integration](./osx-integration.md).

The installation might take up to 10 minutes to complete depending on the OS & existing software installed, so be patient.

### 2. Integrating the payment terminal with Timma

  * Change the terminal settings to [communicate with Timma via USB](#how-do-i-prepare-the-payment-terminal-to-be-connected-to-timma)
  * Connect the payment terminal to the computer via a [USB A to USB B](../assets/images/usb-a-to-usb-b.jpg) cable
  * Launch `Timma.exe` (should be available on the Desktop)
  * Wait for the login screen to appear. If the connection was **not** successfully established, a [warning prompt should appear at the top of the application window](../assets/images/terminal-disconnected.jpeg). If this is the case, try [re-connecting the terminal to the computer](reconnecting-the-terminal.md).

### 3. Checking the terminal software for updates

You can check the terminal for updates via Timma (skip to step 2.), or directly over the Ethernet cable (faster alternative):

  1. If you wish to do the update over the Ethernet cable, [change the terminal settings accordingly](#how-do-i-connect-the-payment-terminal-directly-to-nets-over-ethernet)
  2. [Check/update the payment terminal software](#how-do-i-checkupdate-the-payment-terminal-software-version)
  3. After the terminal is finished with the update, remember to [set the communication type back to USB](#2-integrating-the-payment-terminal-with-timma)



## FAQ

### How do I figure out the ID of the payment terminal?

Make sure power is turned on the terminal, swipe the merchant card on the terminal and select:

`6. Parameters` -> `1. Other` -> `1. Main Settings` (terminal ID should appear on the screen)

### How do I prepare the payment terminal to be connected to Timma?

Make sure power is turned on the terminal, swipe the merchant card on the terminal and select:

* `6. Parameters` -> `1. Other`
  * `2. Connections` -> `Comm. type = Cashier`
  * `3. Cashier` -> `Cashier = Yes`
  * `3. Cashier` -> `Comm. type = USB SLAVE`

### How do I connect the payment terminal directly to Nets (over Ethernet)?

Make sure power is turned on the terminal, swipe the merchant card on the terminal and select:

* `6. Parameters` -> `1. Other`
  * `2. Connections` -> `Comm. type = Ethernet`
  * `2. Connections` -> `Host IP Address = 91.102.24.142` (might be different in Sweden?)
  * `2. Connections` -> `Host Port = 9670`
  * `3. Cashier` -> `Cashier = No`

### How do I update/check the version of the payment terminal software?

Make sure power is turned on the terminal, swipe the merchant card on the terminal and select:

`8. Program` -> `1. Fetch Program`

If updates are avaialble, this will download & install the latest software (rebooting the terminal in the process). If the payment terminal has the most up-to-date version of the software, it will print the software version number on a receipt. The version of the software should be at least that of the [abovementioned version](#supported-terminal-version).
