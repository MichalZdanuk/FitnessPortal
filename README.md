# 💪 FitnessPortal
Projekt realizowany w ramach przedmiotu *"Projekt Dyplomowy - 1DI1633"* na Politechnice Warszawskiej w semestrze 2023L. Tematem projektu jest zaimplementowanie w pełni funkcjonalnego portalu typu fitness (postawienie bazy, zbudowanie oraz wystawienie API oraz zaimplementowanie aplikacji klienckiej, która łączy się z API).
***

## 🎨 Projekt graficzny - FIGMA:
Projekt Figma został wykonany na samym początku projektu, celem ułatwienia sobie implementowania FrontEnd'u. Wykorzystałem podstawowe dobre praktyki projektowania interfejsów użytkownika: prawo Fitsa, liczba Mullera, czy spójność wewnątrz aplikacji. Metodyka jaką kierowałem się projektując interfejs to **Atomic Design** - więcej można o niej poczytać tutaj:

*https://atomicdesign.bradfrost.com/chapter-2/*

![atomic_design](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/0bfba533-3dd1-444e-aa3f-0757012f5bbe)

Link do projektu Figma (*UWAGA: Projekt Figma obejmuje tylko design na urządzenia desktop*):

*https://www.figma.com/file/Gbzk6fX0RZ585CyDuZ9sMN/PortalFitness?type=design&node-id=0-1&t=eWWCsW8vmUPUCj6D-0*

![figma](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/41c639ae-5367-4fce-9bba-f0e506e658f8)

---

## 💻 Technologie:
### BackEnd:
* technologia C# .NET(6.0) - ASP.NET Core Web API
* Swagger
* JWT
* FluentValidation
* EntityFramework + MSSql
* MSUnit, NSubstitute, Shouldly, NetArchTests
### FrontEnd:
* React (m.in. react-router-dom, recharts, jwt-decode, lodash) - Vite
* axios
* localstorage
* JavaScript + Bootstrap
* ESLint, prettier

---

## 📜 Wymagania (wstępnie zdefiniowane funkcjonalności):
* rejestracja/logowanie
* uaktualnianie danych konta (wiąże się to z generowaniem nowego tokena JWT; zaimplementowana blacklist'a na stare token'y)
* dodawanie własnych wpisów/przeglądanie wpisów innych
* wysyłanie zaproszeń do znajomych, akceptacja/odrzucanie zaproszeń, usuwanie znajomych, przeglądanie profili znajomych
* rejestracja swoich treningów, przegląd histori treningów (podsumowanie ile mamy aktywności fiz. w zadanym przedziale czas itp.)
* analiza swojego progresu na podtawie wykresów tworzonych na bazie zarejestrowanych treningów
* proste kalkulatory: kalkulator BMI, kalkulator zapotrzebowania dziennego na kalorie BMR, kalkulator poziomu tkanki tłuszczowej na podstawie podanych obwodów ciała
* opisy różnych ćwiczeń/aktywności fizycznych - (statyczny) leksykon ćwiczeń
* wysoka responsywność na urządzenia mobile oraz desktop

---

## 🌱 Możliwy rozwój aplikacji (funkcjonalności, o które chciałbym wzbogacić aplikację):
* dostosowanie aplikacji front'endowej pod urządzenia typu mobile (responsywność) **(zrobione)**
* dodanie pipeline CI celeme bezpiecznego wprowadzania kodu do repozytorium **(zrobione)**
* paginacja rezultatów **(zrobione)**
* obsługa odpowiedzi od API do po stronie frontend zgodne z praktykami UX - dodanie spinner'ów przy ładowaniu odpowiedzi, zwrócenie komunikatu błędy w przypadku braku połączenia/wysłania
  błędnych danych  **(zrobione)**
