using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharedObjects;

namespace UserApp
{
    public partial class GameUI : ClientObserver
    {
        public Player Player { get; set; }
        public Dictionary<int, GroupBox> PlayerContainers = new Dictionary<int, GroupBox>();
        public List<Player> PlayerList = new List<Player>();

        public List<Card> Hand = new List<Card>();
        public List<CardTypes> CardTypesInHand = new List<CardTypes>();

        public CardTypes SelectedCard { get; set; }

        public GameUI()
        {
            InitializeComponent();
        }

        private void GameUI_Load(object sender, EventArgs e)
        {
            InitializePlayerSeats();
        }

        private void InitializePlayerSeats()
        {
            foreach (var player in PlayerList)
            {
                GroupBox playerSeat = new GroupBox
                {
                    FlatStyle = FlatStyle.Standard
                };

                //FlowLayoutPanel inside of the playerseat groupbox
                FlowLayoutPanel groupBoxFlowLayout = new FlowLayoutPanel
                {
                    Size = new Size(125, 230)   //Adjust size of the flowlayout inside each groupbox
                };
                groupBoxFlowLayout.Location = new Point(groupBoxFlowLayout.Location.X + 5, groupBoxFlowLayout.Location.Y + 25);

                //Add the flowlayout to the groupbox
                playerSeat.Controls.Add(groupBoxFlowLayout);

                //add the labels to see totals for each player
                AddCardLabels(groupBoxFlowLayout);

                //Adjust size of the groupbox container
                playerSeat.Size = new Size(135, 260);
                playerSeat.Text = player.Name;

                //store in dictionary
                PlayerContainers[player.Id] = playerSeat;

                //add to the GameUI form
                PlayersFlowLayoutPanel.Controls.Add(playerSeat);
            }
        }

        private void AddCardLabels(FlowLayoutPanel gp)
        {
            /*
             * This huge whole creation of labels can maybe be solved by a
             * Factory Pattern 
             * 
             * OR
             * 
             * Store in the player shared object
             */

            Label PuddingLabel = new Label();
            PuddingLabel.Text = "Puddings = ";
            PuddingLabel.Tag = "Pudding";

            Label SashimiLabel = new Label();
            SashimiLabel.Text = "Sashimi = ";
            SashimiLabel.Tag = "Sashimi";

            Label DumplingLabel = new Label();
            DumplingLabel.Text = "Dumplings = ";
            DumplingLabel.Tag = "Dumplings";

            Label MakiLabel = new Label();
            MakiLabel.Text = "Maki's = ";
            MakiLabel.Tag = "Maki";

            Label TempuraLabel = new Label();
            TempuraLabel.Text = "Tempura = ";
            TempuraLabel.Tag = "Tempura";

            Label ChopsticksLabel = new Label();
            ChopsticksLabel.Text = "Chopsticks = ";
            ChopsticksLabel.Tag = "Chopsticks";

            Label EggNigiriLabel = new Label();
            EggNigiriLabel.Text = "Egg Nigiri = ";
            EggNigiriLabel.Tag = "Egg Nigiri";

            Label SalmonNigiriLabel = new Label();
            SalmonNigiriLabel.Text = "Salmon Nigiri = ";
            SalmonNigiriLabel.Tag = "Salmon Nigiri";

            Label SquidNigiriLabel = new Label();
            SquidNigiriLabel.Text = "Squid Nigiri = ";
            SquidNigiriLabel.Tag = "Squid Nigiri";

            Label WasabiLabel = new Label();
            WasabiLabel.Text = "Wasabi = ";
            WasabiLabel.Tag = "Wasabi";

            gp.Controls.Add(PuddingLabel);
            gp.Controls.Add(SashimiLabel);
            gp.Controls.Add(DumplingLabel);
            gp.Controls.Add(MakiLabel);
            gp.Controls.Add(TempuraLabel);
            gp.Controls.Add(ChopsticksLabel);
            gp.Controls.Add(EggNigiriLabel);
            gp.Controls.Add(SalmonNigiriLabel);
            gp.Controls.Add(SquidNigiriLabel);
            gp.Controls.Add(WasabiLabel);
        }

        //implemented from client observer base class
        public override void RefreshDisplay()
        {
            //Update the current hand to the incoming hand
            if (MainContext.GetType() == typeof(List<CardTypes>))
            {
                var incomingHand = (List<CardTypes>)MainContext;

                foreach(CardTypes card in incomingHand)
                {
                    AddCardToHand(card);
                }
            }
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {

            /*
             * TESTING:
             * 
             * This button is just testing out the update ability for the different player
             * score values.
             * 
             * Will implement sending when necessary
             */
            UpdateLabel(0, "Tempura", 100);
            AddCardToHand(CardTypes.Chopsticks);
            AddCardToHand(CardTypes.Pudding);
            AddCardToHand(CardTypes.Sashimi);
            AddCardToHand(CardTypes.Chopsticks);
            AddCardToHand(CardTypes.Dumpling);
        }

        public void UpdateLabel(int playerId, string tag, int value)
        {
            foreach (FlowLayoutPanel panel in PlayerContainers[playerId].Controls)
            {
                foreach (Label label in panel.Controls)
                {
                    if (label.Tag.Equals(tag))
                    {
                        var edit = string.Format("{0} = {1}", tag, value);
                        label.Text = edit;
                    }
                }

            }
        }

        //add cards to the flowlayoutpanel
        public void AddCardToHand(CardTypes cardType)
        {
            Card card = new Card(cardType)
            {
                ChangeToSelectedCard = ChangeSelectedCard      //Delegate passed
            };
            Hand.Add(card);
            CardTypesInHand.Add(card.Type);
            CardHandFlowLayoutPanel.Controls.Add(card);
        }

        public delegate void SelectCardHandler(int playerId, CardTypes card);
        public SelectCardHandler SelectCard { get; set; }

        private void PassCardButton_Click(object sender, EventArgs e)
        {
            //Selected card is selected
            SelectCard(Player.Id, SelectedCard);

            //clears the visual representation of the hand
            foreach(Control control in CardHandFlowLayoutPanel.Controls)
            {
                CardHandFlowLayoutPanel.Controls.Remove(control);
                control.Dispose();
            }

            //empty the hand before receving a new hand
            CardTypesInHand.Clear();
            Hand.Clear();
        }

        //Pass as delegate to the Card shared object to select
        public void ChangeSelectedCard(CardTypes card)
        {
            SelectedCard = card;
        }
    }
}
