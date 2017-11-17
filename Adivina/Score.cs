using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adivina
{
    public class Score
    {
        //Se usa para almacenar información sobre el puntaje y pasarlo
        public bool hasWon { get; set; }
        public int correct { get; set; }
        public int rightColor { get; set; }
        public bool hasPassedLevel { get; set; }

        public Score(bool won = false, int cor = 0, int rightcol = 0, bool hasPas = false)
        {
            hasWon = won;
            correct = cor;
            rightColor = rightcol;
            hasPassedLevel = hasPas;
        }
    }
}
