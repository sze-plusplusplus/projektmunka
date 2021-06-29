# Projektmunka I-II

![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/sze-plusplusplus/projektmunka?style=for-the-badge)
![Issue Tracking](https://img.shields.io/badge/YouTrack-Issues-blue?style=for-the-badge)

## Csapat:

| #   | Nev                     | Github         |
| --- | ----------------------- | -------------- |
| 1   | Karcag Tamás            | @karcagtamas   |
| 2   | Baranyai Bence Bendegúz | @bencebaranyai |
| 3   | Balogh Máté             | @Cerberuuus    |
| 4   | Tóth Róbert             |                |

## Konzulens: _Tüű-Szabó Boldizsár_

## A projekt tárgyát képező probléma és a projektcél rövid felvázolása:

> Fantázia név: **MeetHut**

Az általunk elképzelt alkalmazás egy egyetemi konferencia és oktatástámogató webalkalmazás, ami segítséget nyújthat egyes egyetemi előadások és szóbeli vizsgák lebonyolításában.

A tanárok lehetőséget kapnának saját szobák létrehozására, ahova meghívhatják a hallgatókat e-mail cím alapján, vagy megoszthatják velük a szoba címét. Ezáltal a hallgatók szabadon betudnának lépni az órára. A szoba rendelkezne kezdeti és vég időponttal. A szoba mérete szabadon állítható lesz. A szobák órarendbe rendezhetők és a kezdés időpontjában automatikus értesítéssel azonnali becsatlakozásra lesz lehetőség, illetve egy fő felületen látják az összes jövőbeli órájukat.

A tanórák szobái átállíthatók lennének szóbeli vizsga módra, ahol mindig a tanár és az aktuálisan vizsgázó hallgató lehetne bent a szobában, míg a többiek, a “folyosón” várnak.

Lehetőség lenne hang és videó kommunikációra (webrtc) a beszélgetés lebonyolításához és írásbeli üzenetküldésre (websocket) az egyéb információk átadására. A felhasználók a beszélgetésben némíthatják magukat és kikapcsolhatják a képmegosztást. A tanár képes lesz lenémítani a hallgatókat akár egyesével, akár egyszerre mindenkit.

Biztosítanánk lehetőséget a beállítások során a tetszőleges mikrofon, hangszóró és webkamera kiválasztására.

## A projektterv részletesebb kidolgozása:

### Feladatok:

#### Szobák kezelése:

Szobák (azaz konferenciahívások) létrehozása, akár kurzushoz, akár függetlenül. A szobák azonnal létrehozhatók, vagy adott időpontra időzíthetők. A szobák megjelennek az órarendben.

A szobákhoz lehet felhasználókat hozzá rendelni, vagy e-mail alapján meghívni másokat.

A szoba szabadon lezárható, illetve a jogosultságok a létrehozó által szabadon szerkeszthetők.

#### Felhasználók kezelése:

Központi oldal, mely megfelelő jogosultsággal elérhető két módban.

- Adminisztrátori kezelés: Minden felhasználó kezelése, módosítása, csoporthoz rendelése.
- Kurzus nézet: A kurzushoz tartozó felhasználók kezelése, szobákhoz rendelése (órarend kezelés).

#### Belépés/Regisztráció/Felügyelet:

Szimpla bejelentkező, hitelesítő felület, mely később külső hitelesítőszolgáltatókkal is akár összeköthető, hogy központi beléptetőrendszerrel működhessen. Opcionálisan bekapcsolható kétlépcsős beléptetési módként elérhető a TOTP, az alkalmazás biztonságos kezelhetősége érdekében.
A felügyelhetőség érdekében audit log-ok kerülnek mentésre, amelyek alapján a felhasználók saját tevékenységüket, az oktatók hallgatóik (szűrt) tevékenységét ellenőrizhetik (katalógus).

#### Órarend:

Egy órarend elérhető a felhasználók számára, amin keresztül szabadon áttekinthetik az általuk létrehozott, illetve azokat az eseményeket, ahová meg lett hívva az adott felhasználó. Ezek bekerülnek egy órarendbe vagy naptárba, ahol ezeknek az elemeknek a részletei megtekinthetők, illetve megjelenik az esemény időpontja előtt egy bejelentkezési lehetőség.

#### Szoba - szóbeli mód:

A szoba beállítási között funkcióként megjelenik egy szóbeli mód, aminek köszönhetően, csak a megfelelő joggal belépő felhasználók léphetnek be videóhívásba szabadon, és mindenki más egy sorban jelenik meg, ahol a bent levő kezelők szabadon hívhatják vagy a következőt, vagy egy általuk kiválasztott embert.

#### Konferencia hang/videó:

Hang, videó kapcsolat lebonyolítása webrtc segítségével, megfelelő autentikációs és jogosultságkezelési folyamatokkal ellátva. Több kliens közötti kapcsolat megvalósítása MCU topológia szerint, központi szerveren történő átjátszással (média szerveren)

#### Konferencia felület:

Felület a terv alapján, képek elrendezése több tag esetén, mobilon is megfelelő, reszponzív kinézet elkészítése.

#### Konferencia képernyő megosztás:

A kliens képernyőjéből képzett kép továbbítása, amelyet a böngésző választóablaka segítségével a felhasználó megadhat. A felhasználónak egy megosztásra van lehetősége és egy szobában egyszerre egy aktív megosztás lehet, amely kiemelésre kerül.

#### Konferencia chat:

A szobákhoz kapcsolódóan szöveges csevegés funkció megvalósítása melynek a kommunikációja websocket kapcsolaton keresztül történik. Az üzenetek nem kerülnek megőrzésre a szoba használatán kívül, így csak ideiglenes tárolóban kerülnek eltárolásra. Minden felhasználó csak azokat az üzeneteket látja, amiket azóta küldtek, hogy ő a megbeszélés résztvevője lett.

#### Konferencia beállítások - némítás, tagok kezelése, megosztás átvétele:

A konferenciahívások fontos része a mikrofon és kamera ki-/bekapcsolása. A felhasználók a saját beállításaikat módosíthatják, valamint megfelelő joggal, kikapcsolni mások eszközeit is lehetséges (bekapcsolni adatvédelmi okokból nem). Hasonló módon esetlegesen ki is lehet dobni egy-egy résztvevőt.

#### Konferencia eszköz beállítások:

Lehetőség, hogy a felhasználók kiválaszthassák, mely bemeneti/kimeneti eszközöket szeretnék a hívások részeként használni, grafikus felületen. Esetlegesen lehetőség a megjelenített felhasználó számán, a megosztott képernyő méretén állítani.

#### GUI tervek elkészítése:

Fontos része a feladatnak a felhasználói felületek megtervezése. Előzetesen szeretnénk kitalálni, hogy körvonalakban, különböző méreteken, milyen szerkezeti ábrát szeretnénk majd a felhasználók elé tárni.

#### Projekt inicializáció, alapok lefektetése:

A feladat részeként szeretnénk a projekt alapjainak lefektetését. Elkészíteni az előzetes dokumentációkat, illetve projekt alapjait is itt kezdenénk el. Ekkor már egy Git szerveren készítenénk el a projekt alapvető kódjait, amin a későbbiek folyamán elkezdhetünk dolgozni.

### Mérföldkövek:

| #   | Info                                        |
| --- | ------------------------------------------- |
| M0  | Projekt inicializáció                       |
| M1  | GUI tervek                                  |
| M2  | Konferencia alapok, Felhasználók és szobák  |
| M3  | Konferencia GUI, Órarend, Szóba szóbeli mód |
| M4  | Konferencia beállítások és további eszközök |
| M5  | Fejlesztői és felhasználói dokumentációk    |
| M5  | Tesztelés                                   |

### Eredménytermékek:

A végleges alkalmazás a központi felületek, az órarend tervező és a videó konferencia felület együttes integrációjából fog összeállni.

### Elvárt hasznok:

Egy gyors és gördülékeny webalkalmazás, amely támogatja az előadók és hallgatók online előadások lebonyolítását és az azokon való részvételt. Továbbá a résztvevők időmenedzsment-jét, azzal, hogy gyors belépést biztosít a konferenciákra, órákra, s a szóbeli vizsgáztatás bonyodalmait is leegyszerűsíti mindkét oldal számára.

### Projektszervezet:

| #   | Feladat, Funkcionalitás                   | Mérföldkő | Előfeltétel | Megbízott(ak)       |
| --- | ----------------------------------------- | --------- | ----------- | ------------------- |
| T0  | Projekt inicializáció, alapok lefektetése | M0        | -           | Csapat              |
| T1  | GUI tervek elkészítése (wireframe)        | M1        | -           | T. Róbert, B. Máté  |
| T2  | Szobák kezelése                           | M2        | T3, T1      | B. Bence, T. Róbert |
| T3  | Felhasználók kezelése                     | M2        | T0, T1      | K. Tamás, B. Máté   |
| T4  | Belépés / Regisztráció / Felügyelet       | M2        | T3, T1      | K. Tamás, T. Róbert |
| T5  | Órarend                                   | M3        | T2          | K. Tamás, T. Róbert |
| T6  | Szoba szóbeli mód                         | M3        | T2, T5      | K. Tamás, B. Máté   |
| T7  | Konferencia hang/videó                    | M2        | T0          | K. Tamás, B. Bence  |
| T8  | Konferencia felület                       | M3        | T2, T7      | T. Róbert, B. Máté  |
| T9  | Konferencia - képernyő megosztás          | M4        | T8          | K. Tamás, B. Bence  |
| T10 | Konferencia - chat                        | M3        | T2, T8      | T. Róbert, B. Bence |
| T11 | Konferencia beállítások - (mások) némítás | M4        | T8          | B. Máté, B. Bence   |
| T12 | Konferencia eszköz beállítások            | M4        | T8          | B. Máté, B. Bence   |

### Munkafolyamat:

- A forrás _GitHub_-on, a +++ (https://github.com/sze-plusplusplus) szervezetben található meg, írási joggal a csapat tagjai rendelkeznek
- A hibajegyek / feladatok kezelése _YouTrack_-en (https://plusplusplus.myjetbrains.com) történik.
- Aki, amin dolgozik azon _Assignee_-ként kell szerepelnie és az állapotot állítani kell.
- A **main** branch védett és nem lehet rá commit-olni.
- A _Pull request_-ek elfogadásához minimum 1 _approve_ kell a csapat más tagjaitól.
- A szerző nem mergelheti saját kódját.
- A _Pull request_-eknek hivatkoznia kell a _YouTrack issue_-ra, issue nélkül nem szabad _Pull request_-et létrehozni.

### Program szerkezeti ábra (terv):

### Modulok:

Az kitalált modulokhoz egy felelőst rendeltünk a csapatból, aki azért a területért felel és a rajta működő csapatmunkát összefogja és ellenőrzi a kódokat, bizonyos szemléletek alapján, hogy konzekvens maradhasson a jövőben. Ez a személy lesz az, aki az adott területet átfogóan ismeri és felel is érte.

A táblázat arra is tartalmaz ötleteket, hogy az egyes modulok milyen programnyelveken vagy környezetnek legyenek megvalósítva.

| Modul          | Felelős   |
| -------------- | --------- |
| Web Frontend   | B. Máté   |
| Media Frontend | T. Róbert |
| Web Backend    | K. Tamás  |
| Media Backend  | B. Bence  |

## Környezet ötletek:

- FrontEnd: JS Framework, Libs – pl. Angular, React, Vue
- BackEnd: 1 (for everything) or 2 (lightweight media and web)
  - Pl. C#, go, nodets, Java, C++, Python, Elixir, Scala, Kotlin, Dart, F#
  - Tomi ötlet: ASP.NET (C#), Spring (Kotlin), NestJs (JS), Go – WebBackend
  - Bence ötlet: Go (ent + gin + gorilla/websocket + pion/webrtc), Node (socket.io + mediasoup)
- Adatbázis: SQL, SQL with ORM, NoSql
