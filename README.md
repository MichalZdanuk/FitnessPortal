# FitnessPortal
Projekt realizowany w ramach przedmiotu *"Projekt Dyplomowy - 1DI1633"* na Politechnice Warszawskiej w semestrze 2023L. Tematem projektu jest zaimplementowanie w pełni funkcjonalnego portalu typu fitness (postawienie bazy, zbudowanie oraz wystawienie API oraz zaimplementowanie aplikacji klienckiej, która łączy się z API).
***
## Projekt graficzny - FIGMA:
Projekt Figma został wykonany na samym początku projektu, celem ułatwienia sobie implementowania FrontEnd'u. Wykorzystałem podstawowe dobre praktyki projektowania interfejsów użytkownika: prawo Fitsa, liczba Mullera, czy spójność wewnątrz aplikacji. Metodyka jaką kierowałem się projektując interfejs to **Atomic Design** - więcej można o niej poczytać tutaj:

*https://atomicdesign.bradfrost.com/chapter-2/*

![atomic_design](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/0bfba533-3dd1-444e-aa3f-0757012f5bbe)

Link do projektu Figma (*UWAGA: Projekt Figma obejmuje tylko design na urządzenia desktop*):

*https://www.figma.com/file/Gbzk6fX0RZ585CyDuZ9sMN/PortalFitness?type=design&node-id=0-1&t=eWWCsW8vmUPUCj6D-0*

![figma](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/41c639ae-5367-4fce-9bba-f0e506e658f8)

## Technologie:
### BackEnd:
* technologia C# .NET(6.0) - ASP.NET Core Web API
* Swagger
* JWT
* FluentValidation
* EntityFramework + MSSql
* MSUnit, NSubstitute, Shouldly
### FrontEnd:
* React (m.in. react-router-dom, recharts, jwt-decode, lodash) - Vite
* axios
* localstorage
* JavaScript + Bootstrap
* ESLint, prettier
## Wymagania (wstępnie zdefiniowane funkcjonalności):
* rejestracja/logowanie
* uaktualnianie danych konta (wiąże się to z generowaniem nowego tokena JWT; zaimplementowana blacklist'a na stare token'y)
* dodawanie własnych wpisów/przeglądanie wpisów innych
* wysyłanie zaproszeń do znajomych, akceptacja/odrzucanie zaproszeń, usuwanie znajomych, przeglądanie profili znajomych
* rejestracja swoich treningów, przegląd histori treningów (podsumowanie ile mamy aktywności fiz. w zadanym przedziale czas itp.)
* analiza swojego progresu na podtawie wykresów tworzonych na bazie zarejestrowanych treningów
* proste kalkulatory: kalkulator BMI, kalkulator zapotrzebowania dziennego na kalorie BMR, kalkulator poziomu tkanki tłuszczowej na podstawie podanych obwodów ciała
* opisy różnych ćwiczeń/aktywności fizycznych - (statyczny) leksykon ćwiczeń
* wysoka responsywność na urządzenia mobile oraz desktop
## Możliwy rozwój aplikacji(funkcjonalności, o które chciałbym wzbogacić aplikację):
* dostosowanie aplikacji front'endowej pod urządzenia typu mobile (responsywność) **(zrobione)**
* dodanie pipeline CI celeme bezpiecznego wprowadzania kodu do repozytorium **(zrobione)**
* paginacja rezultatów **(zrobione)**
* obsługa odpowiedzi od API do po stronie frontend zgodne z praktykami UX - dodanie spinner'ów przy ładowaniu odpowiedzi, zwrócenie komunikatu błędy w przypadku braku połączenia/wysłania
  błędnych danych  **(zrobione)**
