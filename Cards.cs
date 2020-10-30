using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MiniVille
{
    //structure contenant toute les info des cartes
    public struct CardsInfo
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public int Dice { get; set; }
        public int Gain { get; set; }

        //constructeur de notre structure
        public CardsInfo(int Id, string Color, int Cost, string Name, string Effect, int Dice, int Gain)
        {
            this.Id = Id;
            this.Color = Color;
            this.Cost = Cost;
            this.Name = Name;
            this.Effect = Effect;
            this.Dice = Dice;
            this.Gain = Gain;
        }
    }

    class Cards
    {
        //liste qui contiendra chacune des differentes cartes du jeux
        public List<CardsInfo> ListCards { get; set; }

        //constructeur de notre classe Cards
        public Cards()
        {
            //creation de la liste et ajout de chacune des cartes du jeux
            ListCards = new List<CardsInfo> {
                new CardsInfo(0,"bleu",1,"champs de ble","Recevez 1 pièce",1,1),
                new CardsInfo(1,"vert",1,"boulangerie","Recevez 2 pièces",2,2),
                new CardsInfo(2,"bleu",2,"ferme","Recevez 1 pièce",1,1),
                new CardsInfo(3,"rouge",2,"cafe","Recevez 1 pièce du joueur qui a lancé le dé",3,1),
                new CardsInfo(4,"vert",2,"superette","Recevez 3 pièces",4,3),
                new CardsInfo(5,"bleu",2,"foret","Recevez 1 pièce",5,1),
                new CardsInfo(6,"rouge",4,"restaurant","Recevez 2 pièces du joueur qui a lancé le dé",5,2),
                new CardsInfo(7,"bleu",6,"stade","Recevez 4 pièces",6,4),
            };
        }
        //fonction pour l'affichage des cartes
        public string ToString(int n)
        {
            string z = string.Format("| | {0} | {1} {2} : {3} - {4}$ |", ListCards[n].Color, ListCards[n].Dice, ListCards[n].Name, ListCards[n].Effect, ListCards[n].Cost);
            string toString = "┌"+ string.Concat(Enumerable.Repeat("─", (z.Length-2))) + "┐\n";
            toString += z;
            toString += "\n└" + string.Concat(Enumerable.Repeat("─", (z.Length - 2))) + "┘\n";
            return toString;
        }
    }
}