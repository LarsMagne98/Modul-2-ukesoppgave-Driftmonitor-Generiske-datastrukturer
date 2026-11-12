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
