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
        List<CollisionBox> colBoxLeft = new List<CollisionBox>();
        List<CollisionBox> colBoxRight = new List<CollisionBox>();

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
            box newBox = new box(25, 25, 20);
            box thirdBox = new box(200, 25, 20);
            CollisionBox newColBox = new CollisionBox(25, 25, 20);
            CollisionBox secondColBox = new CollisionBox(200, 25, 20);
            boxLeft.Add(newBox);
            boxRight.Add(thirdBox);
            colBoxLeft.Add(newColBox);
            colBoxRight.Add(secondColBox);

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
            // - update location of all boxes (drop down screen)
            foreach (box b in boxLeft)
            {
                b.y = b.y + 5;
            }
            foreach (CollisionBox c in colBoxLeft)
            {
                c.y = c.y + 5;
            }
            foreach(box b in boxRight)
            {
                b.y = b.y + 5;
            }
            foreach(CollisionBox c in colBoxRight)
            {
                c.y = c.y + 5;
            }
            #endregion

            #region sidetoside



            #endregion

            #region remove
            // - remove box if it has gone of screen
            if (boxLeft[0].y > this.Height - 50)
            {
                boxLeft.RemoveAt(0);
                boxRight.RemoveAt(0);
            }
            if(colBoxLeft[0].y > this.Height - 50)
            {
                colBoxLeft.RemoveAt(0);
                colBoxRight.RemoveAt(0);
            }

            #endregion
            // - add new box if it is time
            counter++;

            if(counter == 8)
            {
               
                box anotherBox = new box(25, 25, 20);
                boxLeft.Add(anotherBox);
                box fourthBox = new box(200, 25, 20);
                boxRight.Add(fourthBox);
                CollisionBox wowBox = new CollisionBox(25, 25, 20);
                CollisionBox yikeBox = new CollisionBox(200, 25, 20);
                colBoxLeft.Add(wowBox);
                colBoxRight.Add(yikeBox);
                counter = 0;
            }


            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            
            Random randGen = new Random();
            int r = randGen.Next(0, 255);
            int g = randGen.Next(0, 255);
            int blue = randGen.Next(0, 255);
            SolidBrush boxBrush = new SolidBrush(Color.FromArgb(r,g,blue));
            // - draw boxes to screen
            for (int i = 0; i < boxLeft.Count(); i++)
            {
                e.Graphics.FillRectangle(boxBrush, boxLeft[i].x, boxLeft[i].y, boxLeft[i].size, boxLeft[i].size);
            }
            foreach(box b in boxRight)
            {
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }
        }
    }
}
