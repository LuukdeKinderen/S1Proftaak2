# Proftaak2

## Project omschrijving
We gaan de drempel om voor het eerst de trein te nemen verlagen. Dit doen we door een simulatie van de trein te maken zodat gebruikers kunnen oefenen hoe de trein werkt. Voor de simulatie zijn ledstrips op tafel aangebracht. Deze ledstrips moeten een treinrails voorstellen. Halverwege de ledstrips zijn een aantal stations te vinden. je kunt inchecken als een trein op het station staat. je kunt weer uitchecken als de trein op een ander station staat. Het in en uitchecken gebeurd met een NFC reader. Er wordt in een database bijgehouden hoeveel saldo je hebt en afhankelijk van hoe ver je hebt gereisd wordt een bedrag afgeschreven van je chip. 

## Hoe werkt het
Het systeem werkt als volgt: Op een computer word in een losse C# applicatie berekend hoe de treinen moeten reizen. Daarna wordt in datzelfde programma berekend hoe de ledstrip er uit moet komen te zien. Dit wordt vervolgens via Seriële communicatie naar een Arduino gestuurd. Deze Arduino zorgt er vervolgens voor dat de ledstrip er uit komt te zien zoals de C# applicatie heeft berekend.
Daarnaast kan de Arduino het hele programma stil leggen als hij een NFC kaart detecteert. Dan kan de gebruiker een station selecteren om in-/ uit te checken. Of zijn saldo opwaarderen.  

## Bekijk de video!
[![Bekijk de video](/Logo.png)](https://youtu.be/H14BrgL_wlc)