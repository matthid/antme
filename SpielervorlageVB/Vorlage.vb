Imports System.Collections.Generic

Imports AntMe.Deutsch

' Füge hier hinter AntMe.Spieler einen Punkt und deinen Namen ohne Leerzeichen 
' ein! Zum Beispiel "AntMe.Spieler.AntonMüller". 
Namespace AntMe.Spieler

	' Das Spieler-Attribut erlaubt das Festlegen des Volk-Names und von Vor-
	' und Nachname des Spielers. Der Volk-Name muß zugewiesen werden, sonst wird
	' das Volk nicht gefunden.
	'
	' Das Typ-Attribut erlaubt das Ändern der Ameisen-Eigenschaften. Um den Typ
	' zu aktivieren muß ein Name zugewiesen und dieser Name in der Methode 
	' BestimmeTyp zurückgegeben werden. Das Attribut kann kopiert und mit
	' verschiedenen Namen mehrfach verwendet werden.
	' Eine genauere Beschreibung gibts in Lektion 6 des Ameisen-Tutorials.
	'
	<Spieler( _
	 Volkname:="", _
	 Vorname:="", _
	 Nachname:="" _
	)> _
	<Kaste( _
	 Name:="Standard", _
	 GeschwindigkeitModifikator:=0, _
	 DrehgeschwindigkeitModifikator:=0, _
	 LastModifikator:=0, _
	 ReichweiteModifikator:=0, _
	 SichtweiteModifikator:=0, _
	 EnergieModifikator:=0, _
	 AngriffModifikator:=0 _
	)> _
	Public Class MeineAmeise
		Inherits Basisameise

#Region "Kaste"

		''' <summary> 
		''' Bestimmt die Kaste einer neuen Ameise. 
		''' </summary> 
		''' <param name="anzahl">Die Anzahl der von jeder Kaste bereits vorhandenen 
		''' Ameisen.</param> 
		''' <returns>Der Name der Kaste der Ameise.</returns> 
		Public Overrides Function BestimmeKaste(ByVal anzahl As Dictionary(Of String, Integer)) As String
			Return "Standard"
		End Function

#End Region

#Region "Fortbewegung"

		''' <summary> 
		''' Wird wiederholt aufgerufen, wenn der die Ameise nicht weiss wo sie 
		''' hingehen soll. 
		''' </summary> 
		Public Overrides Sub Wartet()
		End Sub

		''' <summary> 
		''' Wird einmal aufgerufen, wenn die Ameise ein Drittel ihrer maximalen 
		''' Reichweite überschritten hat. 
		''' </summary> 
		Public Overrides Sub WirdMüde()
		End Sub

#End Region

#Region "Nahrung"

		''' <summary> 
		''' Wird wiederholt aufgerufen, wenn die Ameise mindestens einen 
		''' Zuckerhaufen sieht. 
		''' </summary> 
		''' <param name="zucker">Der nächstgelegene Zuckerhaufen.</param> 
		Public Overrides Sub Sieht(ByVal zucker As Zucker)
		End Sub

		''' <summary> 
		''' Wird wiederholt aufgerufen, wenn die Ameise mindstens ein 
		''' Obststück sieht. 
		''' </summary> 
		''' <param name="obst">Das nächstgelegene Obststück.</param> 
		Public Overrides Sub Sieht(ByVal obst As Obst)
		End Sub

		''' <summary> 
		''' Wird einmal aufgerufen, wenn di e Ameise einen Zuckerhaufen als Ziel 
		''' hat und bei diesem ankommt. 
		''' </summary> 
		''' <param name="zucker">Der Zuckerhaufen.</param> 
		Public Overrides Sub ZielErreicht(ByVal zucker As Zucker)
		End Sub

		''' <summary> 
		''' Wird einmal aufgerufen, wenn die Ameise ein Obststück als Ziel hat und 
		''' bei diesem ankommt. 
		''' </summary> 
		''' <param name="obst">Das Obstück.</param> 
		Public Overrides Sub ZielErreicht(ByVal obst As Obst)
		End Sub

#End Region

#Region "Kommunikation"

		''' <summary> 
		''' Wird einmal aufgerufen, wenn die Ameise eine Markierung des selben 
		''' Volkes riecht. Einmal gerochene Markierungen werden nicht erneut 
		''' gerochen. 
		''' </summary> 
		''' <param name="markierung">Die nächste neue Markierung.</param> 
		Public Overrides Sub RiechtFreund(ByVal markierung As Markierung)
		End Sub

		''' <summary> 
		''' Wird wiederholt aufgerufen, wenn die Ameise mindstens eine Ameise des 
		''' selben Volkes sieht. 
		''' </summary> 
		''' <param name="ameise">Die nächstgelegene befreundete Ameise.</param> 
		Public Overrides Sub SiehtFreund(ByVal ameise As Ameise)
		End Sub

		''' <summary> 
		''' Wird aufgerufen, wenn die Ameise eine befreundete Ameise eines anderen Teams trifft. 
		''' </summary> 
		''' <param name="ameise"></param> 
		Public Overrides Sub SiehtVerbündeten(ByVal ameise As Ameise)
		End Sub

#End Region

#Region "Kampf"

		''' <summary> 
		''' Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Wanze 
		''' sieht. 
		''' </summary> 
		''' <param name="wanze">Die nächstgelegene Wanze.</param> 
		Public Overrides Sub SiehtFeind(ByVal wanze As Wanze)
		End Sub

		''' <summary> 
		''' Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Ameise eines 
		''' anderen Volkes sieht. 
		''' </summary> 
		''' <param name="ameise">Die nächstgelegen feindliche Ameise.</param> 
		Public Overrides Sub SiehtFeind(ByVal ameise As Ameise)
		End Sub

		''' <summary> 
		''' Wird wiederholt aufgerufen, wenn die Ameise von einer Wanze angegriffen 
		''' wird. 
		''' </summary> 
		''' <param name="wanze">Die angreifende Wanze.</param> 
		Public Overrides Sub WirdAngegriffen(ByVal wanze As Wanze)
		End Sub

		''' <summary> 
		''' Wird wiederholt aufgerufen in der die Ameise von einer Ameise eines 
		''' anderen Volkes Ameise angegriffen wird. 
		''' </summary> 
		''' <param name="ameise">Die angreifende feindliche Ameise.</param> 
		Public Overrides Sub WirdAngegriffen(ByVal ameise As Ameise)
		End Sub

#End Region

#Region "Sonstiges"

		''' <summary> 
		''' Wird einmal aufgerufen, wenn die Ameise gestorben ist. 
		''' </summary> 
		''' <param name="todesart">Die Todesart der Ameise</param> 
		Public Overrides Sub IstGestorben(ByVal todesart As Todesart)
		End Sub

		''' <summary> 
		''' Wird unabhängig von äußeren Umständen in jeder Runde aufgerufen. 
		''' </summary> 
		Public Overrides Sub Tick()
		End Sub

#End Region

	End Class

End Namespace
