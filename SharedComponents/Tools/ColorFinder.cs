using System;
using System.Collections.Generic;
using System.Drawing;

using AntMe.SharedComponents.Properties;

namespace AntMe.SharedComponents.Tools {
    /// <summary>
    /// Stellt eine Farbe im RGB-Farbraum dar.
    /// </summary>
    /// <remarks>
    /// Diese Struktur wurde definiert um von den in Windows Forms und Managed
    /// DirectX definierten Farben unabh�ngig zu sein. Zus�tzlich k�nnen Farben
    /// durch die Verwendung dieser Struktur gemischt werden.
    /// </remarks>
    /// <author>Wolfgang Gallo (wolfgang@antme.net)</author>
    public struct Farbe {
        private int blau;
        private int gr�n;
        private int rot;

        /// <summary>
        /// Der Farbe Konstruktor.
        /// </summary>
        /// <param name="rot">Rot-Wert</param>
        /// <param name="gr�n">Gr�n-Wert</param>
        /// <param name="blau">Blau-Wert</param>
        public Farbe(int rot, int gr�n, int blau) {
            this.rot = rot;
            this.gr�n = gr�n;
            this.blau = blau;
        }

        /// <summary>
        /// Der Rot-Wert der Farbe.
        /// </summary>
        public byte Rot {
            get { return rot < 0 ? (byte) 0 : rot > 255 ? (byte) 255 : (byte) rot; }
            set { rot = value; }
        }

        /// <summary>
        /// Der Gr�n-Wert der Farbe.
        /// </summary>
        public byte Gr�n {
            get { return gr�n < 0 ? (byte) 0 : gr�n > 255 ? (byte) 255 : (byte) gr�n; }
            set { gr�n = value; }
        }

        /// <summary>
        /// Der Blau-Wert der Farbe.
        /// </summary>
        public byte Blau {
            get { return blau < 0 ? (byte) 0 : blau > 255 ? (byte) 255 : (byte) blau; }
            set { blau = value; }
        }

        /// <summary>
        /// Gibt die Farbe als Zeichenkette zur�ck.
        /// </summary>
        /// <returns>(Rot,Gr�n,Blau)</returns>
        public override string ToString() {
            return "(" + Rot + "," + Gr�n + "," + Blau + ")";
        }

        /// <summary>
        /// Addiert die RGB-Werte zweier Farben.
        /// </summary><remarks>
        /// Um zwei Farben zu mischen mu� zus�tzlich eine Division durchgef�hrt
        /// werden: (farbe1 + farbe2) / 2.
        /// </remarks>
        /// <param name="f1">Farbe 1</param>
        /// <param name="f2">Farbe 2</param>
        /// <returns>Farbe</returns>
        public static Farbe operator +(Farbe f1, Farbe f2) {
            return new Farbe(f1.rot + f2.rot, f1.gr�n + f2.gr�n, f1.blau + f2.blau);
        }

        /// <summary>
        /// Multipliziert die RGB-Werte einer Farbe mit einer Zahl.
        /// </summary>
        /// <param name="f">Farbe</param>
        /// <param name="i">Zahl</param>
        /// <returns>Farbe</returns>
        public static Farbe operator *(Farbe f, int i) {
            return new Farbe(f.rot*i, f.gr�n*i, f.blau*i);
        }

        /// <summary>
        /// Dividiert die RGB-Werte einer Farbe durch eine Zahl.
        /// </summary>
        /// <param name="f">Farbe</param>
        /// <param name="i">Zahl</param>
        /// <returns>Farbe</returns>
        public static Farbe operator /(Farbe f, int i) {
            return new Farbe(f.rot/i, f.gr�n/i, f.blau/i);
        }

        /// <summary>
        /// Bestimmt ein Abstand-Ma� zwischen zwei Farben im RGB-Farbraum.
        /// </summary><remarks>
        /// Wird von der Farbberater-Klasse verwendet.
        /// </remarks>
        /// <param name="f1">Farbe 1</param>
        /// <param name="f2">Farbe 2</param>
        /// <returns>Abstand�</returns>
        public static int operator -(Farbe f1, Farbe f2) {
            int deltaRot = f1.rot - f2.rot;
            int deltaGr�n = f1.gr�n - f2.gr�n;
            int deltaBlau = f1.blau - f2.blau;
            return deltaRot*deltaRot + deltaGr�n*deltaGr�n + deltaBlau*deltaBlau;
            //return Math.Abs(deltaRot) + Math.Abs(deltaGr�n) + Math.Abs(deltaBlau);
        }
    }

    /// <summary>
    /// Liefert Farben die sie m�glichst stark voneinander Unterscheiden.
    /// </summary>
    /// <author>Wolfgang Gallo (wolfgang@antme.net)</author>
    public class ColorFinder {
        public static Color ColonyColor1 {
            get { return Settings.Default.ColonyColor1; }
        }

