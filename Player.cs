using System.Collections.Generic;

namespace MiniVille
{
    class Player
    {
        //liste qui contiendra les cartes du joueur
        public List<string> cardHand { get; set; }
        //variable pour le nombre de pieces que le joueur possede
        public int piece { get; set; }

        //creation de carte pour la fonction piocher
        Cards carte = new Cards();

        //constructeur de la classe player
        public Player(int piece)
        {
            this.piece = piece;
            this.cardHand = new List<string>();
            //ajout des cartes de debut de jeux
            cardHand.Add("champs de ble");
            cardHand.Add("boulangerie");
        }

        //fonction permetant d'acheter une carte
        public void Piocher(string choix)
        {
            //ajoute la carte a la main du joueur
            cardHand.Add(choix);
            //retire le prix de la carte au nombre de piece du joueur
            foreach (CardsInfo i in carte.ListCards)
            {
                if (i.Name == choix)
                {
                    this.piece -= i.Cost;
                }
            }
        }
    }
}

