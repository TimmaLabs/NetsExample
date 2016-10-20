# Setup

  0. See [README.md](./README.md) on how to configure the USB connection
  1. Connect the terminal via USB and launch `Timma.exe`
  2. Login credentials: `harri@timma.fi` / `timma`

# Flows

## Maksu

  1. Luo uusi aika klikkaamalla tyhjää kohtaa _Kalenteri_-näkymästä
  2. Klikkaa `Maksuun` aukeavasta modaalista
  3. Asiakkaan maksun käsittelemiseksi klikkaa `Vahvista maksu`
    * palvelun hintaa ja ALVia voit säätää kohdista `Alv %` ja `Hinta/kpl`
  4. Suorita maksu maksupäätteellä

  Virhetapauksessa maksupääte tulostaa virhekuitin ja sovellusikkunassa näytetään virheilmoitus.
  Näin menetellään myös, jos maksun vahvistuksen aikana klikataan `Peruuta`-painiketta

## Maksun mitätöinti/palautus

  1. Suorita maksu kohdan [Maksu](#maksu) mukaisesti
  2. Maksun vahvistuksen jälkeen, (edeltäneen) maksutapahtuman mitätöinti on mahdollista suorittaa `Mitätöi maksu`-painikkeella

## Maksun hyvitys

  1. Valitse kalenterista jo maksuun viety aika (merkittynä `€`-merkillä)
  2. Valitse `Tee hyvitys` aukeavasta modaalista
  3. Valitse `OK`, minkä jälkeen maksupäätteessä tulisi näkyä `Kortti`-kentän ilmoittama summa
  4. Suorita hyvitys maksupäätteellä

## Päiväraportin (päivänpäätöksen) tulostaminen

  1. Valitse _Tulot_-sivu sivunavigaatiosta
  2. Valitse `Maksupääteraportti` tulotaulukon oikeasta yläkulmasta
