1. Hvorfor er RingBuffer<T> bedre enn en RingBuffer som lagrer object? Nevn minst to konkrete problemer den utypede varianten ville gitt deg i Oppgave 4.

   **Svar:**
   Med en RingBuffer<object> ville formatteringsfunksjonen fått hvert element som et object, og da kan den ikke bruke Time, Level eller Message direkte. Jeg måtte ha konvertert hvert element til LogEntry. Hvis bufferen inneholdt en annen objekttype, kunne konverteringen feilet under kjøring.

2. Hva skjer hvis du kaller Oldest på et tomt buffer, og du hadde returnert default(T) i stedet for å kaste et unntak? Svar for både RingBuffer<int> og RingBuffer<LogEntry>.

   **Svar:**
   For int er default(int) 0. Siden 0 også kan være en gyldig verdi, kunne jeg ikke vite om bufferen var tom eller faktisk inneholdt 0. LogEntry er en referansetype, så default(LogEntry) er null. Hvis jeg prøvde å bruke egenskapene til resultatet, kunne programmet feile med NullReferenceException.

3. new T[capacity] kompilerer inne i klassen, men new T() gjør det ikke. Hvorfor? (Søk opp where T : new() når du har tenkt gjennom det selv.)

   **Svar:**
   Når jeg oppretter en array av T, reserveres det plass til capacity, og hvert element får standardverdien for T. Det krever ikke at kompilatoren vet hvordan den skal lage et nytt T-objekt. new T() prøver derimot å kalle konstruktøren til T.

4. Hvor mange linjer i RingBuffer<T> måtte du endre da du tok den i bruk mot den tredje modellen i Oppgave 3?

   **Svar:**
   Ingen linjer som jeg husker.

5. Hva ville skjedd med gjenbruksverdien til datastrukturen hvis DataStructures-prosjektet hadde hatt en prosjektreferanse til modellene?

   **Svar:**
   Datastrukturen ville blitt koblet til prosjektet som definerer modellene. Andre prosjekter måtte også referere til det prosjektet for å bruke bufferen, selv om de bare trengte RingBuffer<T>. Det ville gjort datastrukturen mindre selvstendig og vanskeligere å gjenbruke med andre typer og i andre prosjekter.
