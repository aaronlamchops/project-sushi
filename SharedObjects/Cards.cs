using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SharedObjects
{
    public class Card : PictureBox
    {
        public string CardType { get; set; }
        public int CardId { get; set; }
        public PictureBox CardImage { get; set; }

        public Card(string cardType)
        {
            CardType = cardType;

            var location = string.Format("../../../Assets/{0}.jpg", CardType);

            Name = CardType;
            Size = new Size(135, 200);
            Image = Image.FromFile(location);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Click += new EventHandler(Card_Click);
        }

        private void Card_Click(object sender, EventArgs e)
        {
            var card = sender as PictureBox;
            MessageBox.Show(card.Name + "\n" + card.Location.ToString());

            using (Graphics g = card.CreateGraphics())
            {
                Pen pen = new Pen(Color.Green, 3);

                g.DrawRectangle(pen, 3, 3, card.Width - 12, card.Height - 12);

                pen.Dispose();
            }
        }
    }
}
