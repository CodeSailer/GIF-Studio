using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GIF_Studio
{
    public partial class Form1 : Form
    {
        public ImageList imgs = new ImageList();
        public int curr_img = 0;

        public Form1()
        {
            InitializeComponent();

            listView1.DragDrop += listView1_DragDrop;
            listView1.DragEnter += listView1_DragEnter;
            listView1_Initialize();

        }

        private void listView1_Initialize()
        {
            imgs.ImageSize = new Size(50, 50);
            listView1.View = View.Details;
            listView1.Columns.Add("Files", listView1.Width - 10);
        }

        private void DragDrop_GetFiles(DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Array.Sort(files);
            imgs.Images.Clear();
            curr_img = 0;
            foreach (string img_path in files)
            {
                imgs.Images.Add(Image.FromFile(img_path));
            }

            listView1.SmallImageList = imgs;
            listView1.Items.Clear();
            //listView1.Items.Add("first image", 0);
            for (int i = 0; i < files.Length; i++)
            {
                listView1.Items.Add(files[i].ToString(), i);
            }
            /*foreach (ListViewItem item in listView1.Items)
            {
                MessageBox.Show(item.ToString());
            }*/
            pictureBox1.Image = Image.FromFile(listView1.Items[curr_img].Text);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            DragDrop_GetFiles(e);
        }

        //next image preview
        private void button3_Click(object sender, EventArgs e)
        {
            if (curr_img + 1 < listView1.Items.Count)
            {
                curr_img++;
                pictureBox1.Image = Image.FromFile(listView1.Items[curr_img].Text);
            }
            else
            {
                curr_img = 0;
                pictureBox1.Image = Image.FromFile(listView1.Items[curr_img].Text);
            }
        }

        //previus image preview
        private void button2_Click(object sender, EventArgs e)
        {
            if (curr_img - 1 >= 0)
            {
                curr_img--;
                pictureBox1.Image = Image.FromFile(listView1.Items[curr_img].Text);
            }
            else
            {
                curr_img = listView1.Items.Count - 1;
                pictureBox1.Image = Image.FromFile(listView1.Items[curr_img].Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //copy each images into temporary folder
            DirectoryInfo di = new DirectoryInfo(".\\images\\");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (ListViewItem element in listView1.Items)
            {
                File.Copy(element.Text, ".\\images\\" + element.Index.ToString() + ".jpg", true);
            }
            if (File.Exists("settings.txt"))
            {
                File.Delete("settings.txt");
            }
            if (textBox1.Text == null || !int.TryParse(textBox1.Text, out int value))
            {
                File.WriteAllText("settings.txt", "1");
            }
            else
            {
                File.WriteAllText("settings.txt", textBox1.Text);
            }
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            if (checkBox1.Checked)
            {
                proc.StartInfo.FileName = "ToGIFHQ.bat";
            }
            else
            {
                proc.StartInfo.FileName = "ToGIF.bat";
            }
            proc.Start();
        }
    }
}
