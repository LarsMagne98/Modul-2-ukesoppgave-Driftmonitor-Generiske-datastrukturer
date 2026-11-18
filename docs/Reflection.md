# Oppgave 0

## b)

Total antall: 10_000_000
Saved antall: 10_000
Tid: 11_523 ms
Minnebruk: 1 MB

## c)

Det som skjer med de 9_999 andre elementene er at List<T>.RemoveAt(index)
bruker metoden Array.Copy for å tette hullet som oppstår på index 0.
Elementet på index 1 kopieres til index 0, index 2 til index 1, osv helt
til 9_999. Dette gir en tidskompleksitet på O(n) per sletting.

Hvor mange ganger skjer det i løpet av løkken? 10_000_000 - 10_000 =
9_990_000.
det betyr at forskyvningen av de 9_999 elementene skjer 9_990_000 ganger.
Totalt gjennom hele løkken utfører programmet altså 9_990_000 \* 9_999 =
99_890_010_000 referansekopieringer. (Nesten 100 Milliarder)!

# Oppgave 1

Tiden gikk ned til ~580 ms.

## Endring fra int til string:

I Program.cs: Måtte bare endre "RingBuffer<int>" til "RingBuffer<string>", og hva som ble lagt til i listen.
Endret ingenting i RingBuffer<T>.

# Oppgave 2

Tiden gikk ned til ~100 ms.
Minnebruk til 0 MB.

# Oppgave 3

Mye Ai hjelp til DataGeneratoren.

# Oppgave 4

-

# Oppgave 5

RingBuffer<SensorReading> implementerer IEnumerable<SensorReading>, derfor kan LINQ gå gjennom målingene. Average bruker Value fra hver måling til å beregne gjennomsnittet. Jeg sjekker først om bufferen er tom, fordi Average ellers ikke kan beregne et gjennomsnitt.
