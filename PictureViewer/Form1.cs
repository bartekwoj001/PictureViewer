using PictureViewer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PictureViewer
{
    public partial class Form1 : Form
    {
        private string _filePath;

        public Form1()
        {
            InitializeComponent();
            DiappearDeleteButton();

            if (!string.IsNullOrWhiteSpace(IsLastPath))
            {
                pbPicture.Image = Image.FromFile(IsLastPath);
                // pbPicture.Image = Image.FromFile($@"{Path.GetDirectoryName(Application.ExecutablePath)}\path.txt");
                btnDelete.Enabled = true;
            }
            
        }
        private void DiappearDeleteButton()
        {
            if(pbPicture.Image == null) 
                btnDelete.Enabled = false;
        }
        private void SetPicture()
        {            
                try
                {
                    pbPicture.Image = Image.FromFile(_filePath);
                    pbPicture.SizeMode = PictureBoxSizeMode.CenterImage;
                }
                catch (Exception)
                {
                    MessageBox.Show("Nieprawidłowy format pliku!");
                }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            _filePath = openFileDialog.FileName;

            SetPicture();
            btnDelete.Enabled = true;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pbPicture.Image = null;
            btnDelete.Enabled = false;

        }
        public string IsLastPath
        {
            get
            {
                
                return Settings.Default.IsLastPath;
            }
            set
            {
                Settings.Default.IsLastPath = value;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsLastPath = _filePath;
            //File.WriteAllText($@"{Path.GetDirectoryName(Application.ExecutablePath)}\path.txt", _filePath);

            Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

           //var text = File.ReadAllText($@"{Path.GetDirectoryName(Application.ExecutablePath)}\path.txt");
           // if(!string.IsNullOrWhiteSpace(text))
           // pbPicture.Image = Image.FromFile(text);
           
        }
    }
}