        public static Color ColonyColor2 {
            get { return Settings.Default.ColonyColor2; }
        }

        public static Color ColonyColor3 {
            get { return Settings.Default.ColonyColor3; }
        }

        public static Color ColonyColor4 {
            get { return Settings.Default.ColonyColor4; }
        }

        public static Color ColonyColor5 {
            get { return Settings.Default.ColonyColor5; }
        }

        public static Color ColonyColor6 {
            get { return Settings.Default.ColonyColor6; }
        }

        public static Color ColonyColor7 {
            get { return Settings.Default.ColonyColor7; }
        }

        public static Color ColonyColor8 {
            get { return Settings.Default.ColonyColor8; }
        }

        private readonly List<Farbe> farben = new List<Farbe>();

        /// <summary>
        /// Markiert eine neue Farbe als bereits vorhanden.
        /// </summary>
        /// <param name="farbe">Neue Farbe.</param>
        public void BelegeFarbe(Farbe farbe) {
            farben.Add(farbe);
        }

        /// <summary>
        /// Entfernt eine vorhandene Farbe.
        /// </summary>
        /// <param name="farbe">Vorhandene Farbe.</param>
        public void EntferneFarbe(Farbe farbe) {
            farben.Remove(farbe);
        }

        private int bestimmeMinimalenAbstand(Farbe farbe) {
            int besterAbstand = int.MaxValue;
            for (int f = 0; f < farben.Count; f++) {
                int abstand = farben[f] - farbe;
                if (abstand < besterAbstand) {
                    besterAbstand = abstand;
                }
            }
            return besterAbstand;
        }

        /// <summary>
        /// Erzeugt eine neue Farbe mit m�glichst gro�em Abstand zu den bereits
        /// vorhandenen Farben.
        /// </summary>
        /// <returns>Neue Farbe.</returns>
        public Farbe ErzeugeFarbe() {
            int besterAbstand = 0;
            Farbe besteFarbe = new Farbe(0, 0, 0);

            int r, g, b;
            int r0 = 0, g0 = 0, b0 = 0;
            int abstand;
            Farbe farbe;

            for (r = 8; r < 256; r += 16) {
                for (g = 8; g < 256; g += 16) {
                    for (b = 8; b < 256; b += 16) {
                        farbe = new Farbe(r, g, b);
                        abstand = bestimmeMinimalenAbstand(farbe);
                        if (abstand > besterAbstand) {
                            besterAbstand = abstand;
                            r0 = r;
                            g0 = g;
                            b0 = b;
                        }
                    }
                }
            }

            for (r = -8; r < 8; r++) {
                for (g = -8; g < 8; g++) {
                    for (b = -8; b < 8; b++) {
                        farbe = new Farbe(r0 + r, g0 + g, b0 + b);
                        abstand = bestimmeMinimalenAbstand(farbe);
                        if (abstand > besterAbstand) {
                            besterAbstand = abstand;
                            besteFarbe = farbe;
                        }
                    }
                }
            }

            return besteFarbe;
        }

        /// <summary>
        /// Erzeugt eine neue Farbe mit m�glichst gro�em Abstand zu den bereits
        /// vorhandenen Farben und ver�ndert sie leicht.
        /// </summary>
        /// <returns>Neue Farbe.</returns>
        public Farbe ErzeugeFarbe(int streuung) {
            Farbe farbe = ErzeugeFarbe();
            Random zufall = new Random();
            return
                new Farbe
                    (
                    (farbe.Rot*100)/(100 + zufall.Next(-streuung, streuung)),
                    (farbe.Gr�n*100)/(100 + zufall.Next(-streuung, streuung)),
                    (farbe.Blau*100)/(100 + zufall.Next(-streuung, streuung)));
        }

        /// <summary>
        /// Erzeugt eine neue Farbe mit m�glichst gro�em Abstand zu den bereits
        /// vorhandenen Farben und markiert sie als belegt.
        /// </summary>
        /// <returns>Neue Farbe.</returns>
        public Farbe ErzeugeUndBelegeFarbe() {
            Farbe farbe = ErzeugeFarbe();
            BelegeFarbe(farbe);
            return farbe;
        }

        /// <summary>
        /// Erzeugt eine neue Farbe mit m�glichst gro�em Abstand zu den bereits
        /// vorhandenen Farben, ver�ndert sie leicht und markiert sie als belegt.
        /// </summary>
        /// <returns>Neue Farbe.</returns>
        public Farbe ErzeugeUndBelegeFarbe(int streuung) {
            Farbe farbe = ErzeugeFarbe(streuung);
            BelegeFarbe(farbe);
            return farbe;
        }
    }
}