using System;
using System.Collections.Generic;

using AntMe.Deutsch;

namespace AntMe.Spieler.WolfgangGallo
{

	[Spieler(
		Volkname = "KreisAmeisen",
		Vorname = "Wolfgang",
		Nachname = "Gallo"
	)]

	[Kaste(
		Name = "W�chter",
		GeschwindigkeitModifikator = -1,
		DrehgeschwindigkeitModifikator = -1,
		LastModifikator = -1,
		ReichweiteModifikator = 1,
		SichtweiteModifikator = -1,
		EnergieModifikator = 2,
		AngriffModifikator = 1
	)]
	[Kaste(
		Name = "Sammler",
		GeschwindigkeitModifikator = 0,
		DrehgeschwindigkeitModifikator = 0,
		LastModifikator = 0,
		ReichweiteModifikator = 0,
		SichtweiteModifikator = 0,
		EnergieModifikator = 0,
		AngriffModifikator = 0
	)]

	public class KreisAmeise : Basisameise
	{

		// Variablen die ein n-Eck beschreiben.
		private static bool erzeugeW�chter = true;
		private bool aufEntfernungGehen = true;
		private Zucker gemerkterZucker = null;
		private bool imKreisGehen = false;
		private int innenWinkel;
		private int r�ckw�rtsFaktor;
		private int seitenL�nge;
		private int umkreisRadius;

		/// <summary>
		/// Der Konstruktor.
		/// </summary>
		public KreisAmeise()
		{
            
		}

		/// <summary>
		/// Bestimmt den Typ einer neuen Ameise.
		/// </summary>
		/// <param name="anzahl">Die Anzahl der von jedem Typ bereits vorhandenen
		/// Ameisen.</param>
		/// <returns>Der Name des Typs der Ameise.</returns>
		public override string BestimmeKaste(Dictionary<string, int> anzahl)
		{
			if (erzeugeW�chter)
			{
				erzeugeW�chter = false;

                umkreisRadius = Zufall.Zahl(10, Sichtweite * 2);
                r�ckw�rtsFaktor = Zufall.Zahl(2) == 0 ? 1 : -1;

                // Der Kreis um den Zuckerhaufen wird durch ein n-Eck angen�hert.
                int n = umkreisRadius / 2;
                seitenL�nge = (int)(2 * umkreisRadius * Math.Sin(Math.PI / n));
                innenWinkel = 180 * (n - 2) / n;

				return "W�chter";
			}

			erzeugeW�chter = true;
			return "Sammler";
		}

		#region Fortbewegung

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn der die Ameise nicht weiss wo sie
		/// hingehen soll.
		/// </summary>
		public override void Wartet()
		{
			if (Kaste == "W�chter")
				if (gemerkterZucker != null && aufEntfernungGehen)
				{
					// Bestimme die Entfernung zu der Kreisbahn.
					int entfernung =
						Koordinate.BestimmeEntfernung(this, gemerkterZucker) - umkreisRadius;

					// Gehe in Richtung Zucker bzw. vom Zucker weg auf die Kreisbahn.
					if (entfernung > 0)
					{
						DreheZuZiel(gemerkterZucker);
						GeheGeradeaus(entfernung);
					}
					else
					{
						DreheInRichtung
							(Koordinate.BestimmeRichtung(this, gemerkterZucker) + 180);
						GeheGeradeaus(-entfernung);
					}

					aufEntfernungGehen = false;
					imKreisGehen = true;
				}
				else if (gemerkterZucker != null && imKreisGehen)
				{
					// Bestimme die Richtung zum Zucker und z�hle den halben Innenwinkel
					// dazu. Das ergibt die Gehrichtung.
					int richtung = Koordinate.BestimmeRichtung(this, gemerkterZucker)
					               + r�ckw�rtsFaktor * innenWinkel / 2;

					DreheInRichtung(richtung);
					GeheGeradeaus(seitenL�nge);
				}
				else
					GeheGeradeaus();
			else if (AktuelleLast > 0)
				GeheZuBau();
			else if (gemerkterZucker != null)
				GeheZuZiel(gemerkterZucker);
			else
				GeheGeradeaus();
		}

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise ein Drittel ihrer maximalen
		/// Reichweite �berschritten hat.
		/// </summary>
		public override void WirdM�de() {}