* dodawanie komentarzy do artykułów, ocena komentarzy poprzez "łapkę" w górę/dół
* dwujęzyczność portalu - Angielski/Polski (w tym zapisanie po stronie backEnd'u preferencji użytkownika, by po zalogowaniu posiadał stan taki jaki zostawił wylogowując się z aplikacji)
* mechanizm proponowania nowych obciążeń - prognozowanie progresu w ćwiczeniach na podstawie wykonanych już treningów
* konteryzacja rozwiązania przy użyciu Docker'a **(zrobione)**

---

## 🧪 Testowanie aplikacji
### BackEnd
* *testy jednostkowe* - sprawdziłem poprawność funkcjonowania narzędzia kalkulatora, validatorów oraz przede wszystkim serwisów (wykorzystane biblioteki *MSUnit*, *NSubstitute*, *Shouldly*)
  stosując przy tym konwencję podziału testu AAA (Arrange-Act-Assert)
![tests_results](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/4ee6bc39-2e08-4213-bc74-e96297e3884e)

* *testy architektoniczne* - po czasie rozszerzyłem obszar testowania o poprawność architektury. Testy granualrnie sprawdzają, czy cały projekt zachowuje przyjęte konwencje skanując wszystkie pliki, następnie walidując, czy konkrtetne typy są odpowiednio rozdzielone (modularność) oraz czy spełniają konwencje (spełnianie reguł Clean Architecture). Również zostało zastosowana konwencja AAA
![archTests](https://github.com/user-attachments/assets/888d6c89-6cf6-4f0e-80b8-27610eddf237)

* *testy manualne* - testy wykonywane podczas całego okresu powstawania projektu, ręcznie przetestowałem flow akcji udostępnianych przez REST API przy użyciu zarówno Swagger'a jak i Postman'a
![postman](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/1d42e008-d2e7-433f-99f6-8a4e17828adf)


### FrontEnd
* samodzielne ręczne przetestowanie aplikacji poprzez uruchomienie na ekranach różnej rozdzielczości, uruchiomienie w przeglądarce jako tryb mobile
* przekazanie aplikacji do niewielkiej grupy osób trzecich celem sprawdzenia intuicyjności aplikacji oraz poznania opinii na temat design'u

---

## 🤖 Continuous Integration:
Zdecydowałem się w tym repozytorium na wykorzystanie Github Actions do stworzenia **pipeline'a CI**. Dwa proste pliki *api-build-test.yml* oraz *frontend-lint-build.yml* nakładają restrykcje, dzięki którym kod przed wdrożeniem na branch main jest sprawdzany na backend'zie poda kątem budowy projektu oraz uzyskania pozytywnych wyników testów jednostkowych i architektonicznych, a
na frontend'zie pod względem formatowania kodu oraz budowy plików projektowych.

![pipelines](https://github.com/user-attachments/assets/0e44faa7-3c82-4a48-a1cf-2e8eed6702fe)

---

## 🌐 REST API

* ### Autoryzacja poprzez token JWT:
![autoryzacja](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/b750f1e4-5281-4668-b01a-248eed7fe2c0)

* ### Account (rejestracja, logowanie, aktualizacja profilu, przeglądanie profilu):
![account_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/4a64fdea-2182-4e17-8254-f6a1cc462b74)

* ### Article (tworzenie, edycja, usuwanie, przeglądanie):
![article_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/e62682c8-a14a-4f2d-8c77-d71221731b03)

* ### Calculator (wyliczanie, przeglądanie zapisanych wyników BMI):
![calculator_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/b24923bc-334d-4b13-884f-96550d61f270)

* ### Friendship (wysyłanie/akceptowanie/odrzucanie zaproszeń, przeglądanie/usuwanie znajomych):
![friendship_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/9f179bb2-2dd6-49d1-bf74-4023886a43b3)

* ### Training (dodawanie treningów i przegląd, przegląd ulubionych(na podstawie częstotliwości), możliwość filtrowania treningów ze względu na okres czasowy):
![training_endpoints](https://github.com/MichalZdanuk/FitnessPortal/assets/76063659/f16db5a7-313c-4213-afbe-54aa8ce4b666)

---

## 🎥 Demo:
* strona główna

![homePage](https://github.com/user-attachments/assets/b7822d18-c7bf-4a7d-8e53-a8d76a7aabd3)

*Pierwszy punkt kontaktu użytkownika z aplikacją, przedstawia kluczowe funkcje i zachęca do rejestracji.*

* rejestracja
  
![register](https://github.com/user-attachments/assets/b37f168e-ef09-4250-ae38-c1ec59114d82)

*Umożliwia użytkownikom założenie konta, co jest niezbędne do personalizacji funkcji i przechowywania danych treningowych.*

* profil użytkownika

![profile](https://github.com/user-attachments/assets/dba85fc6-e4d5-48d3-9f70-36ce89316cce)

*Miejsce, w którym użytkownicy mogą zarządzać swoimi danymi.*

* sekcja "Friends"

![friends](https://github.com/user-attachments/assets/2e3424a4-d4f2-418b-a68c-a695ff1ccfb4)

*Funkcja wspierająca budowanie społeczności, umożliwiająca użytkownikom dodawanie znajomych i wzajemne motywowanie się.*

* sekcja "Articles"

![articles](https://github.com/user-attachments/assets/49ab76ed-e31f-47e9-9902-825f1ddc581f)

*Dostęp do artykułów związanych z treningiem, dietą i zdrowym stylem życia, pomagających w edukacji użytkowników.*

* sekcja "Exercises"

![exercises](https://github.com/user-attachments/assets/cbb6974b-19f7-44c8-947c-27543f13ec39)

*Baza ćwiczeń pozwalająca użytkownikom zaplanować i dostosować swoje treningi do indywidualnych potrzeb.*

* sekcja "Calcualtors"
  
![calcualtors](https://github.com/user-attachments/assets/332671ec-10bb-4555-ab5c-577ae7b4116b)

*Narzędzia do obliczania kalorii, zapotrzebowania na makroskładniki czy BMI, wspierające lepsze zrozumienie postępów.*

* dodawanie treningu

![addTraining](https://github.com/user-attachments/assets/1b1060a1-44b2-4d60-a9a9-acb6f59f1933)

*Pozwala użytkownikom zapisywać sesje treningowe, co zwiększa zaangażowanie i pozwala na dalsze śledzenie postępów.*

* analiza postępów treningowych

![trainings](https://github.com/user-attachments/assets/93627b38-0109-46e5-a074-50a6c87e056c)

*Funkcja prezentująca użytkownikowi szczegółowe dane dotyczące treningów, wspierając w monitorowaniu i optymalizacji planów.*

* śledzenie wyników BMI

![bmi](https://github.com/user-attachments/assets/ab04a7e4-a39f-4678-8cfc-326718ce5cb9)

*Pomaga użytkownikom w zrozumieniu zmian w ich składzie ciała i ich wpływu na zdrowie.*

---

## 🏃 Uruchamianie aplikacji:

Aplikację można uruchomić przy użyciu **Docker**, wykonując poniższe polecenia:

```bash
git clone https://github.com/MichalZdanuk/FitnessPortal.git
```
```bash
cd FitnessPortal/FitnessPortalBACKEND
```
```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up
```