* dodawanie komentarzy do artykułów, ocena komentarzy poprzez "łapkę" w górę/dół
* dwujęzyczność portalu - Angielski/Polski (w tym zapisanie po stronie backEnd'u preferencji użytkownika, by po zalogowaniu posiadał stan taki jaki zostawił wylogowując się z aplikacji)
* mechanizm proponowania nowych obciążeń - prognozowanie progresu w ćwiczeniach na podstawie wykonanych już treningów
* konteryzacja rozwiązania przy użyciu Docker'a
## Testowanie aplikacji
### BackEnd
* testy jednostkowe - sprawdziłem poprawność funkcjonowania narzędzia kalkulatora, validatorów oraz przede wszystkim serwisów (wykorzystane biblioteki *MSUnit*, *NSubstitute*, *Shouldly*)
  stosując przy tym konwencję podziału testu AAA (Arrange-Act-Assert)
![tests_results](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/4ee6bc39-2e08-4213-bc74-e96297e3884e)

* testy manualne - testy wykonywane podczas całego okresu powstawania projektu, ręcznie przetestowałem flow akcji udostępnianych przez REST API przy użyciu zarówno Swagger'a jak i Postman'a
![postman](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/1d42e008-d2e7-433f-99f6-8a4e17828adf)


### FrontEnd
* samodzielne ręczne przetestowanie aplikacji poprzez uruchomienie na ekranach różnej rozdzielczości, uruchiomienie w przeglądarce jako tryb mobile
* przekazanie aplikacji do niewielkiej grupy osób trzecich celem sprawdzenia intuicyjności aplikacji oraz poznania opinii na temat design'u

## Continuous Integration:
Zdecydowałem się w tym repozytorium na wykorzystanie Github Actions do stworzenia **pipeline'a CI**. Dwa proste pliki *api-build-test.yml* oraz *frontend-lint-build.yml* nakładają restrykcje, dzięki którym kod przed wdrożeniem na branch main jest sprawdzany na backend'zie poda kątem budowy projektu oraz uzyskania pozytywnych wyników testów jednostkowych, a
na frontend'zie pod względem formatowania kodu oraz budowy plików projektowych.

![CI](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/73b278a0-f93e-4a1c-b9b3-5b13c9843f66)

## REST API

* ### Autoryzacja poprzez token JWT:
![autoryzacja](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/b750f1e4-5281-4668-b01a-248eed7fe2c0)

* ### Account Controller (rejestracja, logowanie, aktualizacja profilu, przeglądanie profilu):
![account_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/4a64fdea-2182-4e17-8254-f6a1cc462b74)

* ### Article Controller (tworzenie, edycja, usuwanie, przeglądanie):
![article_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/e62682c8-a14a-4f2d-8c77-d71221731b03)

* ### Calculator Controller (wyliczanie, przeglądanie zapisanych wyników BMI):
![calculator_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/b24923bc-334d-4b13-884f-96550d61f270)

* ### FriendShip Controller (wysyłanie/akceptowanie/odrzucanie zaproszeń, przeglądanie/usuwanie znajomych):
![friendship_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/9f179bb2-2dd6-49d1-bf74-4023886a43b3)

* ### Training Controller (dodawanie treningów i przegląd, przegląd ulubionych(na podstawie częstotliwości), możliwość filtrowania treningów ze względu na okres czasowy):
![training_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/f16db5a7-313c-4213-afbe-54aa8ce4b666)


## Demo:
* strona główna

 ![main_page_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/0f1173c2-ab2d-4811-826b-e3948605c139)

* rejestracja

![register_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/02b0b290-cfd1-4eb3-80c6-c50a678f8587)

* logowanie

![login_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/b0ca513a-48b4-4bab-94eb-98c3749ec7ad)

* strona z artykułami

![articles_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/b35ab460-0acd-4a43-9222-b53be1103934)

* kalkulator BMI

![bmi_calc_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/c6c7c293-5268-468a-981c-ad9d77aad10a)

* kalkulator BMR

![bmr_calc_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/4a321b46-f3f9-4585-8283-efd6134404f8)

* panel treningowy

![training_page_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/9f573a21-8b62-4dde-8b7d-221d45fddac2)

* panel znajomi

![friends_page_gif](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/26b2e687-e4bd-489c-b236-f23ce6595fe0)
