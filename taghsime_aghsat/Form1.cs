using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taghsime_aghsat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Tuple<long, long> ghesting(Int64 kol , Int64 tedad , Int64 darsad , Int64 marja )
        {
            Int64 ghest = 0;
            Int64 naghd = 0;
            Int64 tmp = 0;
            tmp = kol * (100 - darsad) / 100;
            tmp = tmp / tedad;
            ghest = tmp / marja * marja;
            naghd = kol - (tedad * ghest);

            return Tuple.Create(naghd, ghest);
        }


        private void kol_tx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void kol_tx_TextChanged(object sender, EventArgs e)
        {
            if (kol_tx.Text != "")
            {
                kol_tx.Text = String.Format("{0:N0}", Int64.Parse(kol_tx.Text.Replace(",", "")));
                kol_tx.SelectionStart = kol_tx.Text.Length;
                kol_tx.SelectionLength = 0;
                loading();
            }
        }

        void loading()
        {
            if (kol_tx.Text != "" & darsad_tx.Text != "" & tedat_aghsat_tx.Text != ""  )
            {
                Int64 kol;
                Int64 darsad;
                Int64 tedad;
                if (Int64.TryParse(kol_tx.Text.Replace(",", ""),out kol) & Int64.TryParse(darsad_tx.Text, out darsad) & darsad >= 0 & darsad <= 100 & Int64.TryParse(tedat_aghsat_tx.Text, out tedad ))
                {
                    Int64 ghest = 0;
                    Int64 naghd = 0;
                    var t = ghesting(kol, tedad, darsad, 100000);
                    naghd_10_tx.Text = String.Format("{0:N0}", t.Item1);
                    ghest_10_tx.Text = String.Format("{0:N0}", t.Item2);
                    t = ghesting(kol, tedad, darsad, 500000);
                    naghd_50_tx.Text = String.Format("{0:N0}", t.Item1);
                    ghest_50_tx.Text = String.Format("{0:N0}", t.Item2);
                    t = ghesting(kol, tedad, darsad, 1000000);
                    naghd_100_tx.Text = String.Format("{0:N0}", t.Item1);
                    ghest_100_tx.Text = String.Format("{0:N0}", t.Item2);
                }
                else
                {
                    MessageBox.Show("wwwwwww");
                }
            }
        }

        private void tedat_aghsat_tx_TextChanged(object sender, EventArgs e)
        {
            loading();
        }

        private void darsad_tx_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                Int64 darsad = 0;
                if (Int64.TryParse(darsad_tx.Text , out darsad))
                {
                    if (darsad >= 0 & darsad <= 100)
                        loading();
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            
        }

        private void bt_Click(object sender, EventArgs e)
        {
            loading();
        }
    }
}
