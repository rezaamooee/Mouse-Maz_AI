using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maz_Step1
{
    public partial class Form2 : Form
    {
        public bool HasGoalPosition = false;
        public bool MapEditorFlag = false;
        public char[,] CustomGameMap = new char[13, 13];
        public char[,] DefaultGameMap = new char[13, 13]
        {
            //1     2    3    4    5    6    7    8    9    10   11   12  13
            { 'f', 'f', 'b', 'f', 'b', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f' },
            //2
            { 'f', 'f', 'b', 'f', 'b', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f' },
            //3
            { 'f', 'b', 'b', 'f', 'b', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f' },
            //4
            { 'f', 'f', 'f', 'f', 'b', 'f', 'b', 'b', 'b', 'b', 'b', 'b', 'f' },
            //5
            { 'f', 'b', 'b', 'b', 'b', 'f', 'b', 'f', 'f', 'f', 'f', 'b', 'f' },
            //6
            { 'f', 'b', 'f', 'f', 'f', 'f', 'b', 'f', 'f', 'f', 'f', 'b', 'f' },
            //7
            { 'f', 'b', 'f', 'f', 'f', 'f', 'b', 'f', 'b', 'b', 'f', 'b', 'f' },
            //8
            { 'f', 'b', 'f', 'f', 'f', 'f', 'b', 'f', 'b', 'b', 'f', 'b', 'f' },
            //9
            { 'f', 'b', 'f', 'f', 'f', 'f', 'b', 'f', 'b', 'b', 'f', 'b', 'f' },
            //10
            { 'f', 'b', 'b', 'f', 'f', 'f', 'b', 'f', 'g', 'b', 'f', 'f', 'f' },
            //11
            { 'f', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'f' },
            //12
            { 'f', 'f', 'f', 'b', 'f', 'f', 'f', 'f', 'f', 'b', 'f', 'f', 'f' },
            //13
            { 'f', 'f', 'f', 'f', 'f', 'f', 'b', 'f', 'f', 'f', 'f', 'f', 'f' }
            //1     2    3    4    5    6    7    8    9    10   11   12  13
        }; //{b : Block , f :Free , g : Goal }
        public void LoadMapOnScreen(char [,] Map)
        {
            for (int Row =0; Row < 13; Row++)
            {
                for (int Column = 0; Column < 13; Column++)
                    switch (Map[Row,Column])
                    {
                        case 'f':
                            this.Controls["lbl" + (Row * 13 + Column + 1).ToString()].BackColor = Color.White;
                            break;
                        case 'b':
                            this.Controls["lbl" + (Row * 13 + Column + 1).ToString()].BackColor = Color.Black;
                            break;
                        case 'g':
                            this.Controls["lbl" + (Row * 13 + Column + 1).ToString()].BackColor = Color.Green;
                            this.Controls["lbl" + (Row * 13 + Column + 1).ToString()].Text = "Goal";
                            this.HasGoalPosition = true;
                            break;
                    }
            }
        }
        public void ClearMapOnScreen()
        {
            for (int Row = 0; Row < 13; Row++)
                for (int Column = 0; Column < 13; Column++)
                    this.Controls["lbl" + (Row * 13 + Column + 1).ToString()].BackColor = Color.White;
            this.HasGoalPosition = false;
        }
        public void SaveMapOnScreen()
        {
            if (HasGoalPosition == false)
                MessageBox.Show("This Map Dont Has Goal Position");
            else
                for (int Row = 0; Row < 13; Row++)
                {
                    for (int Column = 0; Column < 13; Column++)
                    {
                        if (this.Controls["lbl" + ((Row * 13) + Column + 1).ToString()].BackColor == Color.Black)
                        {
                            this.CustomGameMap[Row, Column] = 'b';
                        }
                        else if (this.Controls["lbl" + ((Row * 13) + Column + 1).ToString()].BackColor == Color.White)
                        {
                            this.CustomGameMap[Row, Column] = 'f';
                        }
                        else if(this.Controls["lbl" + ((Row * 13) + Column + 1).ToString()].BackColor == Color.Green)
                        {
                            this.CustomGameMap[Row, Column] = 'g';
                        }
                    }
                }
        }
        public void MapSell_Click(object sender, EventArgs e)
        {
            if ( ((Control)sender).Name!="lbl1" )
            {
                if (rbtnAddWall.Checked == true)
                {
                    if (((Control)sender).BackColor==Color.Green)
                        HasGoalPosition = false;

                    ((Control)sender).BackColor = Color.Black;
                }
                else if (rbtnAddWay.Checked == true)
                {
                    if (((Control)sender).BackColor == Color.Green)
                        HasGoalPosition = false;

                    ((Control)sender).BackColor = Color.White;
                }
                else if (rbtnSetGoal.Checked == true)
                {
                    if (((Control)sender).BackColor == Color.Green)
                    {
                        HasGoalPosition = false;
                        ((Control)sender).BackColor = Color.White;
                    }
                    else
                    {
                        if (!HasGoalPosition)
                        {
                            ((Control)sender).BackColor = Color.Green;
                            HasGoalPosition = true;
                        }
                    }

                }
            }
        }
        private void btnDefualtMap_Click(object sender, EventArgs e)
        {
            LoadMapOnScreen(DefaultGameMap);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ClearMapOnScreen();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            ClearMapOnScreen();
            LoadMapOnScreen(CustomGameMap);
        }
        public Form2()
        {
            InitializeComponent();
        }
        private void btnSaveMap_Click(object sender, EventArgs e)
        {
            SaveMapOnScreen();
            this.MapEditorFlag = true;
        }
    }
}
