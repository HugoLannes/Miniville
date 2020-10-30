using System;
using System.Collections.Generic;

namespace MiniVille
{
    class Game
    {
        //declaration de nos variable
        string w; 
        string consequence = "";
        int f;
        int dice; 
        int sec; 
        int reçu;
        private bool toursJoueur = true;
        //declaration de nos liste
        public List<string> cardHand { get; set; }
        public List<string> nameCartes = new List<string>();
        //creation de nos objets
        Random r = new Random();
        Die die = new Die();
        Player JoueurA = new Player(3);
        Player JoueurB = new Player(3);
        Piles piles = new Piles();
        Cards cartes = new Cards();
        public void jeu()
        {
            //attribution de tous les noms des cartes appartenant aux cartes du jeux 
            foreach (CardsInfo i in cartes.ListCards)
            {
                nameCartes.Add(i.Name);
            }
            //retrait des cartes de debut de partie du paquet
            for (int x = 0; x < 2; x++)
            {
                sec = 0;
                for (int n = 0; n < piles.PileCards.Count; n++)
                {
                    if (JoueurA.cardHand[x] == piles.PileCards[n] && sec == 0)
                    {
                        piles.RemoveCard(piles.PileCards[n]);
                        sec = 1;
                    }
                    if (JoueurB.cardHand[x] == piles.PileCards[n] && sec == 1)
                    {
                        piles.RemoveCard(piles.PileCards[n]);
                        sec = 2;
                    }
                }
            }
            //debut de notre boucle de jeux
            do
            {
                switch (toursJoueur)
                {
                    //si c'est le tour du joueur
                    case true:
                        {
                            Console.Clear();
                            //résumé des action effectué durant le tour du Bot
                            Console.WriteLine(consequence);
                            reçu = 0;
                            //lancé du dé
                            dice = die.Lancer();
                            //affichage du dé
                            Console.WriteLine(die.ToString());
                            //affichage des cartes dans la main du joueur
                            foreach (string i in JoueurA.cardHand)
                            {
                                foreach (CardsInfo y in cartes.ListCards)
                                {
                                    if (i == y.Name)
                                    {
                                        //definition de la couleur de la carte
                                        switch (y.Color)
                                        {
                                            case "bleu":
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    break;
                                                }
                                            case "vert":
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    break;
                                                }
                                            case "rouge":
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    break;
                                                }
                                        }
                                        Console.WriteLine(cartes.ToString(y.Id));
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                            }
                            //lecture des cartes du Bot et applications des effets
                            for (int x = 0; x < JoueurB.cardHand.Count; x++)
                            {
                                foreach (CardsInfo i in cartes.ListCards)
                                {
                                    if (i.Dice == dice)
                                    {
                                        if (i.Name == JoueurB.cardHand[x])
                                        {
                                            if (i.Color == "bleu")//activation de l'effet d'une carte bleu: ajoute le nombre de $ pour l'utilisateur.
                                            {
                                                reçu += i.Gain;
                                                JoueurB.piece += i.Gain;
                                            }
                                            else if (i.Color == "rouge") //activation de l'effet d'une carte rouge : ajoute le nombre de $ pour l'utilisateur et retire cette meme somme pour l'adversaire
                                            {
                                                if (JoueurA.piece >= i.Gain)//si le joueur a plus de $ que la valeur de la carte
                                                {
                                                    reçu += i.Gain;
                                                    JoueurB.piece += i.Gain;
                                                    JoueurA.piece -= i.Gain;
                                                }
                                                else //si le joueur a moins de $ que la valeur de la carte
                                                {
                                                    reçu += i.Gain;
                                                    JoueurB.piece += JoueurA.piece;
                                                    JoueurA.piece -= JoueurA.piece;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("Le bot a reçu {0} pieces.\n", reçu);
                            reçu = 0;
                            //lecture des cartes du joueur et applications des effets
                            for (int x = 0; x < JoueurA.cardHand.Count; x++)
                            {
                                foreach (CardsInfo i in cartes.ListCards)
                                {
                                    if (i.Dice == dice)
                                    {
                                        if (i.Name == JoueurA.cardHand[x])
                                        {
                                            if (i.Color == "bleu" || i.Color == "vert")//activation des effet des cartes selon leurs couleurs: ajoute le nombre de $ pour l'utilisateur.
                                            {
                                                reçu += i.Gain;
                                                JoueurA.piece += i.Gain;
                                            }
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("Le joueur a reçu {0} pieces.\n", reçu);
                            Console.WriteLine("Le joueur a {0} pieces.\n", JoueurA.piece);
                            Console.WriteLine("Le Bot a {0} pieces.\n", JoueurB.piece);
                            Console.WriteLine("Souaitez vous piocher une nouvelle carte ? oui/non");
                            do
                            {
                                //debut de la boucle de choix
                                switch (Console.ReadLine())
                                {
                                    //dans le cas ou la reponse et positive
                                    case "oui":
                                        {
                                            f = 0;
                                            //si le joueur a moins de 1 piece
                                            if (JoueurA.piece < 1)
                                            {
                                                Console.WriteLine("\nVous ne possedez pas assez de pièces pour vous acheter une carte !!");
                                            }
                                            //si il a plus on lui affiche les cartes qui peuvent etre pioché selon ça somme d'argent
                                            else
                                            {
                                                Console.WriteLine("\nVoici les cartes que vous pouvez acheter :");
                                                foreach (CardsInfo i in cartes.ListCards)
                                                {
                                                    if ((i.Cost <= JoueurA.piece) && piles.PileCards.Contains(i.Name))
                                                    {
                                                        Console.WriteLine("- {0} : {1} - {2}$", i.Name, i.Effect, i.Cost);
                                                    }
                                                }
                                                
                                                do
                                                {
                                                    Console.WriteLine("Quel est votre choix ? : ");
                                                    string g = Console.ReadLine();
                                                    g.ToLower(); //mise en minuscule de la réponse
                                                    if (nameCartes.Contains(g))
                                                    {
                                                        JoueurA.Piocher(g);//on pioche la carte choisie si elle est disponible
                                                        piles.RemoveCard(g);//on supprime de la pioche la carte choisie
                                                        f = 1;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Aucune carte n'a ce nom, vous etes vous trompé ?");
                                                    }
                                                }
                                                //sécurité pour évité les erreurs
                                                while (f != 1);
                                            }
                                            break;
                                        }
                                        //dans le cas ou la réponse est négative
                                    case "non"://dans le cas ou la reponse est negative
                                        {
                                            f = 1;
                                            break;
                                        }
                                    default://dans le cas ou la reponse est incorecte 
                                        {
                                            Console.WriteLine("Ce choix n'est pas correct ! Recommencé :");
                                            break;
                                        }
                                }
                            }
                            while (f != 1);
                            toursJoueur = false;
                            break;
                        }
                        
                    //si c'est le tour du Bot
                    case false:
                        {
                            reçu = 0;
                            dice = die.Lancer();
                            //lecture des cartes du Joueur et applications des effets pour le Bot
                            for (int x = 0; x < JoueurA.cardHand.Count; x++)
                            {
                                foreach (CardsInfo i in cartes.ListCards)
                                {
                                    if (i.Dice == dice)
                                    {
                                        if (i.Name == JoueurA.cardHand[x])
                                        {
                                            if (i.Color == "bleu")//activation de l'effet d'une carte bleu: ajoute le nombre de $ pour le Bot.
                                            {
                                                reçu += i.Gain;
                                                JoueurA.piece += i.Gain;
                                            }
                                            else if (i.Color == "rouge")//activation de l'effet d'une carte rouge : ajoute le nombre de $ pour le Bot et retire cette meme somme à l'utilisateur
                                            {
                                                if (JoueurB.piece >= i.Gain)//si le Bot a au moins la valeur de la carte
                                                {
                                                    reçu += i.Gain;
                                                    JoueurA.piece += i.Gain;
                                                    JoueurB.piece -= i.Gain;
                                                }
                                                else//si le Bot a moins de $ que la valeur de la carte
                                                {
                                                    reçu += i.Gain;
                                                    JoueurA.piece += JoueurB.piece;
                                                    JoueurB.piece -= JoueurB.piece;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            int d = reçu;
                            reçu = 0;
                            //lecture des cartes du Bot et applications des effets pour le Bot
                            for (int x = 0; x < JoueurB.cardHand.Count; x++)
                            {
                                foreach (CardsInfo i in cartes.ListCards)
                                {
                                    if (i.Dice == dice)
                                    {
                                        if (i.Name == JoueurB.cardHand[x])
                                        {
                                            if (i.Color == "bleu" || i.Color == "vert")//activation des effet des cartes selon leurs couleurs: ajoute le nombre de $ pour le Bot.
                                            {
                                                reçu += i.Gain;
                                                JoueurB.piece += i.Gain;
                                            }
                                        }
                                    }
                                }
                            }
                            int choixBot = r.Next(0, 2);//random pour la pioche du bot (oui/non)
                            switch (choixBot == 1)
                            {
                                case true: //si oui
                                    {

                                        f = 0;
                                        if (JoueurB.piece < 1)//si le bot n'a pas assez d'argent
                                        {
                                            w = "n'a pas assez d'argent pour ce payé une carte !";
                                        }
                                        else//si le bot a assez d'argent
                                        {
                                            int nbr = 0;
                                            List<string> carteDispo = new List<string>();//creation d'une liste qui stockera le nom des cartes pour le choix du tirage du bot
                                            foreach (CardsInfo i in cartes.ListCards)
                                            {
                                                if ((i.Cost <= JoueurB.piece) && piles.PileCards.Contains(i.Name))//si le bot a assez d'argent et qu'il reste des carte dans la pioche
                                                {
                                                    nbr++;//rajout d'un possibilité pour le tirage aleatoire
                                                    carteDispo.Add(i.Name);//rajout du nom de la carte dans la liste 
                                                }
                                            }
                                            choixBot = r.Next(0, nbr);//tirage de la carte choisie
                                            do
                                            {
                                                if (nameCartes.Contains(carteDispo[choixBot]))
                                                {
                                                    JoueurB.Piocher(carteDispo[choixBot]);//rajout dans la main du bot, de la carte pioché 
                                                    piles.RemoveCard(carteDispo[choixBot]);//suppression de la pioche, de la carte tiré par le bot
                                                    f = 1;
                                                }
                                            }
                                            while (f != 1);
                                            w = string.Format("à pioché une carte : [ {0} ]", carteDispo[choixBot]);
                                        }
                                        break;
                                    }
                                case false://si non
                                    {
                                        w = "n'a pas pioché de carte !";
                                        break;
                                    }
                            }
                            //definition du résumé du tour du Bot
                            consequence = String.Format("| Durant le tour du bot, le dé a eu la valeur : {0} \n" +
                                                        "| Vous avez reçu : {1} $ \n" +
                                                        "| Le Bot a reçu : {2} $ \n" +
                                                        "| Le Bot {3}\n", dice, d, reçu, w);
                            toursJoueur = true;
                            break;
                        }
                }
            }
            //conditions de fin de partie
            while ((JoueurA.piece < 20) && (JoueurB.piece < 20));
            
            
            switch ((JoueurA.piece >= 20),(JoueurB.piece >=20))
            {
                //en cas de victoire de l'utilisateur
                case (true,false) :
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Vous avez gagné");
                        break;
                    }
                //en cas de victoire du Bot
                case (false, true):
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Vous avez perdu");
                        break;
                    }
            }
        }
    }
}
