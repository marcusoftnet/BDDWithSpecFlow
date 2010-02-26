#language: se
Egenskap: Lista filmer
	För att få en överblick av min samling
	som filmsamlare 
	vill jag kunna lista alla filmer i mitt bibliotek samt se totala antal filmer 

Scenario: "Alien", "E.T." och "Jaws" i filmsamlingen
	Givet att filmsamlingen innehåller "Alien", "E.T.", "Jaws"
	När jag anger kommando: ListaFilmer
	Så ska resultatet vara:
	| Rad |
	| Alien |
	| E.T. |
	| Jaws |
	| Du har 3 filmer i samlingen |