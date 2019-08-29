using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace maz_Step1
{
    public partial class Form1 : Form
    {
        char[,] DefaultGameMap = new char[13, 13]
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
            { 'f', 'f', 'f', 'f', 'f', 'f', 'b', 'f', 'b', 'b', 'f', 'b', 'f' },
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
        public char[,] GameMap = new char[13,13]; //{b : Block , f :Free , g : Goal }
        public bool MapEditorFlag = false;
        char DecisionLogic = 'r';    //{ r : Right , l : Left , s : Straight , a : Accidently=Random} b: No Way Shulde we go back
        char HeadDirection = 's';   //{ n : North , s : South , w : West , e : East }
        Random RandomPath = new Random();
        int MouseRow = 0, MouseColumn = 0, MouseLocation = 0;
        _Memory MouseMemory = new _Memory();
        List<char[]> mem = new List<char[]>();
        
        
        public void GoRight(int Row, int Column)
        {
            this.MouseLocation = _PublicFunction.GetRightNeighborPosition(Row, Column);
            // this.HeadDirection = _Right.SetHeadToRight(this.HeadDirection);
            MouseMemory.SaveInMemory(Row, Column, this.MouseLocation / 13, this.MouseLocation % 13);
            MouseMemory.SaveCurrentSellDetailsInMemory(this.MouseLocation / 13, this.MouseLocation % 13,'v');
            this.MouseRow = this.MouseLocation / 13;
            this.MouseColumn = this.MouseLocation % 13;
            SetGraphicStep(this.MouseRow, this.MouseColumn);
            
        }
        public void GoLeft(int Row, int Column)
        {
            this.MouseLocation = _PublicFunction.GetLeftNeighborPosition(Row, Column);
            //this.HeadDirection = _Left.SetHeadToLeft(this.HeadDirection);
            MouseMemory.SaveInMemory(Row, Column, this.MouseLocation / 13, this.MouseLocation % 13);
            MouseMemory.SaveCurrentSellDetailsInMemory(this.MouseLocation / 13, this.MouseLocation % 13, 'v');
            this.MouseRow = this.MouseLocation / 13;
            this.MouseColumn = this.MouseLocation % 13;
            SetGraphicStep(this.MouseRow, this.MouseColumn);
        }
        public void GoStraight(int Row, int Column)
        {
            this.MouseLocation = _PublicFunction.GetStraightNeighborPosition(Row, Column);
            //this.HeadDirection = _Straight.SetHeadToStraight(this.HeadDirection);
            MouseMemory.SaveInMemory(Row, Column, this.MouseLocation / 13, this.MouseLocation % 13);
            MouseMemory.SaveCurrentSellDetailsInMemory(this.MouseLocation / 13, this.MouseLocation % 13, 'v');
            this.MouseRow = this.MouseLocation / 13;
            this.MouseColumn = this.MouseLocation % 13;
            SetGraphicStep(this.MouseRow, this.MouseColumn);
        }
        public void Goback(int Row, int Column)
        {
            this.MouseLocation = _PublicFunction.GetBackNeighborPosition(Row, Column);
            //this.HeadDirection = _Straight.SetHeadToStraight(this.HeadDirection);
            MouseMemory.SaveInMemory(Row, Column, this.MouseLocation / 13, this.MouseLocation % 13);
            MouseMemory.SaveCurrentSellDetailsInMemory(this.MouseLocation / 13, this.MouseLocation % 13, 'v');
            this.MouseRow = this.MouseLocation / 13;
            this.MouseColumn = this.MouseLocation % 13;
            SetGraphicStep(this.MouseRow, this.MouseColumn);
        }
        public List<char> GetAllUnVisitedValidStep(int Row, int Column)
        {
            List<char> Steps = new List<char>();
            if (_Right.CheckMoveRightIsTrue(Row, Column, this.GameMap))
                if(!MouseMemory.IsVisited(_PublicFunction.GetRightNeighborPosition(Row,Column)))
                    Steps.Add('r');
            if (_Left.CheckMoveLeftIsTrue(Row, Column, this.GameMap))
                if (!MouseMemory.IsVisited(_PublicFunction.GetLeftNeighborPosition(Row, Column)))
                    Steps.Add('l');
            if (_Straight.CheckMoveStraightIsTrue(Row, Column, this.GameMap))
                if (!MouseMemory.IsVisited(_PublicFunction.GetStraightNeighborPosition(Row, Column)))
                    Steps.Add('s');
            if (_Back.CheckMoveToBackIsTrue(Row, Column, this.GameMap))
                if (!MouseMemory.IsVisited(_PublicFunction.GetBackNeighborPosition(Row, Column)))
                    Steps.Add('b');
            return Steps;
        }
        public List<char> GetAllValidStep (int Row, int Column)
        {
            List<char> Steps = new List<char>();
            if (_Right.CheckMoveRightIsTrue(Row, Column, this.GameMap))
                Steps.Add('r');
            if (_Left.CheckMoveLeftIsTrue(Row, Column, this.GameMap))
                Steps.Add('l');
            if (_Straight.CheckMoveStraightIsTrue(Row, Column, this.GameMap))
                Steps.Add('s');
            if (_Back.CheckMoveToBackIsTrue(Row, Column, this.GameMap))
                Steps.Add('b');
            return Steps;
        }
        public Boolean IsValideNeighbor_whithMemory(int Row, int Column,char NeighborDirection)
        {
            int RightNeighbor ;
            int LeftNeighbor ;
            int StraightNaghbor ;
            int BackNeighbor;
            switch (NeighborDirection)
            {
                case 'r':
                    RightNeighbor = _PublicFunction.GetRightNeighborPosition(Row, Column);
                    if (RightNeighbor != -1)
                        if (_PublicFunction.IsRout(RightNeighbor / 13, RightNeighbor % 13,  this.GameMap))
                            if (!MouseMemory.IsVisited(RightNeighbor / 13, RightNeighbor % 13))
                                return true;
                    return false;
                case 'l':
                    LeftNeighbor = _PublicFunction.GetLeftNeighborPosition(Row, Column);
                    if (LeftNeighbor != -1)
                        if (_PublicFunction.IsRout(LeftNeighbor / 13, LeftNeighbor % 13, this.GameMap))
                            if (!MouseMemory.IsVisited(LeftNeighbor / 13, LeftNeighbor % 13))
                                return true;

                    return false;
                case 's':
                    StraightNaghbor = _PublicFunction.GetStraightNeighborPosition(Row, Column);
                    if (StraightNaghbor != -1)
                        if (_PublicFunction.IsRout(StraightNaghbor / 13, StraightNaghbor % 13, this.GameMap))
                            if (!MouseMemory.IsVisited(StraightNaghbor / 13, StraightNaghbor % 13))
                                return true;
                    return false;
                case 'b':
                    BackNeighbor = _PublicFunction.GetBackNeighborPosition(Row, Column);
                    if (BackNeighbor != -1)
                        if (_PublicFunction.IsRout(BackNeighbor / 13, BackNeighbor % 13, this.GameMap))
                            if (!MouseMemory.IsVisited(BackNeighbor / 13, BackNeighbor % 13))
                                return true;
                    return false;
            }
            return false;
        }
        public Boolean IsValideNeighbor(int Row, int Column, char NeighborDirection)
        {
            int RightNeighbor;
            int LeftNeighbor;
            int StraightNaghbor;
            int BackNeighbor;
            switch (NeighborDirection)
            {
                case 'r':
                    RightNeighbor = _PublicFunction.GetRightNeighborPosition(Row, Column);
                    if (RightNeighbor != -1)
                        if (_PublicFunction.IsRout(RightNeighbor / 13, RightNeighbor % 13, this.GameMap))
                                return true;
                    return false;
                case 'l':
                    LeftNeighbor = _PublicFunction.GetLeftNeighborPosition(Row, Column);
                    if (LeftNeighbor != -1)
                        if (_PublicFunction.IsRout(LeftNeighbor / 13, LeftNeighbor % 13, this.GameMap))
                                return true;

                    return false;
                case 's':
                    StraightNaghbor = _PublicFunction.GetStraightNeighborPosition(Row, Column);
                    if (StraightNaghbor != -1)
                        if (_PublicFunction.IsRout(StraightNaghbor / 13, StraightNaghbor % 13, this.GameMap))
                                return true;
                    return false;
                case 'b':
                    BackNeighbor = _PublicFunction.GetBackNeighborPosition(Row, Column);
                    if (BackNeighbor != -1)
                        if (_PublicFunction.IsRout(BackNeighbor / 13, BackNeighbor % 13, this.GameMap))
                                return true;
                    return false;
            }
            return false;
        }
        public void GoNextStepHasMemory(int Row, int Column, char DecisioLogic)
        {
            List<char> ValidStep = new List<char>();
            List<int> ScapeWay = new List<int>();
            int bff;
            switch (DecisioLogic)
            {
                case 'r':
                    if (IsValideNeighbor_whithMemory(Row, Column, 'r'))
                        GoRight(Row, Column);
                    else
                        GoNextStepHasMemory(Row, Column, 'a');
                    break;
                case 'l':
                    if (IsValideNeighbor_whithMemory(Row, Column, 'l'))
                        GoLeft(Row, Column);
                    else
                        GoNextStepHasMemory(Row, Column, 'a');
                    break;
                case 's':
                    if (IsValideNeighbor_whithMemory(Row, Column, 's'))
                        GoStraight(Row, Column);
                    else
                        GoNextStepHasMemory(Row, Column, 'a');
                    break;
                case 'a':
                    ValidStep = GetAllUnVisitedValidStep(Row, Column);
                    if (ValidStep.Count > 0)
                    {
                        bff = RandomPath.Next(1, ValidStep.Count);

                        if (ValidStep[bff - 1] == 'r')
                            GoRight(Row, Column);
                        else if (ValidStep[bff - 1] == 'l')
                            GoLeft(Row, Column);
                        else if (ValidStep[bff - 1] == 's')
                            GoStraight(Row, Column);
                        else if (ValidStep[bff - 1] == 'b')
                            Goback(Row, Column);
                        break;
                    }
                    else // If This Way Is Dead End Then Go back To First Sell where Get New Way or Get Scape Way 
                    {
                        //Goback();
                        ScapeWay = MouseMemory.GetScapePath(Row, Column);
                        this.MouseLocation = ScapeWay[ScapeWay.Count - 1];
                        this.MouseRow = this.MouseLocation / 13;
                        this.MouseColumn = this.MouseLocation % 13;
                        Row = this.MouseRow;
                        Column = this.MouseColumn;
                        SetGraphicStep(this.MouseRow, this.MouseColumn);
                        GoNextStepHasMemory(Row, Column, DecisionLogic);

                    }
                    break;
            }

        }
        public void GoNextStep(int Row, int Column, char DecisioLogic)
        {
            List<char> ValidStep = new List<char>();
            List<int> ScapeWay = new List<int>();
            int bff;
            switch (DecisioLogic)
            {
                case 'r':
                    if (IsValideNeighbor(Row, Column, 'r'))
                        GoRight(Row, Column);
                    else
                        GoNextStep(Row, Column, 'a');
                    break;
                case 'l':
                    if (IsValideNeighbor(Row, Column, 'l'))
                        GoLeft(Row, Column);
                    else
                        GoNextStep(Row, Column, 'a');
                    break;
                case 's':
                    if (IsValideNeighbor(Row, Column, 's'))
                        GoStraight(Row, Column);
                    else
                        GoNextStep(Row, Column, 'a');
                    break;
                case 'a':
                    ValidStep = GetAllValidStep(Row, Column);
                    if (ValidStep.Count > 0)
                    {
                        bff = RandomPath.Next(1, ValidStep.Count);

                        if (ValidStep[bff - 1] == 'r')
                            GoRight(Row, Column);
                        else if (ValidStep[bff - 1] == 'l')
                            GoLeft(Row, Column);
                        else if (ValidStep[bff - 1] == 's')
                            GoStraight(Row, Column);
                        else if (ValidStep[bff - 1] == 'b')
                            Goback(Row, Column);
                        break;
                    }
                    else // If This Way Is Dead End Then Go back To First Sell where Get New Way or Get Scape Way 
                    {
                        //Goback();
                        ScapeWay = MouseMemory.GetScapePath(Row, Column);
                        this.MouseLocation = ScapeWay[ScapeWay.Count - 1];
                        this.MouseRow = this.MouseLocation / 13;
                        this.MouseColumn = this.MouseLocation % 13;
                        Row = this.MouseRow;
                        Column = this.MouseColumn;
                        SetGraphicStep(this.MouseRow, this.MouseColumn);
                        GoNextStep(Row, Column, DecisionLogic);

                    }
                    break;
            }

        }
        ////////////////////////////////////////// GUI Function
        public int GetLastMoveForGraphicalOperate()
        {
            string ControlsName;
            for (int i = 1; i < 170; i++)
            {
                ControlsName = "lbl" + i.ToString();
                if (this.Controls[ControlsName].BackColor == Color.Red)
                    return i;
            }
            return 1;
        }
        public void SetGraphicStep(int Row, int Column)
        {
            this.Controls["lbl" + GetLastMoveForGraphicalOperate().ToString()].BackColor = Color.Gray;
            this.Controls["lbl" + (((Row * 13) + Column) + 1).ToString()].BackColor = Color.Red;
            IsFindGoal(Row,Column,this.GameMap);
        }
        public void SetGraphicStep1(int Row , int Column  )
        {
            this.Controls["lbl" + GetLastMoveForGraphicalOperate().ToString()].BackColor = Color.Gray;
            this.Controls["lbl"+(((Row*13)+Column)+1).ToString()].BackColor = Color.Red;
        }
        public void LoadGameMapOnScreen(char[,] Map )
        {
            for (int Row = 0; Row < 13; Row++)
            {
                for (int Column = 0; Column < 13; Column++)
                    switch (Map[Row, Column])
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
                            break;
                    }
            }
            this.Controls["lbl1"].BackColor = Color.Red;
            this.GameMap = Map;
            MouseMemory.SetGameMap(Map);
        }
        public void IsFindGoal(int Row , int Column,char [,]Map)
        {
            if (Map[Row, Column] == 'g')
            {
                TimAI.Enabled = false;
                MessageBox.Show("Goal Finded");
                Form1_Load(null, null);
            }
        }
        ///////////////////////////////////////////Form Code
        public Form1()
        {
            InitializeComponent();
        }
        private void TimAI_Tick(object sender, EventArgs e)
        {
            GoNextStepHasMemory(this.MouseRow, this.MouseColumn, this.DecisionLogic);
        }
        private void TimAI2_Tick(object sender, EventArgs e)
        {
            GoNextStep(this.MouseRow, this.MouseColumn, this.DecisionLogic);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Stop")
            {
                TimAI.Enabled = false;
                TimAI2.Enabled = false;
                btnStart.Text = "Reset Application";
            }
            else if (btnStart.Text == "Start")
            {
               
                if (chboxHasMemory.Checked)
                {
                    
                    GoNextStepHasMemory(this.MouseRow, this.MouseColumn, this.DecisionLogic);
                    if (chboxRunTimer.Checked)
                    {
                        TimAI.Enabled = true;
                        TimAI_Tick(sender, e);
                        btnStart.Text = "Stop";
                    }
                }
                else
                {
                    GoNextStep(this.MouseRow, this.MouseColumn, this.DecisionLogic);
                    if (chboxRunTimer.Checked)
                    {
                        TimAI2.Enabled = true;
                        TimAI2_Tick(sender, e);
                        btnStart.Text = "Stop";
                    }
                }
            }
        }
        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            Form2 MapEditor = new Form2();
            MapEditor.CustomGameMap = this.GameMap;
            MapEditor.ShowDialog();
            if (MapEditor.MapEditorFlag == true)
            {
                MapEditor.HasGoalPosition = true;
                
                this.MouseRow = 0;
                this.MouseColumn = 0;
                this.MouseLocation = 0;
                this.HeadDirection = 's';
                MouseMemory.SetGameMap(MapEditor.CustomGameMap);
                MouseMemory.SaveCurrentSellDetailsInMemory(0, 0, 'v');
                 SetGraphicStep(0, 0);
                LoadGameMapOnScreen(MapEditor.CustomGameMap);
                MapEditor.Close();
            }
            MapEditor.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TimAI.Enabled = false;
            if (!MapEditorFlag)
                LoadGameMapOnScreen(DefaultGameMap);
            else
                LoadGameMapOnScreen(GameMap);
            this.MouseRow = 0;
            this.MouseColumn = 0;
            this.MouseLocation = 0;
            this.HeadDirection = 's';
            MouseMemory.SetGameMap(GameMap);
            MouseMemory.SaveCurrentSellDetailsInMemory(0,0, 'v');
            SetGraphicStep(0, 0);
        }
        private void SetDecisioLogic(object sender, EventArgs e)
        {
            switch (((Control)sender).Text)
            {
                case "Right Hand":
                    this.DecisionLogic = 'r';
                    break;
                case "Left Hand":
                    this.DecisionLogic = 'l';
                    break;
                case "Random":
                    this.DecisionLogic = 'a';
                    break;
            }
        }
    }
}
