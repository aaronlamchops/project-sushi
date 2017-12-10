using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharedObjects
{
    public class Card : PictureBox
    {
        public CardTypes Type { get; set; }
        public int CardId { get; set; }
        public PictureBox CardImage { get; set; }
        public GraphicsState State { get; set; }

        public delegate void ChooseSelectedCardHandler(CardTypes card);
        public ChooseSelectedCardHandler ChangeToSelectedCard { get; set; }

        public Card(CardTypes cardType)
        {
            Type = cardType;

            var location = string.Format("../../../Assets/{0}.jpg", Type.ToString());

            Name = Type.ToString();
            Size = new Size(135, 200);
            Image = Image.FromFile(location);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Click += new EventHandler(Card_Click);
        }

        private void Card_Click(object sender, EventArgs e)
        {
            var card = sender as PictureBox;
            MessageBox.Show(card.Name + " Chosen!\n" + card.Location.ToString());
            ChangeToSelectedCard(Type);

            using (Graphics g = card.CreateGraphics())
            {
                State = g.Save();

                Pen pen = new Pen(Color.Green, 3);

                g.DrawRectangle(pen, 3, 3, card.Width - 12, card.Height - 12);

                pen.Dispose();
            }
        }
    }
}
