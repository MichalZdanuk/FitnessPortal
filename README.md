# FitnessPortal
Projekt realizowany w ramach przedmiotu *"Projekt Dyplomowy - 1DI1633"* na Politechnice Warszawskiej w semestrze 2023L. Tematem projektu jest zaimplementowanie w pełni funkcjonalnego portalu typu fitness (postawienie bazy, zbudowanie oraz wystawienie API oraz zaimplementowanie aplikacji klienckiej, która łączy się z API).
***
## Projekt graficzny - FIGMA:
Projekt Figma został wykonany na samym początku projektu, celem ułatwienia sobie implementowania FrontEnd'u. Wykorzystałem podstawowe dobre praktyki projektowania interfejsów użytkownika: prawo Fitsa, liczba Mullera, czy spójność wewnątrz aplikacji. Metodyka jaką kierowałem się projektując interfejs to **Atomic Design** - więcej można o niej poczytać tutaj:

*https://atomicdesign.bradfrost.com/chapter-2/*

Link do projektu Figma:

*https://www.figma.com/file/Gbzk6fX0RZ585CyDuZ9sMN/PortalFitness?type=design&node-id=0-1&t=eWWCsW8vmUPUCj6D-0*

## Technologie:
### BackEnd:
* technologia C# .NET(6.0) - ASP.NET Core Web API
* Swagger
* JWT
* FluentValidation
* EntityFramework + MSSql
### FrontEnd:
* React (m.in. react-router-dom, jwt-decode)
* axios
* localstorage
* JavaScript + Bootstrap
## Wymagania(wstępnie zdefiniowane funkcjonalności):
* rejestracja/logowanie
* dodawanie własnych wpisów/przeglądanie wpisów innych
* wysyłanie zaproszeń do znajomych, akceptacja/odrzucanie zaproszeń, usuwanie znajomych, przeglądanie profili znajomych
* rejestracja swoich treningów (podsumowanie ile mamy aktywności fiz. w zadanym przedziale czas itp.)
* mechanizm proponowania nowych obciążeń - prognozowanie progresu w ćwiczeniach na podstawie wykonanych już treningów
* proste kalkulatory: kalkulator BMI, kalkulator zapotrzebowania dziennego na kalorie BMR, kalkulator poziomu tkanki tłuszczowej na podstawie podanych obwodów ciała
* opisy różnych ćwiczeń/aktywności fizycznych - (statyczny) leksykon ćwiczeń
## Możliwy rozwój aplikacji(funkcjonalności, o które chciałbym wzbogacić aplikację):
* dodawanie komentarzy do artykułów, ocena komentarzy poprzez "łapkę" w górę/dół
* paginacja rezultatów **(zrobione)**
* dwujęzyczność portalu - Angielski/Polski (w tym zapisanie po stronie backEnd'u preferencji użytkownika, by po zalogowaniu posiadał stan taki jaki zostawił wylogowując się z aplikacji)
* dodanie analizy swoich treningów/wyników BMI poprzez przeglądanie wykresów prezentujących zmiany na przestrzeni czasu
 
## Demo:
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
