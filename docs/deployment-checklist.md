# Deployment Checklist

## General

### Supported payment terminals

Payment terminals provided by Nets that we currently support:

  * [iCT250](https://shop.nets.eu/fi/web/fin/40?terminal_id=TFIN4400-7648-R) (preferred)
  * [iPP350](https://shop.nets.eu/fi/web/fin/40?terminal_id=TFIN4100-7641-R) (requires an external receipt printer, no automatic reconciliation capability)

### Minimum supported terminal version

* 🇫🇮 `4.82` (equivalent test terminal version `43.19`)
* 🇸🇪 `4.62`

## Before Going On-site

### Which operating system is the customer using / looking to use?

* Windows: the supported versions are 7, 8 and 10 (both 32-bit and 64-bit)

* OS X (Mac): the customer will need to run Windows in a VM (Virtual Machine). See the docs for [OS X Integration](./osx-integration.md). **Minimum system requirements**: 8GB of memory, 30GB of available disk space and at least 4 processor cores

### What type of payment terminal solution does the customer currently have?

* None: ask which one of the [supported terminals](#supported-payment-terminals) the customer would be interested in. We will deal with ordering it from [Nets](#what-do-i-need-to-register-a-newexisting-customer-with-nets).

* Standalone or integrated:
  * If the terminal is **not** provided by Nets, ask the customer to terminate his/her current contract and proceed as above
  * If the terminal is **not** one that we support, proceed as above (ask the customer to fill in the [terminal return form](../assets/palautusilmoitus.pdf).

### What do I need to register a (new/existing) customer with Nets?

* 🇫🇮 [Nets registration form](https://www.signom.com/nets/PowerForm.signom?powerFormId=NETS_FINLAND_HELPER) (online, see the [instructions](../assets/nets_registration_form.pdf) for help)
* 🇸🇪 [Nets registration form](../assets/bestallningsblankett.pdf) (PDF)

Information you will need...

* seller number: `108398`
* password: `n87y62`
* [customer contact/business information](https://timma.fi/hallinta)
* device software version: `Viking`
* cables: `USB` and `Ethernet`
* additional information: "Payment terminal version 4.82 required"
* if the customer is returning his/her existing payment terminal: [payment terminal ID](#how-do-i-figure-out-the-id-of-the-payment-terminal) (or, for older payment terminals, the serial number found on a sticker at the bottom of the terminal device).

In case the customer's payment terminal is not running the [supported software version](#minimum-supported-terminal-version) you will need to contact Nets and ask them to enable the update. For this Nets will need the customer's business (organization) ID or the [payment terminal ID](#how-do-i-figure-out-the-id-of-the-payment-terminal). Any updates to the payment terminal software will be carried out during the [installation process](#installation-on-site--remote).

**NOTE** If the customer has an old iPP350/iCT250 terminal (e.g. the Lumo version), you will need to order him/her a new one (Viking version) using the registration form provided above. The customer will need to fill in a separate [terminal return form](../assets/palautusilmoitus.pdf) to process the return of the older terminal.

**NOTE** Customers should not have to pay any opening/new contract fees. If Nets sends them an invoice about this, tell them to contact us.

### What type of business is the customer running? Does it include multiple proprietorships or is everybody under a single company?

If the customer consists of multiple proprietorships, Nets will need to enable a so-called **multi-banking support** on the payment terminal. Mention this in the terminal order/registration form. **Let the dev team know about any multi-banking terminals!**

### Does the customer want reconciliation ("päivänpäätös") to be done automatically?

If so, inform Nets about this / mention this in the payment terminal registration form. **NOTE** Automatic reconciliation is only possible if the customer has...

* iCT250
* Ethernet cable (shipped together with the terminal)
* router/switch/Ethernet outlet the Ethernet can be connected to.

### What extra equipment & materials does the customer need?

* Receipt rolls (Nets ships only 2 along with the original packaging)
* Receipt printer (only necessary for iPP350)
  * [Star TSP100 drivers](https://goo.gl/xEvKuw) for Windows 10
* [USB A to USB B](../assets/images/usb-a-to-usb-b.jpg) cable (with a 90° angle). Request this in the registration form (or, if this'd be for an existing Nets customer, we can provide one from our own inventory)
* If the customer is running a Mac, a pre-packaged Windows on a USB stick (see [OS X Integration](./osx-integration.md) for more).


## Installation (on-site / remote)

### 1. Installing the Timma software

  * If the customer is running Windows, download the installer from [bit.ly/timma-for-windows](http://bit.ly/timma-for-windows) (either `32-bit` or `64-bit` architecture, check via `Search` -> `System` in Windows) and install Timma
  * If the customer is running OS X, see the docs for [OS X Integration](./osx-integration.md).

It might take up to 10 minutes for the installer to complete depending on the OS & existing software installed, so be patient.

### 2. Integrating the payment terminal with Timma

  1. First, if the customer's payment terminal is configured for multi-banking, see the [instructions on how to update the customer's & users Timma account information accordingly](./multibanking-terminal.md). Otherwise proceed to step 2.
  2. Change the terminal settings to [communicate with Timma via USB](#how-do-i-prepare-the-payment-terminal-to-be-connected-to-timma)
  3. Connect the payment terminal to the computer via a [USB A to USB B](../assets/images/usb-a-to-usb-b.jpg) cable (and if applicable, the Ethernet cable to a router/switch/Ethernet outlet)
    * If the customer is running Timma on a Mac (= inside a Virtual Machine) make sure the [USB connection is also shared from the host machine](../assets/images/share-host-usb.jpeg). Connect the USB via `Devices` -> `USB` -> `Sagem`
  4. Launch `Timma.exe` (should be available on the Desktop)
  5. Wait for the login screen to appear. If the connection was **not** successfully established, a [warning prompt should appear at the top of the application window](../assets/images/terminal-disconnected.jpeg). If this is the case, try [re-connecting the terminal to the computer](reconnecting-the-terminal.md).
  6. Check that everything works by navigating to the [Payment Terminal](../assets/images/payment_terminal_version.png) view and printing out e.g. a `Z-report`. For multi-banking customers, you should try this for each account (change accounts via the `Select account` drop-down on the Payment Terminal view)
  7. Done!

### 3. Checking the terminal software for updates

First, [check if the terminal version is outdated](#how-do-i-check-the-payment-terminal-version) and an update is required. If so, proceed with [updating the payment terminal software](#how-do-i-update-the-version-of-the-payment-terminal-software).

It should take ~5 minutes for the terminal to complete the update if its connected to the Internet via an Ethernet cable (vs ~30min via USB only). Once the update has been fetched, the terminal will restart itself (USB connection will be lost in the process).

## FAQ

### How do I figure out the ID of the payment terminal?

If you have installed Timma and configured, you can see the payment terminal ID via [Payment Terminal](../assets/images/payment_terminal_version.png) page. Otherwise, make sure power is turned on the terminal, swipe the merchant card on the terminal and select:

`6. Parameters` -> `1. Other` -> `1. Main Settings` (terminal ID should appear on the screen)

### How do I prepare the payment terminal to be connected to Timma?

Make sure power is turned on the terminal, swipe the merchant card on the terminal and select:

For iCT250 (with Ethernet access == Ethernet cable + router/switch/outlet to connect it to):

  * `6. Parameters` -> `1. Other`
    * `2. Connections` -> `Comm. type = Ethernet`
    * `3. Cashier` -> `Cashier = Yes`
    * `3. Cashier` -> `Comm. type = USB SLAVE`

For iPP350 (and iCT250 without Ethernet access):

  * `6. Parameters` -> `1. Other`
    * `2. Connections` -> `Comm. type = Cashier`
    * `3. Cashier` -> `Cashier = Yes`
    * `3. Cashier` -> `Comm. type = USB SLAVE`

Return to the default view by hitting the `Back` button (red) a couple of times. The terminal should now restart itself.

### How do I update the version of the payment terminal software?

Make sure power is turned on the terminal, swipe the merchant card on the terminal and select:

`8. Program` -> `1. Fetch Program`

If updates are available, this will download & install the latest software (rebooting the terminal in the process). If the payment terminal has the most up-to-date version of the software, it will print out the software version number. The version of the software should be at least that of the [abovementioned version](#minimum-supported-terminal-version). If you are unable to fetch the required update, contact Nets as they will need to set it available for download first.

### How do I check the software version of the payment terminal?

Open Timma and navigate to the [Payment Terminal](../assets/images/payment_terminal_version.png) view. The version is given as four numbers where e.g. `0481` would correspond to version `4.81`. If the version is lower than [what we currently support](#minimum-supported-terminal-version), you will need to [update the terminal software](#how-do-i-update-the-version-of-the-payment-terminal-software).

### The terminal refuses to print receipt/reports and displays an "unknown function" error. What to do?

The terminal is running an old software version. Update the terminal ([see above](#how-do-i-update-the-version-of-the-payment-terminal-software)).

### The customer does not want the merchant receipt to be printed automatically (iCT250). What to do?

`6. Parameters` -> `1. Other` -> `1. Function` -> `Always Copy = No`