		#endregion
		#region Nahrung

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens einen
		/// Zuckerhaufen sieht.
		/// </summary>
		/// <param name="zucker">Der n�chstgelegene Zuckerhaufen.</param>
		public override void Sieht(Zucker zucker)
		{
			Spr�heMarkierung
				(Koordinate.BestimmeRichtung(this, zucker),
				 Koordinate.BestimmeEntfernung(this, zucker));

			if (Kaste == "W�chter")
			{
				if (gemerkterZucker == null)
				{
					// Dieser Aufruf ist n�tig, damit in der n�chsten Runde Wartet()
					// aufgerufen wird.
					BleibStehen();

					gemerkterZucker = zucker;
					aufEntfernungGehen = true;
				}
			}
			else
			{
				if (gemerkterZucker == null)
					gemerkterZucker = zucker;

				if (Ziel == null)
					GeheZuZiel(zucker);
			}
		}

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindstens ein
		/// Obstst�ck sieht.
		/// </summary>
		/// <param name="obst">Das n�chstgelegene Obstst�ck.</param>
		public override void Sieht(Obst obst) {}

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise einen Zuckerhaufen als Ziel
		/// hat und bei diesem ankommt.
		/// </summary>
		/// <param name="zucker">Der Zuckerhaufen.</param>
		public override void ZielErreicht(Zucker zucker)
		{
			Nimm(zucker);
			GeheZuBau();
		}

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise ein Obstst�ck als Ziel hat und
		/// bei diesem ankommt.
		/// </summary>
		/// <param name="obst">Das Obst�ck.</param>
		public override void ZielErreicht(Obst obst) {}

		#endregion
		#region Kommunikation

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise eine Markierung des selben
		/// Volkes riecht. Einmal gerochene Markierungen werden nicht erneut
		/// gerochen.
		/// </summary>
		/// <param name="markierung">Die n�chste neue Markierung.</param>
		public override void RiechtFreund(Markierung markierung)
		{
			if (Kaste == "W�chter")
			{
				if (gemerkterZucker == null)
				{
					DreheInRichtung(markierung.Information);
					GeheGeradeaus();
				}
			}
			else if (Ziel == null)
			{
				DreheInRichtung(markierung.Information);
				GeheGeradeaus();
			}
		}

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindstens eine Ameise des
		/// selben Volkes sieht.
		/// </summary>
		/// <param name="ameise">Die n�chstgelegene befreundete Ameise.</param>
		public override void SiehtFreund(Ameise ameise) {}

		#endregion
		#region Kampf

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens einen K�fer
		/// sieht.
		/// </summary>
        /// <param name="wanze">Der n�chstgelegene K�fer.</param>
        public override void SiehtFeind(Wanze wanze)
		{
			if (Kaste == "W�chter" && gemerkterZucker != null)
                GreifeAn(wanze);
		}

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Ameise eines
		/// anderen Volkes sieht.
		/// </summary>
		/// <param name="ameise">Die n�chstgelegen feindliche Ameise.</param>
		public override void SiehtFeind(Ameise ameise)
		{
			if (Kaste == "W�chter" && gemerkterZucker != null)
				GreifeAn(ameise);
		}

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise von einem K�fer angegriffen
		/// wird.
		/// </summary>
		/// <param name="wanze">Der angreifende K�fer.</param>
        public override void WirdAngegriffen(Wanze wanze) { }

		/// <summary>
		/// Wird wiederholt aufgerufen in der die Ameise von einer Ameise eines
		/// anderen Volkes Ameise angegriffen wird.
		/// </summary>
		/// <param name="ameise">Die angreifende feindliche Ameise.</param>
		public override void WirdAngegriffen(Ameise ameise) {}

		#endregion
		#region Sonstiges

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise gestorben ist.
		/// </summary>
		/// <param name="todesart">Die Todesart der Ameise</param>
		public override void IstGestorben(Todesart todesart) {}

		/// <summary>
		/// Wird unabh�ngig von �u�eren Umst�nden in jeder Runde aufgerufen.
		/// </summary>
		public override void Tick()
		{
			if (gemerkterZucker != null)
			{
				if (Ziel is Insekt)
				{
					int entfernung = Koordinate.BestimmeEntfernung(this, Ziel);
					if (entfernung > Sichtweite * 3)
						BleibStehen();
				}

				if (Kaste == "W�chter")
					Spr�heMarkierung
						(Koordinate.BestimmeRichtung(this, gemerkterZucker),
						 Math.Min(Sichtweite, Koordinate.BestimmeEntfernung(this, gemerkterZucker)));

				if (gemerkterZucker.Menge <= 0)
				{
					gemerkterZucker = null;
					if (Kaste == "W�chter")
					{
						imKreisGehen = false;
						aufEntfernungGehen = false;
					}
				}
			}
		}

		#endregion

	}

}