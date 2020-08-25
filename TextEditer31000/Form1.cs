using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31000
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string fileName = "";   //Camel形式（⇔Pascal形式）

        public Form1()
        {
            InitializeComponent();
        }
        //終了メニュー
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //［開く］ダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //StreamReaderクラスを使用してファイル読込み
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"),false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    this.fileName = ofdFileOpen.FileName;   //現在開いているファイル名を設定
                }
            }
        }

        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fileName != "")
            {
                FileSave(fileName);
            }
            else
            {
                SaveNameToolStripMenuItem_Click(sender, e);
            }
        }

        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //［名前を付けて保存］ダイアログを表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }
        //ファイル名を指定しデータを保存
        private void FileSave(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
            }
        }
        //編集メニュークリックイベントハンドラ
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMenuMaskCheck();
        }

        //編集メニュー項目内のマスク処理
        private void EditMenuMaskCheck()
        {
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo;
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo;
            CutToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            CopyToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf);
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //EditMenuMaskCheck();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Text = "";
        }
    }
}
