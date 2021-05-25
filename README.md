# RealEstateProject

Autorzy:
Juliusz Mandrosz
Rafał Łazarski

Temat projektu: Agencja nieruchomości

Aplikacja napisana w C# przy użyciu Visual Studio.

UWAGA:
Diagram UML został przygotowany w Visual Studio i znajduje się on w plikach programu pod nazwą DiagramUML.cd. Przy otworzeniu programu w Visual Studio powinien być on dostępny do otworzenia w "Solution Explorer".

DANE DO LOGOWANIA DLA ADMINSTRATORA:
Login: admin
Hasło: admin

Istotne informacje do użytkowania z aplikacji: Administrator jest tylko jeden i jest on użytkownikiem, którego konto powstało na samym początku. W razie wyczyszczenia pliku "UsersList" należy założyć pierwsze konto dla administratora w rejestracji użytkowników. Inne konta nie będą miały uprawnień administratora.

Instrukcja użytkowania:

1) Logowanie i zakładanie konta:

a) Logowanie odbywa się przy wpisaniu loginu i hasła zarejestrowanego użytkownika (należy wcisnąć przycisk "Zaloguj")

b) W przypadku braku konta należy kliknąć "Nie mam jeszcze konta, zarejestruj mnie!" i uzupełnić login, hasło, imię i nazwisko oraz kliknąć przycisk: "Zarejestruj się" lub przycisk "Mam już konto, zaloguj mnie!" w przypadku zaniechania utworzenia konta.

2) Administrator:

a) W zakładce "Oferty" może przeglądać, filtrować nieruchomości (filtry można zapisać używając przycisku "zapisz preferencje", dzięki czemu po ponownym zalogowaniu nadal będą one widoczne) i wchodzić w ich szczegóły (należy zaznaczyć działkę i kliknąć przycisk "zobacz szczegóły"). Administrator po wejściu w szczegóły może w oddzielnym okienku usuwać nieruchomość z listy. W "Ofertach" administrator może także dodawać nieruchomości przy pomocy panelu z boku ekranu.

b) W zakładce "Moje konto" można zmienić login i hasło, zobaczyć swoje aktualne dane i się wylogowywać. Można także akceptować oferty zakupu nieruchomości przez użytkowników lub je odrzucać.

c) W zakładce "Panel administratora" możemy przeglądać użytkowników i ich banować (można także zobaczyć już zbanowanych użytkowników)


3) Zwykły użytkownik:

a) W zakładce "Oferty" może przeglądać, filtrować nieruchomości (filtry można zapisać używając przycisku "zapisz preferencje", dzięki czemu po ponownym zalogowaniu nadal będą one widoczne) i wchodzić w ich szczegóły (należy zaznaczyć działkę i kliknąć przycisk "zobacz szczegóły"). Po wejściu w szczegóły może w oddzielnym okienku zarezerwować nieruchomość do kupna. Kupno działki będzie musiał potwierdzić administrator, do tego momentu, działka nie zostanie w pełni zakupiona.

b) W zakładce "Moje konto" użytkownik może zobaczyć wszystkie swoje dane i zmienić login i hasło oraz może się wylogować. Ma także on wgląd w kupione nieruchomości i ich szczegóły.

c) W zakładce "Skrzynka pocztowa" użytkownik dostaje wiadomości za każdym razem gdy jego wniosek o zakupienie działki zostaje przyjęty lub odrzucony.
