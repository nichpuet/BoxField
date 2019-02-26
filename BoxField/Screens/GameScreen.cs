using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen


        // - create a list to hold a column of boxes        
        List<box> boxLeft = new List<box>();
        List<box> boxRight = new List<box>();
       
        box player;

        int moveSwitch = 0;
        bool moveSide = true;
        int box1X = 25, box2X = 200;

        int counter = 0;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            // - set game start values
            box newBox = new box(25, 25, 20,0,0,0);
            box thirdBox = new box(200, 25, 20,0,0,0);
           
            boxLeft.Add(newBox);
            boxRight.Add(thirdBox);
           
            player = new box(120, this.Height - 75, 25,255,0,0);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;           
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            #region down
            int downSpeed = 5;

            // - update location of all boxes (drop down screen)
            foreach (box b in boxLeft)
            {
                b.boxMove(downSpeed);            }
            
            foreach(box b in boxRight)
            {
                b.boxMove(downSpeed);
            }

            #endregion

            #region player movement
            if (leftArrowDown)
            {
                player.boxMove("left", 5);
            }

            if (rightArrowDown)
            {
                player.boxMove("right", 5);
            }

            foreach (box b in boxLeft.Union(boxRight))
            {
                if (player.Collision(b))
                {
                    gameLoop.Stop();
                }
            }
            #endregion

            #region remove
            // - remove box if it has gone of screen
            if (boxLeft[0].y > this.Height - 50)
            {
                boxLeft.RemoveAt(0);
                boxRight.RemoveAt(0);
            }


            #endregion

            #region addding
            // - add new box if it is time
            counter++;
            
            if (counter == 8)
            {
                Random randGen = new Random();
                int r = randGen.Next(0, 255);
                int g = randGen.Next(0, 255);
                int blue = randGen.Next(0, 255);

                moveSwitch = randGen.Next(0, 11);

                if (moveSide == false)
                {
                    box1X -= 10;
                    box2X -= 10;
                    if (box1X <= 25)
                    {
                        moveSide = true;
                    }
                    if (moveSwitch == counter )
                    {
                        moveSide = true;
                    }
                }
                if (moveSide == true)
                {
                    box1X += 10;
                    box2X += 10;
                    if (box2X >= 475)
                    {
                        moveSide = false;
                    }
                    if (moveSwitch == counter)
                    {
                        moveSide = false;
                    }
                }

                box anotherBox = new box(box1X, 25, 20,r,g,blue);
                boxLeft.Add(anotherBox);
                box fourthBox = new box(box2X, 25, 20,r,g,blue);
                boxRight.Add(fourthBox);

                counter = 0;
            }
            #endregion

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {

            int newR,newG,newB;

 
            // - draw boxes to screen
            for (int i = 0; i < boxLeft.Count(); i++)
            {
                newR = boxLeft[i].r;
                newG = boxLeft[i].g;
                newB = boxLeft[i].blue;
                SolidBrush boxBrush = new SolidBrush(Color.FromArgb(newR, newG, newB));
                e.Graphics.FillRectangle(boxBrush, boxLeft[i].x, boxLeft[i].y, boxLeft[i].size, boxLeft[i].size);
            }
            for (int i = 0; i < boxRight.Count(); i++)
            {

                newR = boxRight[i].r;
                newG = boxRight[i].g;
                newB = boxRight[i].blue;
                SolidBrush boxBrush = new SolidBrush(Color.FromArgb(newR, newG, newB));
                e.Graphics.FillRectangle(boxBrush, boxRight[i].x, boxRight[i].y, boxRight[i].size, boxRight[i].size);
            }
            Pen playerPen = new Pen(Color.Maroon);
            e.Graphics.DrawEllipse(playerPen, player.x, player.y, player.size, player.size);
        }
    }
}
