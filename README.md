# Project .NET Framework

* Naam: Kobe Ponet
* Studentennummer: 0160514-76
* Academiejaar: 23-24
* Klasgroep: INF203A
* Onderwerp: Magic the gathering -> Set * - * Card * - * CardType

## Sprint 3


## Sprint 4
```mermaid
classDiagram
    class Card
    class CardType
    class CardColour
    class CardAbility
    class Set
    class SetEntry
    class Deck
    class DeckEntry
    
    Card "1" -- "1" CardType
    Card "1" -- "1" CardColour
    Card "1" -- "1" CardAbility
    Card "*" -- "1" SetEntry
    Card "*" -- "1" DeckEntry
    Set "*" -- "1" SetEntry
    Deck "*" -- "1" DeckEntry
```