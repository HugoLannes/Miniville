using System.Collections.Generic;

namespace MiniVille
{
    class Piles
    {
        //Creation d'une liste qui contiendra toutes nos cartes
        public List<string> PileCards { get; set; }

        //Constructeru de la classe Piles
        public Piles()
        {
            PileCards = new List<string>();

            //Boucle permetant d'ajouter 5 fois chaque cartes au paquet
            for (int i = 0; i < 5; i++)
            {
                PileCards.Add("champs de ble");
                PileCards.Add("ferme");
                PileCards.Add("boulangerie");
                PileCards.Add("cafe");
                PileCards.Add("superette");
                PileCards.Add("foret");
                PileCards.Add("restaurant");
                PileCards.Add("stade");
            }
        }

        //Fonction permetant de retirer une carte du paquet
        public void RemoveCard(string n)
        {
            PileCards.Remove(n);
        }
    }
}
