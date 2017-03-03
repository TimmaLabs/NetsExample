To set up a multi-banking terminal you will need to add a so called **Nets ID** for each (Nets) user account via https://timma.fi/hallinta. Begin by swiping the merchant card on the terminal and selecting an account. For each account select `1. Cards` -> `2. Print` (from the `Main menu` / `Adminmeny` / `Kauppiasmenu`)

This will print out a [receipt with information about the given Nets account](../assets/images/payment_terminal_accounts..jpg). For each receipt, do the following

  1. Make a note about the account ID (`<8-digit-terminal-id>-<6-digit-merchant-id>`), highlighted in red in the aforementioned image
  2. Hit the red button and swipe the merchant card on the terminal again to select the same account. This time make a note about the [2-digit ID on the display](../assets/images/nets-account.jpeg) (aka _OperID_)
  3. Compose a **Nets ID** from the abovementioned IDs in the following format: `<6-digit-merchant-id>-<8-digit-terminal-id>-<4-digit-zero-padded-oper-id>`, e.g. `721342-72134201-0003`
  4. Associate the composed **Nets ID** with the corresponding user via https://timma.fi/hallinta
    * salon's Nets ID should be set to that of the main user (typically, where _OperID_ is `0001`)
    * if a user's Nets ID field is left empty, the salon's Nets ID (if any) will be used instead

If the Nets ID is misconfigured, you'll see an `Invalid merchant ID` (`Ovanligt användare ID`) error upon printing/processing a payment.

In most cases, it makes sense to disable a so called **double control** for multi-banking terminals:

1. Swipe the merchant card and select any account
2. From the merchant menu, select `6. Parameters / Kommunikation` -> `1. Other / Ändra` -> `1. Function` -> `Double control = No`. After changing the value, hit the red _Stop_ button to return to the welcome screen. The terminal will now restart.
