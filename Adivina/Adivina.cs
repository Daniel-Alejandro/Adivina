using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adivina
{
    public partial class Adivina : Form
    {
        string PRUEBA;
        //almacena el color activo actualmente
        Color selectedColor = Color.Red;

        //Tiene los colores que el jugador intenta adivinar
        Color[] colors;
        
        Nivel nivel;

        //Holds the active level's boxes
        List<Control> activeBoxes;
        //Holds the active level's score box
        List<Control> scoreBoxes;

        //Holds the next level's group box
        List<Control> nextLev;

        //When the user fails
        bool hasFailed = false;

        //Used for when the buttons are clicked for checking thigs
        public delegate void onPassLevelNotWin();

        //The method called when the buttons are clicked
        onPassLevelNotWin passNotWin;






        public Adivina()
        {
            //Init the level
            nivel = new Nivel();
            //Agregue el controlador de cambios para el objeto Level
            nivel.Changed += new Nivel.ChangeHandler(HandleLevelUp);
            //Set the method for the check class
            passNotWin = doesNothing;
            //

            InitializeComponent();
            colors = new Color[4];
            //Set the colors to random colors
            coloresRandom();
            //Initialize the activeBoxes variable
            activeBoxes = new List<Control> { niv1Tb1, niv1Tb2, niv1Tb3, niv1Tb4 };
            scoreBoxes = new List<Control> { niv1ColorCorrecto, niv1Correcto };
            nextLev = new List<Control> { gb2 };
        }


        public void coloresRandom()
        {
            //Create a Random
            Random r = new Random();
            //For loop that loops through all the objects in colors
            for (int i = 0; i < colors.Length; i++)
            {
                //create a random int, between 0 and 5
                int asdf = r.Next(0, 5);
                //switch statement based on the random int
                switch (asdf)
                {
                    //if it is 0, the color is red
                    case (0):
                        colors[i] = Color.Red;
                        break;
                    //if it is 1, color is orange
                    case (1):
                        colors[i] = Color.Purple;
                        break;
                    //etc
                    case (2):
                        colors[i] = Color.Yellow;
                        break;
                    case (3):
                        colors[i] = Color.Green;
                        break;
                    case (4):
                        colors[i] = Color.Blue;
                        break;
                    
                }

            }

        }

       
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void gb1tb1_BackColorChanged(object sender, EventArgs e)
        {
           
        }

        private void gb1tb1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void tbPrueba_BackColorChanged(object sender, EventArgs e)
        {
        }

        private void tbPrueba_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void rbAmarillo_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopStart();

        }
        
        private int StopStart()
        {
            //If colors is enabled (basically the game is running)
            if (gbColores.Enabled)
            {
                //Show the "Do you want to stop" messagebox
                DialogResult qwer = MessageBox.Show("Estas seguro de parar el juego?\nTodo el progreso se perdera.", "Detener?", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                //If they say yes, return Yes (you stopped the game), and reset the game.
                if (DialogResult.Yes == qwer)
                {
                    resetGame();
                    return 1;
                }
                //Otherwiser, return 0, because nothing happened
                else
                {
                    return 0;
                }
            }
            //If the game IS NOT running, start it, return 2 as we started the game.
            else
            {
                startGame();
                return 2;
            }
        }

        private void startGame()
        {
            //Enable gboColors, so you can select color
            gbColores.Enabled = true;
            //Enable level 1 so you can go through the game
            gb1.Enabled = true;
            //Change the text on the start stop button
            btnStartStop.Text = "Stop";
        }

        private void resetGame()
        {
            btnStartStop.Text = "Start";
            //Disables the colors so you can't click them. This is because it is how the demo application does it.
            gbColores.Enabled = false;
            //The text boxes (lev[X]Box[X]) hook into the visible changed event to set their background back to white. This is handled in 'resetBoxes'
            gb1.Enabled = false;
            gb2.Visible = false;
            gb3.Visible = false;
            gb4.Visible = false;
            gb5.Visible = false;
            gb6.Visible = false;
            
            //The text boxes in the Answer groupbox hook into the visible changed event, to change to white or the proper color. This is hooked into in the 'handling the answer things' region
            gbRespuesta.Visible = false;
            //Reset the level
            nivel.nivel = 1;
            //Reset the colors
            coloresRandom();
            //The user hasn't failed because the game isn't started
            hasFailed = false;
            //Reset things
            nextLev = new List<Control> { gb2};
            activeBoxes = new List<Control> { niv1Tb1, niv1Tb2, niv1Tb3, niv1Tb4 };
            scoreBoxes = new List<Control> { niv1ColorCorrecto, niv1Correcto };
        }

        private void changeColor(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                selectedColor = ((Control)sender).BackColor;
            }
        }

        private void levColBox_Clicked(object sender, MouseEventArgs e)
        {
            //If the sender is a control (just to be safe)
            if (sender is Control)
            {
                //If the user HAS NOT failed
                if (!hasFailed)
                {
                    //Pass it to the check delegate method, so it goes to the correct method, AND cast it to a control
                    checkLevels((Control)sender);
                    //Focus on the button, so you don't see the | flashing cursor in the text box
                    btnStartStop.Focus();
                }
                else
                {
                    failed();
                }
            }
        }


        #region Level Checkers
        //Called when the user has FAILED
        private void failed()
        {
            //Show a failed box
            MessageBox.Show("Perdiste, Empieza un nuevo juego!", "FAILED");
            /*
             * When the user has chosen 'Ignore' on the fail box, if they try to click a textbox, give them an error
             */
        }

        //Used when failed is not needed.
        private void doesNothing()
        {

        }

        private Score checkLevels(Control sender)
        {
            //If the sender is lev1Box[1,2,3,4]
            if (activeBoxes.Contains(sender))
            {
                //Set the background color to the selected color
                sender.BackColor = selectedColor;
            }
            //Create a score
            Score toReturn = new Score();
            //If any of them are white
            if (activeBoxes[0].BackColor == Color.White || activeBoxes[1].BackColor == Color.White || activeBoxes[2].BackColor == Color.White || activeBoxes[3].BackColor == Color.White)
            {
                //They have NOT passed the level, still have boxes to fill
                toReturn.hasPassedLevel = false;
            }
            //Otherwise
            else
            {
                //they HAVE passed the level
                toReturn.hasPassedLevel = true;
                //Check all the boxes, give it the score, and the lev1Box[1,2,3,4] boxes
                checkBoxes(toReturn, new Control[] { activeBoxes[0], activeBoxes[1], activeBoxes[2], activeBoxes[3] });

                //Set the correct color and Correct color correct place boxes to the value from the score (set in checkBoxes)
                scoreBoxes[0].Text = toReturn.rightColor.ToString();
                scoreBoxes[1].Text = toReturn.correct.ToString();
                //Set level 2 visible
                nextLev[0].Visible = true;
                //If the player has won
                if (toReturn.hasWon == true)
                {
                    //Show them the winner dialog
                    winner();
                    //Exit out of the method
                    return toReturn;
                }
                //Called when you have passed a level and didn't win. Used for failing at level 7.
                passNotWin();
                //Go up a level, we are now on the NEXT LEVEL
                nivel++;
            }
            //Exit out of the method
            return toReturn;
        }
        
        
        private void fail()
        {
            //Show them the answer
            gbRespuesta.Visible = true;
            //Gotta loose the game to see what this does
            //BackgroundImage = Mastermind.Properties.Resources.obama_frowns_nh___Copy;
            //Show them a messagebox saying they failed
            DialogResult d = MessageBox.Show("You Failed!", "You Failed!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            //If they chose abort
            if (d == DialogResult.Abort)
            {
                //Disable gboColors so the 'Are you sure you want to stop' box isn't shown
                gbColores.Enabled = false;
                //Exit
                Application.Exit();
            }
            //Otherwise, if you chose retry
            else if (d == DialogResult.Retry)
            {
                //Reset the game
                resetGame();
            }
            //Otherwise if they chose ignore
            else if (d == DialogResult.Ignore)
            {
                //the check method (called when a text box is clicked) is set to 'failed' so that if they click anything it alerts them to their failure.
                hasFailed = true;
            }
        }

        #endregion

        //Called when you win
        private void winner()
        {
            //Show the answer, they already guessed it
            gbRespuesta.Visible = true;
            //Set the background (if you want to see it, play the game, and win)
            //BackgroundImage = Mastermind.Properties.Resources.obama_smiling_getty___Copy;
            //Tell the user they are a winner (Reference to Big Rigs: Over the Road Racing)
            MessageBox.Show("You're Winner!", "You're Winner!");
            //Reset the game after they dismiss the messagebox.
            resetGame();
        }


        //Check the boxes, return a Score.
        public void checkBoxes(Score s, Control[] uiop)
        {
            //Create a color to hold the colors
            Color[] c = new Color[4];
            //Copy to the temp array from the colors array
            colors.CopyTo(c, 0);
            //This will be full of the background colors of the uiop array
            Color[] qwer = new Color[uiop.Length];
            //Get all the backcolors of the controls in uiop and put them into qwer
            for (int i = 0; i < qwer.Length; i++)
            {
                //This is because if we modify the controls (even if we .CopyTo them a new array)
                //it will modify the controls on screen. This allows us to change the color without
                //causing issues. Reasoning is explained later.
                qwer[i] = uiop[i].BackColor;
            }
            //Check for correct colors
            for (int i = 0; i < qwer.Length; i++)
            {
                if (c[i] == qwer[i])
                {
                    //Add one to the score's correct value
                    s.correct++;
                    //Set the color in the temp array to white, so it won't be counted again
                    c[i] = Color.White;
                    //Set the color to black. This prevents a certain issue from happening when you have 1 color and then 3 of a different color as an answer.
                    /*
                     * Example:
                     * Answer: Orange Red Red Red
                     * Without this line:
                     * User Enters: Orange Orange Red Rd
                     * Score Boxes: Correct: 3, Color Correct: 1
                     * With this line:
                     * User Enters: Orange Red Red Red
                     * Score Boxes: Correct: 3 Color Correct: 0
                     */
                    qwer[i] = Color.Black;
                }
            }
            //Check for the correct color in the wrong places
            for (int i = 0; i < qwer.Length; i++)
            {
                //Loop through controls
                for (int j = 0; j < c.Length; j++)
                {
                    //Loop through colors
                    //If the colors of the control is the same as the color
                    if (qwer[i] == c[j])
                    {
                        //Add one to the rightcolor property of the score
                        s.rightColor++;
                        //Set the color to white so it is not able to be confused
                        /*
                         * Example:
                         * Answer: Red Orange Yellow Green
                         * 
                         * Without this line:
                         * User entered: Green Green Green Green
                         * Score Boxes: Correct: 1 Color Correct: 3
                         * 
                         * With this line:
                         * User entered: Green Green Green Green
                         * Score Boxes: Correct: 1 Color correct: 0
                         */
                        c[j] = Color.White;
                        break;
                    }
                }
            }
            //If 4 are correct, then you are correct
            if (s.correct == 4)
            {
                //Set the score's hasWon value to true
                s.hasWon = true;
            }
            //The score that was passed in is how the value is returned.
        }


        //Called when boxes' visibility is changed (with some exceptions), to reset their colors to white
        private void resetBoxes(object sender, EventArgs e)
        {
            //Check that the sender is a Control, to be safe
            if (sender is Control)
            {
                //Reset the color to white
                ((Control)sender).BackColor = Color.White;
                //Clear any text
                ((Control)sender).Text = "";
                //If it is one of the boxes that should be gray
                if (sender == niv1ColorCorrecto || sender == niv2ColorCorrecto || sender == niv3ColorCorrecto || sender == niv4ColorCorrecto || sender == niv5ColorCorrecto || sender == niv6ColorCorrecto)
                {
                    //Set it to gray
                    ((Control)sender).BackColor = Color.DimGray;
                }
            }
        }


        //Click event for score boxes
        private void denyAccess(object sender, EventArgs e)
        {
            //Focuses the startstop button so that the | cursor is not shown in the boxes
            btnStartStop.Focus();
        }

        //The method that hooks into the Changed event of the Level class
        public void HandleLevelUp(Nivel l)
        {
            //Swtich based on the level
            switch (l.nivel)
            {
                //if level 1, set the active boxes to level 1 boxes, score boxes to lev1 score boxes, and next level gbo is gboSecond, and passnotwin (When you have completed a level and not won) to doesNothing
                case 1:
                    activeBoxes = new List<Control> { niv1Tb1, niv1Tb2, niv1Tb3, niv1Tb4 };
                    scoreBoxes = new List<Control> { niv1ColorCorrecto, niv1Correcto };
                    nextLev = new List<Control> { gb2 };
                    passNotWin = doesNothing;
                    break;
                //If level 2, etc
                case 2:
                    activeBoxes = new List<Control> { niv2Tb1, niv2Tb2, niv2Tb3, niv2Tb4 };
                    scoreBoxes = new List<Control> { niv2ColorCorrecto, niv2Correcto };
                    nextLev = new List<Control> { gb3 };
                    passNotWin = doesNothing;
                    break;
                case 3:
                    activeBoxes = new List<Control> { niv3Tb1, niv3Tb2, niv3Tb3, niv3Tb4 };
                    scoreBoxes = new List<Control> { niv3ColorCorrecto, niv3Correcto };
                    nextLev = new List<Control> { gb4 };
                    passNotWin = doesNothing;
                    break;
                case 4:
                    activeBoxes = new List<Control> { niv4Tb1, niv4Tb2, niv4Tb3, niv4Tb4 };
                    scoreBoxes = new List<Control> { niv4ColorCorrecto, niv4Correcto };
                    nextLev = new List<Control> { gb5 };
                    passNotWin = doesNothing;
                    break;
                case 5:
                    activeBoxes = new List<Control> { niv5Tb1, niv5Tb2, niv5Tb3, niv5Tb4 };
                    scoreBoxes = new List<Control> { niv5ColorCorrecto, niv5Correcto };
                    nextLev = new List<Control> { gb6 };
                    passNotWin = doesNothing;
                    break;
                case 6:
                    activeBoxes = new List<Control> { niv6Tb1, niv6Tb2, niv6Tb3, niv6Tb4 };
                    scoreBoxes = new List<Control> { niv6ColorCorrecto, niv6Correcto };
                    nextLev = new List<Control> { gb1 };
                    passNotWin = fail;
                    break;
                
            }
        }


        #region handling the answer things
        //When gboAnswer's visibility is changed
        private void gboAnswer_VisibleChanged(object sender, EventArgs e)
        {
            //If it was changed to be visible
            if (gbRespuesta.Visible)
            {
                //Set the colors of the answer boxes so the user can see the answer
                gbResTb1.BackColor = colors[0];
                gbResTb2.BackColor = colors[1];
                gbResTb3.BackColor = colors[2];
                gbResTb4.BackColor = colors[3];
            }
            //Otherwise
            else
            {
                //Do Nothing
            }
        }

        //Called when the visibility is changed on the Answer Boxes
        private void AnswBox_VisibleChanged(object sender, EventArgs e)
        {
            //If any of them are visible
            if (gbResTb1.Visible || gbResTb2.Visible || gbResTb3.Visible || gbResTb4.Visible)
            {
                //Do Nothing
            }
            //Otherwise
            else
            {
                // Clear them out
                resetBoxes(sender, e);
            }
        }
        #endregion

        //If the game is running, and you choose to keep the game running, don't close, otherwise, close
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Call the stop start method
            int i = StopStart();
            //If 1 or 2(Game stopped or game started)
            if (i == 1 || i == 2)
            {
                //Do Nothing
            }
            //Otherwise
            else
            {
                //Cancel the form closing
                e.Cancel = true;
            }
        }



    }
}
