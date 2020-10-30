using System;

namespace MiniVille
{
    class Die
    {
        protected Random random = new Random();
        //variable pour le nombre de face qu'on souhaite sur le dé
        public int nbFaces;
        //variable pour le resultat du dé
        public int face { get; protected set; }

        //constructeur pour un dé basique a 6 face
        public Die()
        {
            nbFaces = 6;
        }
        //constructeur pour un dé special a X nombre de faces
        public Die(int nbfaces)
        {
            this.nbFaces = nbfaces;
        }

        //Fonction pour l'affichage du dé.
        public override string ToString()
        {
            string toString = String.Format("┌───┐\n" +
                                            "│ {0} │\n" +
                                            "└───┘", face);
            return toString;
        }
        //Fonction permetant de lancer le dé.
        public virtual int Lancer()
        {
            face = random.Next(1, nbFaces + 1);
            return face;
        }
    }
}