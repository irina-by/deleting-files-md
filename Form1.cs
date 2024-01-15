// программа должна удалить все пустые файлы с раcширением.md внутри всех папок и подпапок где лежит сама программа.
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

namespace Deleting_files_md
{
    public partial class Удалить : Form
    {
        private string currentDirectory;
        private List<string> deletedFiles;

        public Удалить()
        {
            InitializeComponent();
            currentDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            deletedFiles = new List<string>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить все пустые файлы с расширением '.md'?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                DeleteEmptyFiles(currentDirectory);
                MessageBox.Show("Удаление завершено.\n\nУдаленные файлы:\n" + string.Join("\n", deletedFiles), "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteEmptyFiles(string directory)
        {
            foreach (string file in Directory.GetFiles(directory, "*.md"))
            {
                if (IsFileEmpty(file))
                {
                    File.Delete(file);
                    deletedFiles.Add(file);
                }
            }

            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                DeleteEmptyFiles(subDirectory);
                if (Directory.GetFiles(subDirectory).Length == 0 && Directory.GetDirectories(subDirectory).Length == 0)
                {
                    Directory.Delete(subDirectory);
                }
            }
        }

        private bool IsFileEmpty(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length == 0;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] mdFiles = Directory.GetFiles(currentDirectory, "*.md");
           
        }


    }
}