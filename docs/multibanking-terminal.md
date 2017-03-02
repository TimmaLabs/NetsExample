To set up a multi-banking terminal you will need to add a so called **Nets ID** for each Nets user account via https://timma.fi/hallinta. Proceed as follows:

  1. Make sure you have connected the terminal to Timma via USB as described in the [integration instructions](./deployment-checklist.md#2-integrating-the-payment-terminal-with-timma)
  2. Open Timma and navigate to the [Payment Terminal](../assets/images/payment_terminal_version.png) view
  3. Print out the payment terminal account information via the _Print information_ button.

This will print out a [receipt with information about each Nets account](../assets/images/payment_terminal_accounts..jpg) configured for the terminal. For each account entry in the receipt, do the following

  1. Make a note about the account ID on the receipt (`<8-digit-terminal-id>-<6-digit-merchant-id>`), highlighted with a red rectangle in the aforementioned image
  2. Swipe the merchant card on the terminal and select the corresponding account. Make a note about the [2-digit ID on the display](../assets/images/nets-account.jpeg) (aka _OperID_)
  3. Compose a **Nets ID** from the abovementioned IDs in the following format: `<6-digit-merchant-id>-<8-digit-terminal-id>-<4-digit-zero-padded-oper-id>`, e.g. `721342-72134201-0003`
  4. Associate the composed **Nets ID** with the given user via https://timma.fi/hallinta
    * if the user's Nets ID field is left empty, the salon's Nets ID (if any) will be used instead
    * salon's Nets ID should be set to that of the main user (typically, where _OperID_ is `0001`)

If the Nets ID is misconfigured, you'll see an `Invalid merchant ID` (`Ovanligt användare ID`) error upon printing/processing a payment.

In most cases, it makes sense to disable a so called **double control** for multi-banking terminals:

1. Swipe the merchant card and select any account
2. From the merchant menu, select `6. Parameters / Kommunikation` -> `1. Other / Ändra` -> `1. Function` -> `Double control = No`. After changing the value, hit the red _Stop_ button to return to the welcome screen. The terminal will now restart.
